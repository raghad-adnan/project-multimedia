using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;

namespace AudioCompressionProject
{
    public partial class Form1 : Form
    {
        string currentFile = "";
        string compressedFilePath = "";

        WaveOutEvent outputDevice;
        AudioFileReader audioFile;

        CancellationTokenSource cancellationTokenSource;
        Stopwatch stopwatch;

        // Quantization step used for the residual of DPCM / Predictive coding.
        // 16-bit residual -> signed 8-bit (1 byte) => ~50% size reduction.
        const short ResidualStep = 256;

        // Payload reported to the UI during compression so the live charts use
        // REAL measured data instead of hard-coded constants.
        private struct ProgressInfo
        {
            public int Percent;
            public long InputBytes;   // input bytes consumed so far
            public long OutputBytes;  // output bytes produced so far
        }

        public Form1()
        {
            InitializeComponent();

            cmbAlgorithm.Items.Add("Nonlinear Quantization");
            cmbAlgorithm.Items.Add("DPCM");
            cmbAlgorithm.Items.Add("Delta Modulation");
            cmbAlgorithm.Items.Add("Predictive Differential Coding");
            cmbAlgorithm.Items.Add("Adaptive Delta Modulation");

            this.AllowDrop = true;

            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
            this.FormClosing += Form1_FormClosing;

            InitializeCharts();
            UpdateStatus("Ready");

            // Add additional parameter controls programmatically
            AddParameterControls();
        }

        private void AddParameterControls()
        {
            // Increase groupbox height to accommodate new controls
            grpCompressionSettings.Height = 160;

            // Create mu parameter control (Mu-law companding factor)
            Label lblMu = new Label();
            lblMu.Text = "Mu (μ):";
            lblMu.Location = new System.Drawing.Point(650, 30);
            lblMu.Size = new System.Drawing.Size(60, 20);
            lblMu.ForeColor = System.Drawing.Color.White;
            grpCompressionSettings.Controls.Add(lblMu);

            NumericUpDown nudMu = new NumericUpDown();
            nudMu.Name = "nudMu";
            nudMu.Location = new System.Drawing.Point(720, 25);
            nudMu.Size = new System.Drawing.Size(80, 25);
            nudMu.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            nudMu.ForeColor = System.Drawing.Color.White;
            nudMu.Minimum = 1;
            nudMu.Maximum = 500;
            nudMu.Value = 255;
            nudMu.Increment = 5;
            grpCompressionSettings.Controls.Add(nudMu);

            // Create step parameter control (Delta / ADM step, scaled for 16-bit audio)
            Label lblStep = new Label();
            lblStep.Text = "Step:";
            lblStep.Location = new System.Drawing.Point(650, 70);
            lblStep.Size = new System.Drawing.Size(60, 20);
            lblStep.ForeColor = System.Drawing.Color.White;
            grpCompressionSettings.Controls.Add(lblStep);

            NumericUpDown nudStep = new NumericUpDown();
            nudStep.Name = "nudStep";
            nudStep.Location = new System.Drawing.Point(720, 65);
            nudStep.Size = new System.Drawing.Size(80, 25);
            nudStep.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            nudStep.ForeColor = System.Drawing.Color.White;
            nudStep.Minimum = 1;
            nudStep.Maximum = 8000;
            nudStep.Value = 512;
            nudStep.Increment = 16;
            grpCompressionSettings.Controls.Add(nudStep);

            // Create minStep parameter control
            Label lblMinStep = new Label();
            lblMinStep.Text = "Min Step:";
            lblMinStep.Location = new System.Drawing.Point(650, 95);
            lblMinStep.Size = new System.Drawing.Size(70, 20);
            lblMinStep.ForeColor = System.Drawing.Color.White;
            grpCompressionSettings.Controls.Add(lblMinStep);

            NumericUpDown nudMinStep = new NumericUpDown();
            nudMinStep.Name = "nudMinStep";
            nudMinStep.Location = new System.Drawing.Point(720, 90);
            nudMinStep.Size = new System.Drawing.Size(80, 25);
            nudMinStep.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            nudMinStep.ForeColor = System.Drawing.Color.White;
            nudMinStep.Minimum = 1;
            nudMinStep.Maximum = 2000;
            nudMinStep.Value = 64;
            nudMinStep.Increment = 8;
            grpCompressionSettings.Controls.Add(nudMinStep);

            // Create maxStep parameter control
            Label lblMaxStep = new Label();
            lblMaxStep.Text = "Max Step:";
            lblMaxStep.Location = new System.Drawing.Point(650, 120);
            lblMaxStep.Size = new System.Drawing.Size(70, 20);
            lblMaxStep.ForeColor = System.Drawing.Color.White;
            grpCompressionSettings.Controls.Add(lblMaxStep);

            NumericUpDown nudMaxStep = new NumericUpDown();
            nudMaxStep.Name = "nudMaxStep";
            nudMaxStep.Location = new System.Drawing.Point(720, 115);
            nudMaxStep.Size = new System.Drawing.Size(80, 25);
            nudMaxStep.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            nudMaxStep.ForeColor = System.Drawing.Color.White;
            nudMaxStep.Minimum = 64;
            nudMaxStep.Maximum = 16000;
            nudMaxStep.Value = 4096;
            nudMaxStep.Increment = 128;
            grpCompressionSettings.Controls.Add(nudMaxStep);
        }

        private void InitializeCharts()
        {
            chartCompressionRatio.Series[0].Points.Clear();
            chartProcessingSpeed.Series[0].Points.Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "WAV Audio|*.wav";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFile = ofd.FileName;
                listBoxFiles.Items.Clear();
                listBoxFiles.Items.Add(currentFile);
                ShowAudioProperties(currentFile);
            }
        }

        private void ShowAudioProperties(string file)
        {
            try
            {
                FileInfo fi = new FileInfo(file);

                // Use WaveFileReader (not AudioFileReader) so the TRUE format is reported.
                // AudioFileReader always converts to 32-bit IEEE float, which would make
                // the encoding and bit-rate fields wrong.
                using (var reader = new WaveFileReader(file))
                {
                    var f = reader.WaveFormat;
                    lblSize.Text = "Size: " + FormatFileSize(fi.Length);
                    lblDuration.Text = "Duration: " + reader.TotalTime.ToString(@"hh\:mm\:ss\.fff");
                    lblSampleRate.Text = "Sample Rate: " + f.SampleRate + " Hz";
                    lblChannels.Text = "Channels: " + f.Channels;
                    lblBitRate.Text = "Bit Rate: " + (f.AverageBytesPerSecond * 8) + " bps";
                    lblEncoding.Text = "Encoding: " + f.Encoding + " (" + f.BitsPerSample + "-bit)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot read audio file: " + ex.Message);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (currentFile == "") return;

            // Stop & dispose any existing playback first so repeated Play presses
            // don't stack overlapping (and unstoppable) playback.
            StopPlayback();

            try
            {
                outputDevice = new WaveOutEvent();
                audioFile = new AudioFileReader(currentFile);
                outputDevice.Init(audioFile);
                outputDevice.Play();
            }
            catch (Exception ex)
            {
                StopPlayback();
                MessageBox.Show("Cannot play file: " + ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        // Single point of teardown for playback. Stops the active device (if any)
        // and releases both the device and the file handle.
        private void StopPlayback()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
        }

        private async void btnCompress_Click(object sender, EventArgs e)
        {
            if (currentFile == "")
            {
                MessageBox.Show("Load a WAV file first");
                return;
            }

            if (cmbAlgorithm.SelectedItem == null)
            {
                MessageBox.Show("Select Algorithm");
                return;
            }

            // Release any playback handle so the input file is not locked.
            StopPlayback();

            cancellationTokenSource = new CancellationTokenSource();
            stopwatch = new Stopwatch();

            InitializeCharts();

            UpdateStatus("Compressing");
            btnCancel.Enabled = true;
            btnCompress.Enabled = false;
            btnSave.Enabled = false;

            string algorithm = cmbAlgorithm.SelectedItem.ToString();

            // Read compression settings from the UI controls.
            int sampleRate = (int)nudSampleRate.Value;
            int levels = (int)nudQuantizationLevels.Value;

            double mu = 255.0;
            short step = 512;
            short minStep = 64;
            short maxStep = 4096;

            NumericUpDown nudMu = grpCompressionSettings.Controls.Find("nudMu", true).FirstOrDefault() as NumericUpDown;
            if (nudMu != null) mu = (double)nudMu.Value;

            NumericUpDown nudStep = grpCompressionSettings.Controls.Find("nudStep", true).FirstOrDefault() as NumericUpDown;
            if (nudStep != null) step = (short)nudStep.Value;

            NumericUpDown nudMinStep = grpCompressionSettings.Controls.Find("nudMinStep", true).FirstOrDefault() as NumericUpDown;
            if (nudMinStep != null) minStep = (short)nudMinStep.Value;

            NumericUpDown nudMaxStep = grpCompressionSettings.Controls.Find("nudMaxStep", true).FirstOrDefault() as NumericUpDown;
            if (nudMaxStep != null) maxStep = (short)nudMaxStep.Value;

            // Human-readable settings summary for the report.
            string settings;
            if (algorithm == "Nonlinear Quantization") settings = "mu=" + mu + ", levels=" + levels;
            else if (algorithm == "Delta Modulation") settings = "step=" + step;
            else if (algorithm == "Adaptive Delta Modulation") settings = "step=" + step + ", min=" + minStep + ", max=" + maxStep;
            else settings = "2nd-order/8-bit residual";

            compressedFilePath =
                Path.Combine(
                Path.GetDirectoryName(currentFile),
                Path.GetFileNameWithoutExtension(currentFile) + "_compressed.bin");

            try
            {
                // Normalize the input to 16-bit PCM WAV at the chosen sample rate.
                // This is what wires the "Sample Rate" control into the pipeline and
                // guarantees every algorithm receives clean 16-bit samples.
                byte[] data = await Task.Run(() => PrepareInputData(currentFile, sampleRate));

                int channels = data.Length >= 24 ? BitConverter.ToInt16(data, 22) : 1;
                if (channels < 1) channels = 1;

                await Task.Run(() => CompressAudio(data, algorithm, compressedFilePath,
                    cancellationTokenSource.Token, mu, levels, step, minStep, maxStep));

                if (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    CalculateAndDisplayStatistics(algorithm, settings, sampleRate, channels);
                    UpdateStatus("Completed");
                    btnSave.Enabled = true;
                    MessageBox.Show("Compression Completed");
                }
                else
                {
                    UpdateStatus("Cancelled");
                }
            }
            catch (OperationCanceledException)
            {
                UpdateStatus("Cancelled");
            }
            catch (Exception ex)
            {
                UpdateStatus("Error");
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnCancel.Enabled = false;
                btnCompress.Enabled = true;
                cancellationTokenSource?.Dispose();
            }
        }

        // Decode the input file and re-encode it as 16-bit PCM WAV at the target
        // sample rate. Resampling only runs when the rate actually differs.
        private byte[] PrepareInputData(string file, int targetSampleRate)
        {
            using (var reader = new AudioFileReader(file))
            {
                ISampleProvider source = reader;
                if (reader.WaveFormat.SampleRate != targetSampleRate)
                    source = new WdlResamplingSampleProvider(reader, targetSampleRate);

                IWaveProvider pcm16 = source.ToWaveProvider16();
                using (var ms = new MemoryStream())
                {
                    WaveFileWriter.WriteWavFileToStream(ms, pcm16);
                    return ms.ToArray();
                }
            }
        }

        private void CompressAudio(byte[] data, string algorithm, string outputPath, CancellationToken token,
                                   double mu, int levels, short step, short minStep, short maxStep)
        {
            stopwatch.Start();
            long totalBytes = data.Length;

            // Real-time progress: ratio and speed are derived from the ACTUAL number
            // of input/output bytes, not from per-algorithm constants.
            Progress<ProgressInfo> progress = new Progress<ProgressInfo>(info =>
            {
                UpdateProgress(info.Percent);

                double ratio = info.InputBytes > 0
                    ? (1.0 - ((double)info.OutputBytes / info.InputBytes)) * 100.0
                    : 0;
                UpdateCompressionChart(ratio);

                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                if (elapsedSeconds > 0)
                {
                    double processedMB = info.InputBytes / (1024.0 * 1024.0);
                    UpdateSpeedChart(processedMB / elapsedSeconds);
                }
            });

            byte[] compressedData;
            if (algorithm == "Nonlinear Quantization")
                compressedData = NonlinearQuantization(data, token, progress, mu, levels);
            else if (algorithm == "DPCM")
                compressedData = DPCM(data, token, progress);
            else if (algorithm == "Delta Modulation")
                compressedData = DeltaModulation(data, token, progress, step);
            else if (algorithm == "Predictive Differential Coding")
                compressedData = PredictiveDifferentialCoding(data, token, progress);
            else if (algorithm == "Adaptive Delta Modulation")
                compressedData = AdaptiveDeltaModulation(data, token, progress, step, minStep, maxStep);
            else
                compressedData = data;

            // Cancelled mid-way -> do not write a partial file.
            if (compressedData == null)
            {
                stopwatch.Stop();
                return;
            }

            double finalRatio = ((double)(totalBytes - compressedData.Length) / totalBytes) * 100;
            UpdateCompressionChart(finalRatio);
            UpdateProgress(100);

            File.WriteAllBytes(outputPath, compressedData);
            stopwatch.Stop();
        }

        private void CalculateAndDisplayStatistics(string algorithm, string settings, int sampleRate, int channels)
        {
            FileInfo originalInfo = new FileInfo(currentFile);
            FileInfo compressedInfo = new FileInfo(compressedFilePath);

            long originalSize = originalInfo.Length;
            long compressedSize = compressedInfo.Length;
            double compressionRatio = ((double)(originalSize - compressedSize) / originalSize) * 100;
            long spaceSaved = originalSize - compressedSize;
            double processingTime = stopwatch.Elapsed.TotalSeconds;

            int bitRate = 16 * sampleRate * channels;

            ShowFinalReport(
                FormatFileSize(originalSize),
                FormatFileSize(compressedSize),
                compressionRatio.ToString("F2") + "%",
                FormatFileSize(spaceSaved),
                processingTime.ToString("F2") + " s",
                algorithm + (string.IsNullOrEmpty(settings) ? "" : " (" + settings + ")"),
                sampleRate + " Hz",
                bitRate + " bps"
            );
        }

        private string FormatFileSize(long bytes)
        {
            bool negative = bytes < 0;
            long abs = Math.Abs(bytes);
            string text;
            if (abs < 1024)
                text = abs + " B";
            else if (abs < 1024 * 1024)
                text = (abs / 1024.0).ToString("F2") + " KB";
            else
                text = (abs / (1024.0 * 1024.0)).ToString("F2") + " MB";
            return negative ? "-" + text : text;
        }

        private void ShowFinalReport(
            string originalSize, string compressedSize, string compressionRatio,
            string savedSpace, string processingTime, string algorithm,
            string sampleRate, string bitRate)
        {
            lblReportOriginalSize.Text = originalSize;
            lblReportCompressedSize.Text = compressedSize;
            lblReportCompressionRatio.Text = compressionRatio;
            lblReportSpaceSaved.Text = savedSpace;
            lblReportProcessingTime.Text = processingTime;
            lblReportAlgorithm.Text = algorithm;
            lblReportSampleRate.Text = sampleRate;
            lblReportBitRate.Text = bitRate;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            UpdateStatus("Cancelling...");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            StopPlayback();

            currentFile = "";
            compressedFilePath = "";
            listBoxFiles.Items.Clear();

            cmbAlgorithm.SelectedIndex = -1;
            nudSampleRate.Value = 44100;
            nudQuantizationLevels.Value = 256;

            // Reset new parameter controls
            NumericUpDown nudMu = grpCompressionSettings.Controls.Find("nudMu", true).FirstOrDefault() as NumericUpDown;
            if (nudMu != null) nudMu.Value = 255;

            NumericUpDown nudStep = grpCompressionSettings.Controls.Find("nudStep", true).FirstOrDefault() as NumericUpDown;
            if (nudStep != null) nudStep.Value = 512;

            NumericUpDown nudMinStep = grpCompressionSettings.Controls.Find("nudMinStep", true).FirstOrDefault() as NumericUpDown;
            if (nudMinStep != null) nudMinStep.Value = 64;

            NumericUpDown nudMaxStep = grpCompressionSettings.Controls.Find("nudMaxStep", true).FirstOrDefault() as NumericUpDown;
            if (nudMaxStep != null) nudMaxStep.Value = 4096;

            progressCompression.Value = 0;
            lblProgressPercent.Text = "0%";
            UpdateStatus("Ready");

            InitializeCharts();

            lblReportOriginalSize.Text = "-";
            lblReportCompressedSize.Text = "-";
            lblReportCompressionRatio.Text = "-";
            lblReportSpaceSaved.Text = "-";
            lblReportProcessingTime.Text = "-";
            lblReportAlgorithm.Text = "-";
            lblReportSampleRate.Text = "-";
            lblReportBitRate.Text = "-";

            lblSize.Text = "";
            lblDuration.Text = "";
            lblSampleRate.Text = "";
            lblChannels.Text = "";
            lblBitRate.Text = "";
            lblEncoding.Text = "";

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnCompress.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (compressedFilePath == "" || !File.Exists(compressedFilePath))
            {
                MessageBox.Show("No compressed file to save");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Binary Files|*.bin";
            sfd.FileName = Path.GetFileName(compressedFilePath);

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.Copy(compressedFilePath, sfd.FileName, true);
                MessageBox.Show("File saved successfully");
            }
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Compressed Files|*.bin";

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string compressedFile = ofd.FileName;

            try
            {
                byte[] data = File.ReadAllBytes(compressedFile);
                if (data.Length < 1)
                {
                    MessageBox.Show("Invalid compressed file");
                    return;
                }

                // The algorithm is identified by the first byte that was written at
                // compression time -- we no longer depend on the ComboBox selection.
                byte algoId = data[0];
                byte[] decompressedData;
                string usedAlgorithm;

                switch (algoId)
                {
                    case 0x01: decompressedData = NonlinearQuantizationDecompress(data); usedAlgorithm = "Nonlinear Quantization"; break;
                    case 0x02: decompressedData = DPCMDecompress(data); usedAlgorithm = "DPCM"; break;
                    case 0x03: decompressedData = DeltaModulationDecompress(data); usedAlgorithm = "Delta Modulation"; break;
                    case 0x04: decompressedData = PredictiveDifferentialCodingDecompress(data); usedAlgorithm = "Predictive Differential Coding"; break;
                    case 0x05: decompressedData = AdaptiveDeltaModulationDecompress(data); usedAlgorithm = "Adaptive Delta Modulation"; break;
                    default:
                        MessageBox.Show("Unrecognized algorithm ID in file. Was it produced by this app?");
                        return;
                }

                string output = Path.Combine(
                    Path.GetDirectoryName(compressedFile),
                    Path.GetFileNameWithoutExtension(compressedFile) + "_decompressed.wav");
                File.WriteAllBytes(output, decompressedData);
                MessageBox.Show("Decompression Completed (" + usedAlgorithm + ")\nSaved to: " + output);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Decompression failed: " + ex.Message);
            }
        }

        // ============================================================
        // Helpers
        // ============================================================

        // Split a WAV byte buffer into its header (everything up to and including
        // the "data" chunk descriptor) and the raw audio sample bytes.
        private void SplitWav(byte[] data, out byte[] header, out byte[] audio)
        {
            int headerSize = GetWavHeaderSize(data);
            if (headerSize > data.Length) headerSize = data.Length;

            header = new byte[headerSize];
            Array.Copy(data, 0, header, 0, headerSize);

            int audioLen = data.Length - headerSize;
            audio = new byte[audioLen];
            Array.Copy(data, headerSize, audio, 0, audioLen);
        }

        private byte[] Concat(byte[] header, byte[] audio)
        {
            byte[] result = new byte[header.Length + audio.Length];
            Array.Copy(header, 0, result, 0, header.Length);
            Array.Copy(audio, 0, result, header.Length, audio.Length);
            return result;
        }

        private static short ClampShort(int value)
        {
            if (value > short.MaxValue) return short.MaxValue;
            if (value < short.MinValue) return short.MinValue;
            return (short)value;
        }

        // ============================================================
        // ALGORITHM 1: Nonlinear Quantization (Mu-law companding)
        // Each 16-bit sample -> 1 quantized byte  =>  ~50% size reduction.
        // "levels" controls the number of quantization steps (quality).
        // ============================================================
        private byte[] NonlinearQuantization(byte[] data, CancellationToken token, IProgress<ProgressInfo> progress, double mu, int levels)
        {
            if (levels < 2) levels = 2;
            if (levels > 256) levels = 256;
            int maxIndex = levels - 1;

            byte[] header, audio;
            SplitWav(data, out header, out audio);

            List<byte> result = new List<byte>();
            result.Add(0x01); // Algorithm ID
            result.AddRange(BitConverter.GetBytes(header.Length));
            result.AddRange(BitConverter.GetBytes(audio.Length));
            result.AddRange(BitConverter.GetBytes(mu));
            result.AddRange(BitConverter.GetBytes(levels));
            result.AddRange(header);

            int sampleCount = audio.Length / 2;
            int chunk = 4000;

            for (int i = 0; i < sampleCount; i += chunk)
            {
                if (token.IsCancellationRequested) return null;

                int end = Math.Min(i + chunk, sampleCount);
                for (int s = i; s < end; s++)
                {
                    short sample = BitConverter.ToInt16(audio, s * 2);
                    double normalized = sample / 32768.0;
                    double companded = Math.Sign(normalized) * (Math.Log(1 + mu * Math.Abs(normalized)) / Math.Log(1 + mu));
                    int q = (int)Math.Round(((companded + 1.0) / 2.0) * maxIndex);
                    if (q < 0) q = 0;
                    if (q > maxIndex) q = maxIndex;
                    result.Add((byte)q);
                }

                progress?.Report(new ProgressInfo
                {
                    Percent = (int)(end / (double)sampleCount * 100),
                    InputBytes = (long)header.Length + end * 2,
                    OutputBytes = result.Count
                });
            }

            progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
            return result.ToArray();
        }

        private byte[] NonlinearQuantizationDecompress(byte[] data)
        {
            int offset = 1;
            int headerSize = BitConverter.ToInt32(data, offset); offset += 4;
            int audioLen = BitConverter.ToInt32(data, offset); offset += 4;
            double mu = BitConverter.ToDouble(data, offset); offset += 8;
            int levels = BitConverter.ToInt32(data, offset); offset += 4;
            if (levels < 2) levels = 2;
            int maxIndex = levels - 1;

            byte[] header = new byte[headerSize];
            Array.Copy(data, offset, header, 0, headerSize); offset += headerSize;

            int sampleCount = audioLen / 2;
            List<byte> audio = new List<byte>(audioLen);

            for (int s = 0; s < sampleCount && offset < data.Length; s++, offset++)
            {
                int q = data[offset];
                double companded = ((double)q / maxIndex) * 2.0 - 1.0;
                double normalized = Math.Sign(companded) * (1.0 / mu) * (Math.Pow(1 + mu, Math.Abs(companded)) - 1.0);
                short sample = ClampShort((int)Math.Round(normalized * 32768.0));
                audio.AddRange(BitConverter.GetBytes(sample));
            }

            return Concat(header, audio.ToArray());
        }

        // ============================================================
        // ALGORITHM 2: DPCM (1st-order predictor, quantized residual)
        // First sample stored verbatim; each later sample stored as an 8-bit
        // quantized difference  =>  ~50% size reduction (lossy).
        // The predictor uses the RECONSTRUCTED value to avoid drift.
        // ============================================================
        private byte[] DPCM(byte[] data, CancellationToken token, IProgress<ProgressInfo> progress)
        {
            short qstep = ResidualStep;

            byte[] header, audio;
            SplitWav(data, out header, out audio);

            List<byte> result = new List<byte>();
            result.Add(0x02); // Algorithm ID
            result.AddRange(BitConverter.GetBytes(header.Length));
            result.AddRange(BitConverter.GetBytes(audio.Length));
            result.AddRange(BitConverter.GetBytes(qstep));
            result.AddRange(header);

            int sampleCount = audio.Length / 2;
            if (sampleCount == 0)
            {
                progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
                return result.ToArray();
            }

            short prev = BitConverter.ToInt16(audio, 0);
            result.AddRange(BitConverter.GetBytes(prev)); // first sample verbatim

            int chunk = 4000;
            for (int i = 1; i < sampleCount; i += chunk)
            {
                if (token.IsCancellationRequested) return null;

                int end = Math.Min(i + chunk, sampleCount);
                for (int s = i; s < end; s++)
                {
                    short sample = BitConverter.ToInt16(audio, s * 2);
                    int diff = sample - prev;
                    int r = (int)Math.Round(diff / (double)qstep);
                    if (r < -128) r = -128;
                    if (r > 127) r = 127;
                    result.Add((byte)(sbyte)r);
                    prev = ClampShort(prev + r * qstep);
                }

                progress?.Report(new ProgressInfo
                {
                    Percent = (int)(end / (double)sampleCount * 100),
                    InputBytes = (long)header.Length + end * 2,
                    OutputBytes = result.Count
                });
            }

            progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
            return result.ToArray();
        }

        private byte[] DPCMDecompress(byte[] data)
        {
            int offset = 1;
            int headerSize = BitConverter.ToInt32(data, offset); offset += 4;
            int audioLen = BitConverter.ToInt32(data, offset); offset += 4;
            short qstep = BitConverter.ToInt16(data, offset); offset += 2;

            byte[] header = new byte[headerSize];
            Array.Copy(data, offset, header, 0, headerSize); offset += headerSize;

            int sampleCount = audioLen / 2;
            List<byte> audio = new List<byte>(audioLen);
            if (sampleCount == 0)
                return Concat(header, audio.ToArray());

            short prev = BitConverter.ToInt16(data, offset); offset += 2;
            audio.AddRange(BitConverter.GetBytes(prev));

            for (int s = 1; s < sampleCount && offset < data.Length; s++, offset++)
            {
                sbyte r = (sbyte)data[offset];
                prev = ClampShort(prev + r * qstep);
                audio.AddRange(BitConverter.GetBytes(prev));
            }

            return Concat(header, audio.ToArray());
        }

        // ============================================================
        // ALGORITHM 3: Delta Modulation (1 bit per 16-bit sample)
        // First sample stored verbatim; every later sample -> 1 bit  =>  ~94%.
        // ============================================================
        private byte[] DeltaModulation(byte[] data, CancellationToken token, IProgress<ProgressInfo> progress, short step)
        {
            if (step < 1) step = 1;

            byte[] header, audio;
            SplitWav(data, out header, out audio);

            List<byte> result = new List<byte>();
            result.Add(0x03); // Algorithm ID
            result.AddRange(BitConverter.GetBytes(header.Length));
            result.AddRange(BitConverter.GetBytes(audio.Length));
            result.AddRange(BitConverter.GetBytes(step));
            result.AddRange(header);

            int sampleCount = audio.Length / 2;
            if (sampleCount == 0)
            {
                progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
                return result.ToArray();
            }

            short predicted = BitConverter.ToInt16(audio, 0);
            result.AddRange(BitConverter.GetBytes(predicted)); // first sample verbatim

            int bitBuffer = 0;
            int bitCount = 0;
            int chunk = 8000;

            for (int i = 1; i < sampleCount; i += chunk)
            {
                if (token.IsCancellationRequested) return null;

                int end = Math.Min(i + chunk, sampleCount);
                for (int s = i; s < end; s++)
                {
                    short sample = BitConverter.ToInt16(audio, s * 2);
                    int bit;
                    if (sample >= predicted)
                    {
                        bit = 1;
                        predicted = ClampShort(predicted + step);
                    }
                    else
                    {
                        bit = 0;
                        predicted = ClampShort(predicted - step);
                    }

                    bitBuffer = (bitBuffer << 1) | bit;
                    bitCount++;
                    if (bitCount == 8)
                    {
                        result.Add((byte)bitBuffer);
                        bitBuffer = 0;
                        bitCount = 0;
                    }
                }

                progress?.Report(new ProgressInfo
                {
                    Percent = (int)(end / (double)sampleCount * 100),
                    InputBytes = (long)header.Length + end * 2,
                    OutputBytes = result.Count
                });
            }

            if (bitCount > 0)
            {
                bitBuffer <<= (8 - bitCount);
                result.Add((byte)bitBuffer);
            }

            progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
            return result.ToArray();
        }

        private byte[] DeltaModulationDecompress(byte[] data)
        {
            int offset = 1;
            int headerSize = BitConverter.ToInt32(data, offset); offset += 4;
            int audioLen = BitConverter.ToInt32(data, offset); offset += 4;
            short step = BitConverter.ToInt16(data, offset); offset += 2;

            byte[] header = new byte[headerSize];
            Array.Copy(data, offset, header, 0, headerSize); offset += headerSize;

            int sampleCount = audioLen / 2;
            List<byte> audio = new List<byte>(audioLen);
            if (sampleCount == 0)
                return Concat(header, audio.ToArray());

            short predicted = BitConverter.ToInt16(data, offset); offset += 2;
            audio.AddRange(BitConverter.GetBytes(predicted));
            int produced = 1;

            for (int i = offset; i < data.Length && produced < sampleCount; i++)
            {
                byte byteVal = data[i];
                for (int bit = 7; bit >= 0 && produced < sampleCount; bit--)
                {
                    int b = (byteVal >> bit) & 1;
                    if (b == 1)
                        predicted = ClampShort(predicted + step);
                    else
                        predicted = ClampShort(predicted - step);
                    audio.AddRange(BitConverter.GetBytes(predicted));
                    produced++;
                }
            }

            return Concat(header, audio.ToArray());
        }

        // ============================================================
        // ALGORITHM 4: Predictive Differential Coding
        // 2nd-order linear predictor (2*p1 - p2), residual quantized to 8 bits
        // =>  ~50% size reduction (lossy). Predictor uses reconstructed samples.
        // ============================================================
        private byte[] PredictiveDifferentialCoding(byte[] data, CancellationToken token, IProgress<ProgressInfo> progress)
        {
            short qstep = ResidualStep;

            byte[] header, audio;
            SplitWav(data, out header, out audio);

            List<byte> result = new List<byte>();
            result.Add(0x04); // Algorithm ID
            result.AddRange(BitConverter.GetBytes(header.Length));
            result.AddRange(BitConverter.GetBytes(audio.Length));
            result.AddRange(BitConverter.GetBytes(qstep));
            result.AddRange(header);

            int sampleCount = audio.Length / 2;
            if (sampleCount == 0)
            {
                progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
                return result.ToArray();
            }

            short prev2 = BitConverter.ToInt16(audio, 0);
            result.AddRange(BitConverter.GetBytes(prev2));
            short prev1 = prev2;

            if (sampleCount > 1)
            {
                prev1 = BitConverter.ToInt16(audio, 2);
                result.AddRange(BitConverter.GetBytes(prev1));
            }

            int chunk = 4000;
            for (int i = 2; i < sampleCount; i += chunk)
            {
                if (token.IsCancellationRequested) return null;

                int end = Math.Min(i + chunk, sampleCount);
                for (int s = i; s < end; s++)
                {
                    short sample = BitConverter.ToInt16(audio, s * 2);
                    int prediction = ClampShort(2 * prev1 - prev2);
                    int error = sample - prediction;
                    int r = (int)Math.Round(error / (double)qstep);
                    if (r < -128) r = -128;
                    if (r > 127) r = 127;
                    result.Add((byte)(sbyte)r);
                    short recon = ClampShort(prediction + r * qstep);
                    prev2 = prev1;
                    prev1 = recon;
                }

                progress?.Report(new ProgressInfo
                {
                    Percent = (int)(end / (double)sampleCount * 100),
                    InputBytes = (long)header.Length + end * 2,
                    OutputBytes = result.Count
                });
            }

            progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
            return result.ToArray();
        }

        private byte[] PredictiveDifferentialCodingDecompress(byte[] data)
        {
            int offset = 1;
            int headerSize = BitConverter.ToInt32(data, offset); offset += 4;
            int audioLen = BitConverter.ToInt32(data, offset); offset += 4;
            short qstep = BitConverter.ToInt16(data, offset); offset += 2;

            byte[] header = new byte[headerSize];
            Array.Copy(data, offset, header, 0, headerSize); offset += headerSize;

            int sampleCount = audioLen / 2;
            List<byte> audio = new List<byte>(audioLen);
            if (sampleCount == 0)
                return Concat(header, audio.ToArray());

            short prev2 = BitConverter.ToInt16(data, offset); offset += 2;
            audio.AddRange(BitConverter.GetBytes(prev2));
            int produced = 1;
            short prev1 = prev2;

            if (sampleCount > 1)
            {
                prev1 = BitConverter.ToInt16(data, offset); offset += 2;
                audio.AddRange(BitConverter.GetBytes(prev1));
                produced = 2;
            }

            for (int i = offset; i < data.Length && produced < sampleCount; i++, produced++)
            {
                sbyte r = (sbyte)data[i];
                int prediction = ClampShort(2 * prev1 - prev2);
                short recon = ClampShort(prediction + r * qstep);
                audio.AddRange(BitConverter.GetBytes(recon));
                prev2 = prev1;
                prev1 = recon;
            }

            return Concat(header, audio.ToArray());
        }

        // ============================================================
        // ALGORITHM 5: Adaptive Delta Modulation (1 adaptive bit per sample)
        // Like Delta Modulation but the step doubles/halves between
        // minStep and maxStep to track the signal  =>  ~94% size reduction.
        // ============================================================
        private byte[] AdaptiveDeltaModulation(byte[] data, CancellationToken token, IProgress<ProgressInfo> progress, short step, short minStep, short maxStep)
        {
            if (step < 1) step = 1;
            if (minStep < 1) minStep = 1;
            if (maxStep < minStep) maxStep = minStep;

            byte[] header, audio;
            SplitWav(data, out header, out audio);

            List<byte> result = new List<byte>();
            result.Add(0x05); // Algorithm ID
            result.AddRange(BitConverter.GetBytes(header.Length));
            result.AddRange(BitConverter.GetBytes(audio.Length));
            result.AddRange(BitConverter.GetBytes(step));
            result.AddRange(BitConverter.GetBytes(minStep));
            result.AddRange(BitConverter.GetBytes(maxStep));
            result.AddRange(header);

            int sampleCount = audio.Length / 2;
            if (sampleCount == 0)
            {
                progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
                return result.ToArray();
            }

            short predicted = BitConverter.ToInt16(audio, 0);
            result.AddRange(BitConverter.GetBytes(predicted)); // first sample verbatim

            int curStep = step;
            int bitBuffer = 0;
            int bitCount = 0;
            int previousBit = 0;
            int chunk = 8000;

            for (int i = 1; i < sampleCount; i += chunk)
            {
                if (token.IsCancellationRequested) return null;

                int end = Math.Min(i + chunk, sampleCount);
                for (int s = i; s < end; s++)
                {
                    short sample = BitConverter.ToInt16(audio, s * 2);
                    int bit;
                    if (sample >= predicted)
                    {
                        bit = 1;
                        predicted = ClampShort(predicted + curStep);
                    }
                    else
                    {
                        bit = 0;
                        predicted = ClampShort(predicted - curStep);
                    }

                    if (bit == previousBit)
                        curStep = Math.Min(maxStep, curStep * 2);
                    else
                        curStep = Math.Max(minStep, curStep / 2);
                    previousBit = bit;

                    bitBuffer = (bitBuffer << 1) | bit;
                    bitCount++;
                    if (bitCount == 8)
                    {
                        result.Add((byte)bitBuffer);
                        bitBuffer = 0;
                        bitCount = 0;
                    }
                }

                progress?.Report(new ProgressInfo
                {
                    Percent = (int)(end / (double)sampleCount * 100),
                    InputBytes = (long)header.Length + end * 2,
                    OutputBytes = result.Count
                });
            }

            if (bitCount > 0)
            {
                bitBuffer <<= (8 - bitCount);
                result.Add((byte)bitBuffer);
            }

            progress?.Report(new ProgressInfo { Percent = 100, InputBytes = data.Length, OutputBytes = result.Count });
            return result.ToArray();
        }

        private byte[] AdaptiveDeltaModulationDecompress(byte[] data)
        {
            int offset = 1;
            int headerSize = BitConverter.ToInt32(data, offset); offset += 4;
            int audioLen = BitConverter.ToInt32(data, offset); offset += 4;
            short step = BitConverter.ToInt16(data, offset); offset += 2;
            short minStep = BitConverter.ToInt16(data, offset); offset += 2;
            short maxStep = BitConverter.ToInt16(data, offset); offset += 2;

            byte[] header = new byte[headerSize];
            Array.Copy(data, offset, header, 0, headerSize); offset += headerSize;

            int sampleCount = audioLen / 2;
            List<byte> audio = new List<byte>(audioLen);
            if (sampleCount == 0)
                return Concat(header, audio.ToArray());

            short predicted = BitConverter.ToInt16(data, offset); offset += 2;
            audio.AddRange(BitConverter.GetBytes(predicted));
            int produced = 1;

            int curStep = step;
            int previousBit = 0;

            for (int i = offset; i < data.Length && produced < sampleCount; i++)
            {
                byte byteVal = data[i];
                for (int bitPos = 7; bitPos >= 0 && produced < sampleCount; bitPos--)
                {
                    int bit = (byteVal >> bitPos) & 1;

                    if (bit == 1)
                        predicted = ClampShort(predicted + curStep);
                    else
                        predicted = ClampShort(predicted - curStep);

                    audio.AddRange(BitConverter.GetBytes(predicted));
                    produced++;

                    if (bit == previousBit)
                        curStep = Math.Min(maxStep, curStep * 2);
                    else
                        curStep = Math.Max(minStep, curStep / 2);
                    previousBit = bit;
                }
            }

            return Concat(header, audio.ToArray());
        }

        // ============================================================
        // Helper: read the real WAV header size (offset of the audio data)
        // ============================================================
        private int GetWavHeaderSize(byte[] data)
        {
            // WAV file: "RIFF" + 4 bytes size + "WAVE" + chunks
            if (data.Length < 12) return 0;

            if (data[0] != 'R' || data[1] != 'I' || data[2] != 'F' || data[3] != 'F') return 44;
            if (data[8] != 'W' || data[9] != 'A' || data[10] != 'V' || data[11] != 'E') return 44;

            int pos = 12;
            while (pos + 8 <= data.Length)
            {
                string chunkId = System.Text.Encoding.ASCII.GetString(data, pos, 4);
                int chunkSize = BitConverter.ToInt32(data, pos + 4);

                if (chunkId == "data")
                    return pos + 8; // audio data starts right after the "data" chunk header

                pos += 8 + chunkSize;
                if (chunkSize % 2 != 0) pos++; // word alignment
            }

            return 44; // fallback
        }

        private void UpdateProgress(int percent)
        {
            if (progressCompression.InvokeRequired)
            {
                progressCompression.Invoke(new Action(() => UpdateProgress(percent)));
                return;
            }
            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;
            progressCompression.Value = percent;
            lblProgressPercent.Text = percent + "%";
        }

        private void UpdateStatus(string status)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => UpdateStatus(status)));
                return;
            }
            lblStatus.Text = status;
        }

        private void UpdateCompressionChart(double value)
        {
            if (chartCompressionRatio.InvokeRequired)
            {
                chartCompressionRatio.Invoke(new Action(() => UpdateCompressionChart(value)));
                return;
            }
            double time = stopwatch.Elapsed.TotalSeconds;
            chartCompressionRatio.Series[0].Points.AddXY(time, value);
        }

        private void UpdateSpeedChart(double value)
        {
            if (chartProcessingSpeed.InvokeRequired)
            {
                chartProcessingSpeed.Invoke(new Action(() => UpdateSpeedChart(value)));
                return;
            }
            double time = stopwatch.Elapsed.TotalSeconds;
            chartProcessingSpeed.Series[0].Points.AddXY(time, value);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0) return;

            string file = files[0];
            if (!file.ToLower().EndsWith(".wav"))
            {
                MessageBox.Show("Please drop a WAV (.wav) file.");
                return;
            }

            currentFile = file;
            listBoxFiles.Items.Clear();
            listBoxFiles.Items.Add(currentFile);
            ShowAudioProperties(currentFile);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopPlayback();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

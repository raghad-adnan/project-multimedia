using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace AudioCompressionProject
{
    public partial class Form1 : Form
    {
        string currentFile = "";

        WaveOutEvent outputDevice;
        AudioFileReader audioFile;

        public Form1()
        {
            InitializeComponent();

            cmbAlgorithm.Items.Add("Nonlinear Quantization");
            cmbAlgorithm.Items.Add("DPCM");
            cmbAlgorithm.Items.Add("Delta Modulation");

            this.AllowDrop = true;

            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Audio Files|*.wav;*.mp3";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFile = ofd.FileName;

                listBoxFiles.Items.Add(currentFile);

                ShowAudioProperties(currentFile);
            }
        }

        private void ShowAudioProperties(string file)
        {
            FileInfo fi = new FileInfo(file);

            using (var reader = new AudioFileReader(file))
            {
                lblSize.Text = "Size: " + (fi.Length / 1024) + " KB";

                lblDuration.Text = "Duration: " + reader.TotalTime.ToString();

                lblSampleRate.Text = "Sample Rate: " +
                                      reader.WaveFormat.SampleRate;

                lblChannels.Text = "Channels: " +
                                    reader.WaveFormat.Channels;

                lblBitRate.Text = "Bit Rate: " +
                                  (reader.WaveFormat.BitsPerSample *
                                  reader.WaveFormat.SampleRate *
                                  reader.WaveFormat.Channels);

                lblEncoding.Text = "Encoding: " +
                                    reader.WaveFormat.Encoding.ToString();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (currentFile == "") return;

            outputDevice = new WaveOutEvent();

            audioFile = new AudioFileReader(currentFile);

            outputDevice.Init(audioFile);

            outputDevice.Play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            if (currentFile == "") return;

            if (cmbAlgorithm.SelectedItem == null)
            {
                MessageBox.Show("Select Algorithm");
                return;
            }

            string algorithm = cmbAlgorithm.SelectedItem.ToString();

            string compressedFile =
                Path.Combine(
                Path.GetDirectoryName(currentFile),
                Path.GetFileNameWithoutExtension(currentFile)
                + "_compressed.bin");

            byte[] data = File.ReadAllBytes(currentFile);

            byte[] compressedData;

            if (algorithm == "Nonlinear Quantization")
            {
                compressedData = NonlinearQuantization(data);
            }
            else if (algorithm == "DPCM")
            {
                compressedData = DPCM(data);
            }
            else
            {
                compressedData = DeltaModulation(data);
            }

            File.WriteAllBytes(compressedFile, compressedData);

            MessageBox.Show("Compression Completed");
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "BIN Files|*.bin";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string compressedFile = ofd.FileName;

                byte[] data = File.ReadAllBytes(compressedFile);

                string output =
                    Path.Combine(
                    Path.GetDirectoryName(compressedFile),
                    "decompressed.wav");

                File.WriteAllBytes(output, data);

                MessageBox.Show("Decompression Completed");
            }
        }

        private byte[] NonlinearQuantization(byte[] data)
        {
            byte[] result = new byte[data.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = data[i * 2];
            }

            return result;
        }

        private byte[] DPCM(byte[] data)
        {
            byte[] result = new byte[data.Length];

            result[0] = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] - data[i - 1]);
            }

            return result;
        }

        private byte[] DeltaModulation(byte[] data)
        {
            byte[] result = new byte[data.Length];

            result[0] = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > data[i - 1])
                    result[i] = 1;
                else
                    result[i] = 0;
            }

            return result;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files =
                (string[])e.Data.GetData(DataFormats.FileDrop);

            currentFile = files[0];

            listBoxFiles.Items.Add(currentFile);

            ShowAudioProperties(currentFile);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
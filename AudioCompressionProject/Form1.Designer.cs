using System.Windows.Forms.DataVisualization.Charting;
namespace AudioCompressionProject
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnCompress = new System.Windows.Forms.Button();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();

            this.cmbAlgorithm = new System.Windows.Forms.ComboBox();

            this.listBoxFiles = new System.Windows.Forms.ListBox();

            this.lblSize = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblSampleRate = new System.Windows.Forms.Label();
            this.lblChannels = new System.Windows.Forms.Label();
            this.lblBitRate = new System.Windows.Forms.Label();
            this.lblEncoding = new System.Windows.Forms.Label();

            // New controls
            this.grpCompressionSettings = new System.Windows.Forms.GroupBox();
            this.nudSampleRate = new System.Windows.Forms.NumericUpDown();
            this.nudQuantizationLevels = new System.Windows.Forms.NumericUpDown();
            this.lblSampleRateLabel = new System.Windows.Forms.Label();
            this.lblQuantizationLabel = new System.Windows.Forms.Label();
            this.lblAlgorithmLabel = new System.Windows.Forms.Label();

            this.progressCompression = new System.Windows.Forms.ProgressBar();
            this.lblProgressPercent = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();

            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            this.chartCompressionRatio = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartProcessingSpeed = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.grpCompressionReport = new System.Windows.Forms.GroupBox();
            this.lblReportOriginalSize = new System.Windows.Forms.Label();
            this.lblReportCompressedSize = new System.Windows.Forms.Label();
            this.lblReportCompressionRatio = new System.Windows.Forms.Label();
            this.lblReportSpaceSaved = new System.Windows.Forms.Label();
            this.lblReportProcessingTime = new System.Windows.Forms.Label();
            this.lblReportAlgorithm = new System.Windows.Forms.Label();
            this.lblReportSampleRate = new System.Windows.Forms.Label();
            this.lblReportBitRate = new System.Windows.Forms.Label();
            this.lblOriginalSizeTitle = new System.Windows.Forms.Label();
            this.lblCompressedSizeTitle = new System.Windows.Forms.Label();
            this.lblCompressionRatioTitle = new System.Windows.Forms.Label();
            this.lblSpaceSavedTitle = new System.Windows.Forms.Label();
            this.lblProcessingTimeTitle = new System.Windows.Forms.Label();
            this.lblAlgorithmTitle = new System.Windows.Forms.Label();
            this.lblSampleRateTitle = new System.Windows.Forms.Label();
            this.lblBitRateTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.nudSampleRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantizationLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompressionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProcessingSpeed)).BeginInit();
            this.grpCompressionSettings.SuspendLayout();
            this.grpCompressionReport.SuspendLayout();
            this.SuspendLayout();

            // btnBrowse
            this.btnBrowse.Location = new System.Drawing.Point(40, 100);
            this.btnBrowse.Size = new System.Drawing.Size(70, 120);
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);

            // btnPlay
            this.btnPlay.Location = new System.Drawing.Point(230, 120);
            this.btnPlay.Size = new System.Drawing.Size(120, 45);
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);

            // btnStop
            this.btnStop.Location = new System.Drawing.Point(390, 120);
            this.btnStop.Size = new System.Drawing.Size(120, 45);
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);

            // btnCompress
            this.btnCompress.Location = new System.Drawing.Point(550, 120);
            this.btnCompress.Size = new System.Drawing.Size(120, 45);
            this.btnCompress.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnCompress.ForeColor = System.Drawing.Color.White;
            this.btnCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompress.FlatAppearance.BorderSize = 0;
            this.btnCompress.Text = "Compress";
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            this.btnCompress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompress.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);

            // btnDecompress
            this.btnDecompress.Location = new System.Drawing.Point(710, 120);
            this.btnDecompress.Size = new System.Drawing.Size(120, 45);
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnDecompress.ForeColor = System.Drawing.Color.White;
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.FlatAppearance.BorderSize = 0;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            this.btnDecompress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDecompress.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(840, 120);
            this.btnCancel.Size = new System.Drawing.Size(80, 45);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(180, 50, 50);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 70, 70);
            this.btnCancel.Enabled = false;

            // btnReset
            this.btnReset.Location = new System.Drawing.Point(930, 120);
            this.btnReset.Size = new System.Drawing.Size(80, 45);
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(60, 60, 60);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(1020, 120);
            this.btnSave.Size = new System.Drawing.Size(80, 45);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(50, 150, 50);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(70, 170, 70);
            this.btnSave.Enabled = false;

            // btnExit
            this.btnExit.Location = new System.Drawing.Point(1110, 20);
            this.btnExit.Size = new System.Drawing.Size(55, 32);
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExit.Text = "?";
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(220, 50, 50);
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;

            // grpCompressionSettings
            this.grpCompressionSettings.Location = new System.Drawing.Point(40, 200);
            this.grpCompressionSettings.Size = new System.Drawing.Size(820, 120);
            this.grpCompressionSettings.Text = "Compression Settings";
            this.grpCompressionSettings.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.grpCompressionSettings.ForeColor = System.Drawing.Color.White;
            this.grpCompressionSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // lblAlgorithmLabel
            this.lblAlgorithmLabel.Location = new System.Drawing.Point(20, 30);
            this.lblAlgorithmLabel.Size = new System.Drawing.Size(100, 20);
            this.lblAlgorithmLabel.Text = "Algorithm:";
            this.lblAlgorithmLabel.ForeColor = System.Drawing.Color.White;

            // cmbAlgorithm (moved into groupbox)
            this.cmbAlgorithm.Location = new System.Drawing.Point(120, 25);
            this.cmbAlgorithm.Size = new System.Drawing.Size(200, 30);
            this.cmbAlgorithm.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.cmbAlgorithm.ForeColor = System.Drawing.Color.White;
            this.cmbAlgorithm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // lblSampleRateLabel
            this.lblSampleRateLabel.Location = new System.Drawing.Point(20, 70);
            this.lblSampleRateLabel.Size = new System.Drawing.Size(100, 20);
            this.lblSampleRateLabel.Text = "Sample Rate:";
            this.lblSampleRateLabel.ForeColor = System.Drawing.Color.White;

            // nudSampleRate
            this.nudSampleRate.Location = new System.Drawing.Point(120, 65);
            this.nudSampleRate.Size = new System.Drawing.Size(100, 25);
            this.nudSampleRate.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.nudSampleRate.ForeColor = System.Drawing.Color.White;
            this.nudSampleRate.Minimum = 8000;
            this.nudSampleRate.Maximum = 48000;
            this.nudSampleRate.Value = 44100;
            this.nudSampleRate.Increment = 1000;

            // lblQuantizationLabel
            this.lblQuantizationLabel.Location = new System.Drawing.Point(350, 30);
            this.lblQuantizationLabel.Size = new System.Drawing.Size(140, 20);
            this.lblQuantizationLabel.Text = "Quantization Levels:";
            this.lblQuantizationLabel.ForeColor = System.Drawing.Color.White;

            // nudQuantizationLevels
            this.nudQuantizationLevels.Location = new System.Drawing.Point(500, 25);
            this.nudQuantizationLevels.Size = new System.Drawing.Size(100, 25);
            this.nudQuantizationLevels.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.nudQuantizationLevels.ForeColor = System.Drawing.Color.White;
            this.nudQuantizationLevels.Minimum = 2;
            this.nudQuantizationLevels.Maximum = 256;
            this.nudQuantizationLevels.Value = 16;
            this.nudQuantizationLevels.Increment = 2;

            this.grpCompressionSettings.Controls.Add(this.lblAlgorithmLabel);
            this.grpCompressionSettings.Controls.Add(this.cmbAlgorithm);
            this.grpCompressionSettings.Controls.Add(this.lblSampleRateLabel);
            this.grpCompressionSettings.Controls.Add(this.nudSampleRate);
            this.grpCompressionSettings.Controls.Add(this.lblQuantizationLabel);
            this.grpCompressionSettings.Controls.Add(this.nudQuantizationLevels);

            // ListBox
            this.listBoxFiles.Location = new System.Drawing.Point(70, 340);
            this.listBoxFiles.Size = new System.Drawing.Size(760, 80);
            this.listBoxFiles.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.listBoxFiles.ForeColor = System.Drawing.Color.White;
            this.listBoxFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // Labels (Audio Properties)
            this.lblSize.Location = new System.Drawing.Point(70, 430);
            this.lblSize.Size = new System.Drawing.Size(400, 25);

            this.lblDuration.Location = new System.Drawing.Point(70, 455);
            this.lblDuration.Size = new System.Drawing.Size(400, 25);

            this.lblSampleRate.Location = new System.Drawing.Point(70, 480);
            this.lblSampleRate.Size = new System.Drawing.Size(400, 25);

            this.lblChannels.Location = new System.Drawing.Point(70, 505);
            this.lblChannels.Size = new System.Drawing.Size(400, 25);

            this.lblBitRate.Location = new System.Drawing.Point(70, 530);
            this.lblBitRate.Size = new System.Drawing.Size(400, 25);

            this.lblEncoding.Location = new System.Drawing.Point(70, 555);
            this.lblEncoding.Size = new System.Drawing.Size(400, 25);

            // Progress Bar
            this.progressCompression.Location = new System.Drawing.Point(70, 590);
            this.progressCompression.Size = new System.Drawing.Size(760, 25);
            this.progressCompression.BackColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.progressCompression.ForeColor = System.Drawing.Color.FromArgb(50, 150, 200);

            // lblProgressPercent
            this.lblProgressPercent.Location = new System.Drawing.Point(840, 590);
            this.lblProgressPercent.Size = new System.Drawing.Size(60, 25);
            this.lblProgressPercent.Text = "0%";
            this.lblProgressPercent.ForeColor = System.Drawing.Color.White;
            this.lblProgressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(70, 620);
            this.lblStatus.Size = new System.Drawing.Size(200, 25);
            this.lblStatus.Text = "Ready";
            this.lblStatus.ForeColor = System.Drawing.Color.White;

            // chartCompressionRatio
            this.chartCompressionRatio.Location = new System.Drawing.Point(50, 660);
            this.chartCompressionRatio.Size = new System.Drawing.Size(1050, 300);
            this.chartCompressionRatio.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.chartCompressionRatio.BorderlineColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.chartCompressionRatio.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartCompressionRatio.BorderlineWidth = 1;
            var chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea1.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(100, 100, 100);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(100, 100, 100);
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(60, 60, 60);
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(60, 60, 60);
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White; chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 14F);
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White; chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 14F);
            chartArea1.AxisX.Title = "Time (s)";
            chartArea1.AxisY.Title = "Compression Ratio %";
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White; chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.White; chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.chartCompressionRatio.ChartAreas.Add(chartArea1);
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(50, 150, 200); series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle; series1.MarkerSize = 8;
            series1.BorderWidth = 3;
            this.chartCompressionRatio.Series.Add(series1);
            this.chartCompressionRatio.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend());
            this.chartCompressionRatio.Legends[0].ForeColor = System.Drawing.Color.White;
            this.chartCompressionRatio.Titles.Add("Compression Ratio");
            this.chartCompressionRatio.Titles[0].ForeColor = System.Drawing.Color.White; this.chartCompressionRatio.Titles[0].Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);

            // chartProcessingSpeed
            this.chartProcessingSpeed.Location = new System.Drawing.Point(50, 980);
            this.chartProcessingSpeed.Size = new System.Drawing.Size(1050, 300);
            this.chartProcessingSpeed.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.chartProcessingSpeed.BorderlineColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.chartProcessingSpeed.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartProcessingSpeed.BorderlineWidth = 1;
            var chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea2.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            chartArea2.AxisX.LineColor = System.Drawing.Color.FromArgb(100, 100, 100);
            chartArea2.AxisY.LineColor = System.Drawing.Color.FromArgb(100, 100, 100);
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(60, 60, 60);
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(60, 60, 60);
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White; chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 14F);
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White; chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 14F);
            chartArea2.AxisX.Title = "Time (s)";
            chartArea2.AxisY.Title = "Speed (MB/s)";
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.White; chartArea2.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.White; chartArea2.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.chartProcessingSpeed.ChartAreas.Add(chartArea2);
            var series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.FromArgb(150, 200, 50); series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle; series2.MarkerSize = 8;
            series2.BorderWidth = 3;
            this.chartProcessingSpeed.Series.Add(series2);
            this.chartProcessingSpeed.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend());
            this.chartProcessingSpeed.Legends[0].ForeColor = System.Drawing.Color.White;
            this.chartProcessingSpeed.Titles.Add("Processing Speed");
            this.chartProcessingSpeed.Titles[0].ForeColor = System.Drawing.Color.White; this.chartProcessingSpeed.Titles[0].Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);

            // grpCompressionReport
            this.grpCompressionReport.Location = new System.Drawing.Point(50, 1300);
            this.grpCompressionReport.Size = new System.Drawing.Size(1050, 150);
            this.grpCompressionReport.Text = "Compression Report";
            this.grpCompressionReport.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.grpCompressionReport.ForeColor = System.Drawing.Color.White;
            this.grpCompressionReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // Report labels - Titles
            this.lblOriginalSizeTitle.Location = new System.Drawing.Point(20, 30);
            this.lblOriginalSizeTitle.Size = new System.Drawing.Size(150, 20);
            this.lblOriginalSizeTitle.Text = "Original File Size:";
            this.lblOriginalSizeTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblOriginalSizeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblCompressedSizeTitle.Location = new System.Drawing.Point(20, 60);
            this.lblCompressedSizeTitle.Size = new System.Drawing.Size(150, 20);
            this.lblCompressedSizeTitle.Text = "Compressed File Size:";
            this.lblCompressedSizeTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblCompressedSizeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblCompressionRatioTitle.Location = new System.Drawing.Point(20, 90);
            this.lblCompressionRatioTitle.Size = new System.Drawing.Size(150, 20);
            this.lblCompressionRatioTitle.Text = "Compression Ratio:";
            this.lblCompressionRatioTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblCompressionRatioTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblSpaceSavedTitle.Location = new System.Drawing.Point(20, 120);
            this.lblSpaceSavedTitle.Size = new System.Drawing.Size(150, 20);
            this.lblSpaceSavedTitle.Text = "Space Saved:";
            this.lblSpaceSavedTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblSpaceSavedTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblProcessingTimeTitle.Location = new System.Drawing.Point(20, 150);
            this.lblProcessingTimeTitle.Size = new System.Drawing.Size(150, 20);
            this.lblProcessingTimeTitle.Text = "Processing Time:";
            this.lblProcessingTimeTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblProcessingTimeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblAlgorithmTitle.Location = new System.Drawing.Point(420, 30);
            this.lblAlgorithmTitle.Size = new System.Drawing.Size(150, 20);
            this.lblAlgorithmTitle.Text = "Algorithm Used:";
            this.lblAlgorithmTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblAlgorithmTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblSampleRateTitle.Location = new System.Drawing.Point(420, 60);
            this.lblSampleRateTitle.Size = new System.Drawing.Size(150, 20);
            this.lblSampleRateTitle.Text = "Sample Rate:";
            this.lblSampleRateTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblSampleRateTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblBitRateTitle.Location = new System.Drawing.Point(420, 90);
            this.lblBitRateTitle.Size = new System.Drawing.Size(150, 20);
            this.lblBitRateTitle.Text = "Bit Rate:";
            this.lblBitRateTitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.lblBitRateTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // Report labels - Values
            this.lblReportOriginalSize.Location = new System.Drawing.Point(180, 30);
            this.lblReportOriginalSize.Size = new System.Drawing.Size(250, 25);
            this.lblReportOriginalSize.Text = "-";
            this.lblReportOriginalSize.ForeColor = System.Drawing.Color.White;
            this.lblReportOriginalSize.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.lblReportCompressedSize.Location = new System.Drawing.Point(180, 60);
            this.lblReportCompressedSize.Size = new System.Drawing.Size(250, 25);
            this.lblReportCompressedSize.Text = "-";
            this.lblReportCompressedSize.ForeColor = System.Drawing.Color.White;
            this.lblReportCompressedSize.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.lblReportCompressionRatio.Location = new System.Drawing.Point(180, 90);
            this.lblReportCompressionRatio.Size = new System.Drawing.Size(250, 25);
            this.lblReportCompressionRatio.Text = "-";
            this.lblReportCompressionRatio.ForeColor = System.Drawing.Color.White;
            this.lblReportCompressionRatio.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.lblReportSpaceSaved.Location = new System.Drawing.Point(180, 120);
            this.lblReportSpaceSaved.Size = new System.Drawing.Size(250, 25);
            this.lblReportSpaceSaved.Text = "-";
            this.lblReportSpaceSaved.ForeColor = System.Drawing.Color.White;
            this.lblReportSpaceSaved.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.lblReportProcessingTime.Location = new System.Drawing.Point(180, 150);
            this.lblReportProcessingTime.Size = new System.Drawing.Size(250, 25);
            this.lblReportProcessingTime.Text = "-";
            this.lblReportProcessingTime.ForeColor = System.Drawing.Color.White;
            this.lblReportProcessingTime.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.lblReportAlgorithm.Location = new System.Drawing.Point(580, 30);
            this.lblReportAlgorithm.Size = new System.Drawing.Size(250, 25);
            this.lblReportAlgorithm.Text = "-";
            this.lblReportAlgorithm.ForeColor = System.Drawing.Color.White;
            this.lblReportAlgorithm.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.lblReportSampleRate.Location = new System.Drawing.Point(580, 60);
            this.lblReportSampleRate.Size = new System.Drawing.Size(250, 25);
            this.lblReportSampleRate.Text = "-";
            this.lblReportSampleRate.ForeColor = System.Drawing.Color.White;
            this.lblReportSampleRate.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.lblReportBitRate.Location = new System.Drawing.Point(580, 90);
            this.lblReportBitRate.Size = new System.Drawing.Size(250, 25);
            this.lblReportBitRate.Text = "-";
            this.lblReportBitRate.ForeColor = System.Drawing.Color.White;
            this.lblReportBitRate.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);

            this.grpCompressionReport.Controls.Add(this.lblOriginalSizeTitle);
            this.grpCompressionReport.Controls.Add(this.lblCompressedSizeTitle);
            this.grpCompressionReport.Controls.Add(this.lblCompressionRatioTitle);
            this.grpCompressionReport.Controls.Add(this.lblSpaceSavedTitle);
            this.grpCompressionReport.Controls.Add(this.lblProcessingTimeTitle);
            this.grpCompressionReport.Controls.Add(this.lblAlgorithmTitle);
            this.grpCompressionReport.Controls.Add(this.lblSampleRateTitle);
            this.grpCompressionReport.Controls.Add(this.lblBitRateTitle);
            this.grpCompressionReport.Controls.Add(this.lblReportOriginalSize);
            this.grpCompressionReport.Controls.Add(this.lblReportCompressedSize);
            this.grpCompressionReport.Controls.Add(this.lblReportCompressionRatio);
            this.grpCompressionReport.Controls.Add(this.lblReportSpaceSaved);
            this.grpCompressionReport.Controls.Add(this.lblReportProcessingTime);
            this.grpCompressionReport.Controls.Add(this.lblReportAlgorithm);
            this.grpCompressionReport.Controls.Add(this.lblReportSampleRate);
            this.grpCompressionReport.Controls.Add(this.lblReportBitRate);

            // Form
            this.ClientSize = new System.Drawing.Size(1150, 1050); this.AutoScroll = true; this.AutoScrollMinSize = new System.Drawing.Size(1150, 1550);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);

            this.Controls.Add(this.grpCompressionSettings);

            this.Controls.Add(this.listBoxFiles);

            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblSampleRate);
            this.Controls.Add(this.lblChannels);
            this.Controls.Add(this.lblBitRate);
            this.Controls.Add(this.lblEncoding);

            this.Controls.Add(this.progressCompression);
            this.Controls.Add(this.lblProgressPercent);
            this.Controls.Add(this.lblStatus);

            this.Controls.Add(this.chartCompressionRatio);
            this.Controls.Add(this.chartProcessingSpeed);

            this.Controls.Add(this.grpCompressionReport);

            this.Text = "Audio Compression System";
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.ForeColor = System.Drawing.Color.White;

            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            this.MaximizeBox = true;

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            System.Windows.Forms.Label titleLabel = new System.Windows.Forms.Label();
            titleLabel.Text = "Audio Compression System";
            titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F);
            titleLabel.ForeColor = System.Drawing.Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new System.Drawing.Point(425, 40);
            this.Controls.Add(titleLabel);

            ((System.ComponentModel.ISupportInitialize)(this.nudSampleRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantizationLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompressionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProcessingSpeed)).EndInit();
            this.grpCompressionSettings.ResumeLayout(false);
            this.grpCompressionReport.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;

        private System.Windows.Forms.ComboBox cmbAlgorithm;

        private System.Windows.Forms.ListBox listBoxFiles;

        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblSampleRate;
        private System.Windows.Forms.Label lblChannels;
        private System.Windows.Forms.Label lblBitRate;
        private System.Windows.Forms.Label lblEncoding;

        private System.Windows.Forms.GroupBox grpCompressionSettings;
        private System.Windows.Forms.NumericUpDown nudSampleRate;
        private System.Windows.Forms.NumericUpDown nudQuantizationLevels;
        private System.Windows.Forms.Label lblSampleRateLabel;
        private System.Windows.Forms.Label lblQuantizationLabel;
        private System.Windows.Forms.Label lblAlgorithmLabel;

        private System.Windows.Forms.ProgressBar progressCompression;
        private System.Windows.Forms.Label lblProgressPercent;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.GroupBox grpCompressionReport;
        private System.Windows.Forms.Label lblReportOriginalSize;
        private System.Windows.Forms.Label lblReportCompressedSize;
        private System.Windows.Forms.Label lblReportCompressionRatio;
        private System.Windows.Forms.Label lblReportSpaceSaved;
        private System.Windows.Forms.Label lblReportProcessingTime;
        private System.Windows.Forms.Label lblReportAlgorithm;
        private System.Windows.Forms.Label lblReportSampleRate;
        private System.Windows.Forms.Label lblReportBitRate;
        private System.Windows.Forms.Label lblOriginalSizeTitle;
        private System.Windows.Forms.Label lblCompressedSizeTitle;
        private System.Windows.Forms.Label lblCompressionRatioTitle;
        private System.Windows.Forms.Label lblSpaceSavedTitle;
        private System.Windows.Forms.Label lblProcessingTimeTitle;
        private System.Windows.Forms.Label lblAlgorithmTitle;
        private System.Windows.Forms.Label lblSampleRateTitle;
        private System.Windows.Forms.Label lblBitRateTitle;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompressionRatio;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProcessingSpeed;
    }
}
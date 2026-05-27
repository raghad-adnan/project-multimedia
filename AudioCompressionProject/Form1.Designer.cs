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
            this.btnBrowse.FlatAppearance.MouseOverBackColor =
System.Drawing.Color.FromArgb(60, 60, 60);
            // btnPlay
            this.btnPlay.Location = new System.Drawing.Point(230,120);
            this.btnPlay.Size = new System.Drawing.Size(120, 45);
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlay.FlatAppearance.MouseOverBackColor =
System.Drawing.Color.FromArgb(60, 60, 60);
            // btnStop
            this.btnStop.Location = new System.Drawing.Point(390, 120);
            this.btnStop.Size = new System.Drawing.Size(120, 45);
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStop.FlatAppearance.MouseOverBackColor =
System.Drawing.Color.FromArgb(60, 60, 60);

            // btnCompress
            this.btnCompress.Location = new System.Drawing.Point(550, 120);
            this.btnCompress.Size = new System.Drawing.Size(120, 45);
            this.btnCompress.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompress.FlatAppearance.BorderSize = 0;
            this.btnCompress.Text = "Compress";
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            this.btnCompress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompress.FlatAppearance.MouseOverBackColor =
System.Drawing.Color.FromArgb(60, 60, 60);

            // btnDecompress
            this.btnDecompress.Location = new System.Drawing.Point(710, 120);
            this.btnDecompress.Size = new System.Drawing.Size(120, 45);
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(40, 40, 40); this.btnDecompress.ForeColor = System.Drawing.Color.White;
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.FlatAppearance.BorderSize = 0;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            this.btnDecompress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDecompress.FlatAppearance.MouseOverBackColor =
System.Drawing.Color.FromArgb(60, 60, 60);

            // btnExit

            this.btnExit.Location = new System.Drawing.Point(820, 20);

            this.btnExit.Size = new System.Drawing.Size(55, 32);

            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnExit.Text = "✕";

            this.btnExit.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);

            this.btnExit.FlatAppearance.MouseOverBackColor =
            System.Drawing.Color.FromArgb(220, 50, 50);

            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;

            // ComboBox
            this.cmbAlgorithm.Location = new System.Drawing.Point(325, 210); this.cmbAlgorithm.Size = new System.Drawing.Size(260, 35);
            this.cmbAlgorithm.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.cmbAlgorithm.ForeColor = System.Drawing.Color.White;

            this.cmbAlgorithm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;


            // ListBox
            this.listBoxFiles.Location = new System.Drawing.Point(70, 280); this.listBoxFiles.Size = new System.Drawing.Size(760, 100);
            this.listBoxFiles.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);
            this.listBoxFiles.ForeColor = System.Drawing.Color.White;

            this.listBoxFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // Labels
            this.lblSize.Location = new System.Drawing.Point(70, 450); this.lblSize.Size = new System.Drawing.Size(400, 25);

            this.lblDuration.Location = new System.Drawing.Point(70, 480); this.lblDuration.Size = new System.Drawing.Size(400, 25);

            this.lblSampleRate.Location = new System.Drawing.Point(70, 510); this.lblSampleRate.Size = new System.Drawing.Size(400, 25);

            this.lblChannels.Location = new System.Drawing.Point(70, 540); this.lblChannels.Size = new System.Drawing.Size(400, 25);

            this.lblBitRate.Location = new System.Drawing.Point(70, 570); this.lblBitRate.Size = new System.Drawing.Size(400, 25);

            this.lblEncoding.Location = new System.Drawing.Point(70, 600); this.lblEncoding.Size = new System.Drawing.Size(400, 25);

            // Form
this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.btnExit);

            this.Controls.Add(this.cmbAlgorithm);

            this.Controls.Add(this.listBoxFiles);

            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblSampleRate);
            this.Controls.Add(this.lblChannels);
            this.Controls.Add(this.lblBitRate);
            this.Controls.Add(this.lblEncoding);

            this.Text = "Audio Compression System";
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.ForeColor = System.Drawing.Color.White;

            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            System.Windows.Forms.Label titleLabel = new System.Windows.Forms.Label();

            titleLabel.Text = "Audio Compression System";

            titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 24F);
            titleLabel.ForeColor = System.Drawing.Color.White;

            titleLabel.AutoSize = true;

            titleLabel.Location = new System.Drawing.Point(250, 40);
            this.Controls.Add(titleLabel);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnExit;

        private System.Windows.Forms.ComboBox cmbAlgorithm;

        private System.Windows.Forms.ListBox listBoxFiles;

        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblSampleRate;
        private System.Windows.Forms.Label lblChannels;
        private System.Windows.Forms.Label lblBitRate;
        private System.Windows.Forms.Label lblEncoding;
    }
}
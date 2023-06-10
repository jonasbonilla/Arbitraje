namespace Arbitraje
{
    partial class Odds
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbxSports = new ComboBox();
            lblSports = new Label();
            lblGroup = new Label();
            cbxGroup = new ComboBox();
            pnlLoading = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pnlArbitraje = new Panel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolProgressBar = new ToolStripProgressBar();
            cbxBettingMarket = new ComboBox();
            chkBookmarkers = new CheckedListBox();
            dtpDateFrom = new DateTimePicker();
            lblFrom = new Label();
            dtpDateTo = new DateTimePicker();
            lblDate = new Label();
            btnConsultar = new Button();
            txtResponse = new RichTextBox();
            lblBettingMarket = new Label();
            lblBookmarkers = new Label();
            lblRegiones = new Label();
            pnlLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlArbitraje.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // cbxSports
            // 
            cbxSports.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSports.FlatStyle = FlatStyle.Flat;
            cbxSports.FormattingEnabled = true;
            cbxSports.Location = new Point(360, 99);
            cbxSports.Name = "cbxSports";
            cbxSports.Size = new Size(252, 23);
            cbxSports.TabIndex = 2;
            // 
            // lblSports
            // 
            lblSports.AutoSize = true;
            lblSports.Location = new Point(305, 102);
            lblSports.Name = "lblSports";
            lblSports.Size = new Size(35, 15);
            lblSports.TabIndex = 3;
            lblSports.Text = "LIGA:";
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(305, 73);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(49, 15);
            lblGroup.TabIndex = 5;
            lblGroup.Text = "GRUPO:";
            // 
            // cbxGroup
            // 
            cbxGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxGroup.FlatStyle = FlatStyle.Flat;
            cbxGroup.FormattingEnabled = true;
            cbxGroup.Location = new Point(360, 70);
            cbxGroup.Name = "cbxGroup";
            cbxGroup.Size = new Size(252, 23);
            cbxGroup.TabIndex = 4;
            cbxGroup.SelectedValueChanged += cbxGroup_SelectedValueChanged;
            // 
            // pnlLoading
            // 
            pnlLoading.Controls.Add(pictureBox1);
            pnlLoading.Controls.Add(label1);
            pnlLoading.Location = new Point(0, 0);
            pnlLoading.Name = "pnlLoading";
            pnlLoading.Size = new Size(624, 440);
            pnlLoading.TabIndex = 6;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.Image = Properties.Resources.loading;
            pictureBox1.Location = new Point(253, 156);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(128, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(275, 335);
            label1.Name = "label1";
            label1.Size = new Size(88, 20);
            label1.TabIndex = 0;
            label1.Text = "Cargando...";
            // 
            // pnlArbitraje
            // 
            pnlArbitraje.Controls.Add(statusStrip1);
            pnlArbitraje.Controls.Add(cbxGroup);
            pnlArbitraje.Controls.Add(cbxBettingMarket);
            pnlArbitraje.Controls.Add(cbxSports);
            pnlArbitraje.Controls.Add(chkBookmarkers);
            pnlArbitraje.Controls.Add(dtpDateFrom);
            pnlArbitraje.Controls.Add(lblFrom);
            pnlArbitraje.Controls.Add(dtpDateTo);
            pnlArbitraje.Controls.Add(lblDate);
            pnlArbitraje.Controls.Add(btnConsultar);
            pnlArbitraje.Controls.Add(txtResponse);
            pnlArbitraje.Controls.Add(lblBettingMarket);
            pnlArbitraje.Controls.Add(lblGroup);
            pnlArbitraje.Controls.Add(lblSports);
            pnlArbitraje.Controls.Add(lblBookmarkers);
            pnlArbitraje.Controls.Add(lblRegiones);
            pnlArbitraje.Location = new Point(0, 0);
            pnlArbitraje.Name = "pnlArbitraje";
            pnlArbitraje.Size = new Size(624, 440);
            pnlArbitraje.TabIndex = 7;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolProgressBar });
            statusStrip1.Location = new Point(0, 418);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(624, 22);
            statusStrip1.TabIndex = 20;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(76, 17);
            toolStripStatusLabel1.Text = "Arbitraje v1.0";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(431, 17);
            toolStripStatusLabel2.Spring = true;
            toolStripStatusLabel2.Text = "By Jonas";
            // 
            // toolProgressBar
            // 
            toolProgressBar.Name = "toolProgressBar";
            toolProgressBar.Size = new Size(100, 16);
            toolProgressBar.Step = 1;
            toolProgressBar.Style = ProgressBarStyle.Continuous;
            // 
            // cbxBettingMarket
            // 
            cbxBettingMarket.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBettingMarket.Enabled = false;
            cbxBettingMarket.FlatStyle = FlatStyle.Flat;
            cbxBettingMarket.FormattingEnabled = true;
            cbxBettingMarket.Location = new Point(360, 128);
            cbxBettingMarket.Name = "cbxBettingMarket";
            cbxBettingMarket.Size = new Size(252, 23);
            cbxBettingMarket.TabIndex = 9;
            // 
            // chkBookmarkers
            // 
            chkBookmarkers.BorderStyle = BorderStyle.FixedSingle;
            chkBookmarkers.FormattingEnabled = true;
            chkBookmarkers.Location = new Point(0, 18);
            chkBookmarkers.Name = "chkBookmarkers";
            chkBookmarkers.ScrollAlwaysVisible = true;
            chkBookmarkers.Size = new Size(299, 398);
            chkBookmarkers.TabIndex = 18;
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Enabled = false;
            dtpDateFrom.Location = new Point(341, 12);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(271, 23);
            dtpDateFrom.TabIndex = 15;
            dtpDateFrom.Value = new DateTime(2023, 6, 5, 0, 0, 0, 0);
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(305, 47);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(30, 15);
            lblFrom.TabIndex = 14;
            lblFrom.Text = "DEL:";
            // 
            // dtpDateTo
            // 
            dtpDateTo.Location = new Point(341, 41);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(271, 23);
            dtpDateTo.TabIndex = 13;
            dtpDateTo.Value = new DateTime(2023, 6, 5, 23, 59, 59, 0);
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(305, 18);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(24, 15);
            lblDate.TabIndex = 12;
            lblDate.Text = "AL:";
            // 
            // btnConsultar
            // 
            btnConsultar.AutoSize = true;
            btnConsultar.Location = new Point(305, 157);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(307, 25);
            btnConsultar.TabIndex = 11;
            btnConsultar.Text = "CONSULTAR";
            btnConsultar.UseVisualStyleBackColor = true;
            btnConsultar.Click += button1_Click;
            // 
            // txtResponse
            // 
            txtResponse.BorderStyle = BorderStyle.None;
            txtResponse.Location = new Point(305, 188);
            txtResponse.Name = "txtResponse";
            txtResponse.Size = new Size(316, 228);
            txtResponse.TabIndex = 10;
            txtResponse.Text = "";
            // 
            // lblBettingMarket
            // 
            lblBettingMarket.AutoSize = true;
            lblBettingMarket.Location = new Point(305, 131);
            lblBettingMarket.Name = "lblBettingMarket";
            lblBettingMarket.Size = new Size(55, 15);
            lblBettingMarket.TabIndex = 8;
            lblBettingMarket.Text = "MARKET:";
            // 
            // lblBookmarkers
            // 
            lblBookmarkers.Location = new Point(3, 3);
            lblBookmarkers.Name = "lblBookmarkers";
            lblBookmarkers.Size = new Size(84, 16);
            lblBookmarkers.TabIndex = 6;
            lblBookmarkers.Text = "PLATAFORMA:";
            // 
            // lblRegiones
            // 
            lblRegiones.AutoSize = true;
            lblRegiones.Location = new Point(164, 4);
            lblRegiones.Name = "lblRegiones";
            lblRegiones.Size = new Size(0, 15);
            lblRegiones.TabIndex = 19;
            // 
            // Odds
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(624, 441);
            Controls.Add(pnlArbitraje);
            Controls.Add(pnlLoading);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Odds";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "APUESTAS DEPORTIVAS - ARBITRAJE v1.0";
            Load += Form1_Load;
            pnlLoading.ResumeLayout(false);
            pnlLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlArbitraje.ResumeLayout(false);
            pnlArbitraje.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ComboBox cbxSports;
        private Label lblSports;
        private Label lblGroup;
        private ComboBox cbxGroup;
        private Panel pnlLoading;
        private Label label1;
        private PictureBox pictureBox1;
        private Panel pnlArbitraje;
        private Label lblBookmarkers;
        private ComboBox cbxBettingMarket;
        private Label lblBettingMarket;
        private Button btnConsultar;
        private RichTextBox txtResponse;
        private DateTimePicker dtpDateTo;
        private Label lblDate;
        private DateTimePicker dtpDateFrom;
        private Label lblFrom;
        private CheckedListBox chkBookmarkers;
        private Label lblRegiones;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripProgressBar toolProgressBar;
    }
}
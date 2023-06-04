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
            btnConsultar = new Button();
            txtResponse = new RichTextBox();
            cbxBettingMarket = new ComboBox();
            lblBettingMarket = new Label();
            cbxBookmarkers = new ComboBox();
            lblBookmarkers = new Label();
            pnlLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlArbitraje.SuspendLayout();
            SuspendLayout();
            // 
            // cbxSports
            // 
            cbxSports.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSports.FlatStyle = FlatStyle.Flat;
            cbxSports.FormattingEnabled = true;
            cbxSports.Location = new Point(325, 13);
            cbxSports.Name = "cbxSports";
            cbxSports.Size = new Size(287, 23);
            cbxSports.TabIndex = 2;
            // 
            // lblSports
            // 
            lblSports.AutoSize = true;
            lblSports.Location = new Point(284, 16);
            lblSports.Name = "lblSports";
            lblSports.Size = new Size(35, 15);
            lblSports.TabIndex = 3;
            lblSports.Text = "Sport";
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(5, 16);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(40, 15);
            lblGroup.TabIndex = 5;
            lblGroup.Text = "Group";
            // 
            // cbxGroup
            // 
            cbxGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxGroup.FlatStyle = FlatStyle.Flat;
            cbxGroup.FormattingEnabled = true;
            cbxGroup.Location = new Point(45, 13);
            cbxGroup.Name = "cbxGroup";
            cbxGroup.Size = new Size(200, 23);
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
            pnlArbitraje.Controls.Add(btnConsultar);
            pnlArbitraje.Controls.Add(txtResponse);
            pnlArbitraje.Controls.Add(cbxBettingMarket);
            pnlArbitraje.Controls.Add(lblBettingMarket);
            pnlArbitraje.Controls.Add(cbxBookmarkers);
            pnlArbitraje.Controls.Add(lblBookmarkers);
            pnlArbitraje.Controls.Add(lblGroup);
            pnlArbitraje.Controls.Add(cbxSports);
            pnlArbitraje.Controls.Add(cbxGroup);
            pnlArbitraje.Controls.Add(lblSports);
            pnlArbitraje.Location = new Point(0, 0);
            pnlArbitraje.Name = "pnlArbitraje";
            pnlArbitraje.Size = new Size(624, 440);
            pnlArbitraje.TabIndex = 7;
            // 
            // btnConsultar
            // 
            btnConsultar.AutoSize = true;
            btnConsultar.Location = new Point(12, 404);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(600, 25);
            btnConsultar.TabIndex = 11;
            btnConsultar.Text = "CONSULTAR";
            btnConsultar.UseVisualStyleBackColor = true;
            btnConsultar.Click += button1_Click;
            // 
            // txtResponse
            // 
            txtResponse.BorderStyle = BorderStyle.None;
            txtResponse.Location = new Point(12, 85);
            txtResponse.Name = "txtResponse";
            txtResponse.Size = new Size(600, 313);
            txtResponse.TabIndex = 10;
            txtResponse.Text = "";
            // 
            // cbxBettingMarket
            // 
            cbxBettingMarket.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBettingMarket.Enabled = false;
            cbxBettingMarket.FlatStyle = FlatStyle.Flat;
            cbxBettingMarket.FormattingEnabled = true;
            cbxBettingMarket.Location = new Point(380, 56);
            cbxBettingMarket.Name = "cbxBettingMarket";
            cbxBettingMarket.Size = new Size(232, 23);
            cbxBettingMarket.TabIndex = 9;
            // 
            // lblBettingMarket
            // 
            lblBettingMarket.AutoSize = true;
            lblBettingMarket.Location = new Point(284, 59);
            lblBettingMarket.Name = "lblBettingMarket";
            lblBettingMarket.Size = new Size(90, 15);
            lblBettingMarket.TabIndex = 8;
            lblBettingMarket.Text = "Betting Markets";
            // 
            // cbxBookmarkers
            // 
            cbxBookmarkers.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBookmarkers.FlatStyle = FlatStyle.Flat;
            cbxBookmarkers.FormattingEnabled = true;
            cbxBookmarkers.Location = new Point(82, 56);
            cbxBookmarkers.Name = "cbxBookmarkers";
            cbxBookmarkers.Size = new Size(163, 23);
            cbxBookmarkers.TabIndex = 7;
            // 
            // lblBookmarkers
            // 
            lblBookmarkers.AutoSize = true;
            lblBookmarkers.Location = new Point(5, 59);
            lblBookmarkers.Name = "lblBookmarkers";
            lblBookmarkers.Size = new Size(71, 15);
            lblBookmarkers.TabIndex = 6;
            lblBookmarkers.Text = "Bookmarker";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(624, 441);
            Controls.Add(pnlArbitraje);
            Controls.Add(pnlLoading);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "The Odds Api";
            Load += Form1_Load;
            pnlLoading.ResumeLayout(false);
            pnlLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlArbitraje.ResumeLayout(false);
            pnlArbitraje.PerformLayout();
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
        private ComboBox cbxBookmarkers;
        private ComboBox cbxBettingMarket;
        private Label lblBettingMarket;
        private Button btnConsultar;
        private RichTextBox txtResponse;
    }
}
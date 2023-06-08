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
            chcbxGroup = new CheckComboBox.CheckComboBox();
            dtpDateFrom = new DateTimePicker();
            lblFrom = new Label();
            dtpDateTo = new DateTimePicker();
            lblDate = new Label();
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
            cbxSports.Location = new Point(345, 13);
            cbxSports.Name = "cbxSports";
            cbxSports.Size = new Size(267, 23);
            cbxSports.TabIndex = 2;
            // 
            // lblSports
            // 
            lblSports.AutoSize = true;
            lblSports.Location = new Point(304, 16);
            lblSports.Name = "lblSports";
            lblSports.Size = new Size(35, 15);
            lblSports.TabIndex = 3;
            lblSports.Text = "LIGA:";
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(5, 16);
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
            cbxGroup.Location = new Point(60, 13);
            cbxGroup.Name = "cbxGroup";
            cbxGroup.Size = new Size(227, 23);
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
            pnlArbitraje.Controls.Add(chcbxGroup);
            pnlArbitraje.Controls.Add(dtpDateFrom);
            pnlArbitraje.Controls.Add(lblFrom);
            pnlArbitraje.Controls.Add(dtpDateTo);
            pnlArbitraje.Controls.Add(lblDate);
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
            // chcbxGroup
            // 
            chcbxGroup.DrawMode = DrawMode.OwnerDrawFixed;
            chcbxGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            chcbxGroup.FlatStyle = FlatStyle.Flat;
            chcbxGroup.FormattingEnabled = true;
            chcbxGroup.Location = new Point(95, 128);
            chcbxGroup.Name = "chcbxGroup";
            chcbxGroup.Size = new Size(121, 24);
            chcbxGroup.TabIndex = 16;
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Enabled = false;
            dtpDateFrom.Location = new Point(38, 71);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(249, 23);
            dtpDateFrom.TabIndex = 15;
            dtpDateFrom.Value = new DateTime(2023, 6, 5, 0, 0, 0, 0);
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(5, 75);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(27, 15);
            lblFrom.TabIndex = 14;
            lblFrom.Text = "Del:";
            // 
            // dtpDateTo
            // 
            dtpDateTo.Location = new Point(331, 71);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(281, 23);
            dtpDateTo.TabIndex = 13;
            dtpDateTo.Value = new DateTime(2023, 6, 5, 23, 59, 59, 0);
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(304, 75);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(21, 15);
            lblDate.TabIndex = 12;
            lblDate.Text = "Al:";
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
            txtResponse.Location = new Point(12, 213);
            txtResponse.Name = "txtResponse";
            txtResponse.Size = new Size(600, 171);
            txtResponse.TabIndex = 10;
            txtResponse.Text = "";
            // 
            // cbxBettingMarket
            // 
            cbxBettingMarket.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBettingMarket.Enabled = false;
            cbxBettingMarket.FlatStyle = FlatStyle.Flat;
            cbxBettingMarket.FormattingEnabled = true;
            cbxBettingMarket.Location = new Point(365, 42);
            cbxBettingMarket.Name = "cbxBettingMarket";
            cbxBettingMarket.Size = new Size(247, 23);
            cbxBettingMarket.TabIndex = 9;
            // 
            // lblBettingMarket
            // 
            lblBettingMarket.AutoSize = true;
            lblBettingMarket.Location = new Point(304, 45);
            lblBettingMarket.Name = "lblBettingMarket";
            lblBettingMarket.Size = new Size(55, 15);
            lblBettingMarket.TabIndex = 8;
            lblBettingMarket.Text = "MARKET:";
            // 
            // cbxBookmarkers
            // 
            cbxBookmarkers.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxBookmarkers.FlatStyle = FlatStyle.Flat;
            cbxBookmarkers.FormattingEnabled = true;
            cbxBookmarkers.Location = new Point(95, 42);
            cbxBookmarkers.Name = "cbxBookmarkers";
            cbxBookmarkers.Size = new Size(192, 23);
            cbxBookmarkers.TabIndex = 7;
            // 
            // lblBookmarkers
            // 
            lblBookmarkers.AutoSize = true;
            lblBookmarkers.Location = new Point(5, 45);
            lblBookmarkers.Name = "lblBookmarkers";
            lblBookmarkers.Size = new Size(84, 15);
            lblBookmarkers.TabIndex = 6;
            lblBookmarkers.Text = "PLATAFORMA:";
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
        private DateTimePicker dtpDateTo;
        private Label lblDate;
        private DateTimePicker dtpDateFrom;
        private Label lblFrom;
        private CheckComboBox.CheckComboBox chcbxGroup;
    }
}
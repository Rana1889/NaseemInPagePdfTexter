namespace UnicodeToInpage
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label6;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pb1 = new System.Windows.Forms.ProgressBar();
            this.rtbxPDFText = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numCounter = new System.Windows.Forms.NumericUpDown();
            this.btnTextWithFont = new System.Windows.Forms.Button();
            this.lblHelp1 = new System.Windows.Forms.Label();
            this.btnInpageToUni = new System.Windows.Forms.Button();
            this.btnUniToInpageExcel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkSkipEnglishWords = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSpecialMethod = new System.Windows.Forms.RadioButton();
            this.rbEncodingDefault = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkLineFeed = new System.Windows.Forms.CheckBox();
            this.chkSwapText = new System.Windows.Forms.CheckBox();
            this.lblPDF = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlPDF = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblHelp = new System.Windows.Forms.Label();
            this.pbxSkipEnglishWords = new System.Windows.Forms.PictureBox();
            this.pbxHelpShowTextLineByLine = new System.Windows.Forms.PictureBox();
            this.pbxHelpRepairText = new System.Windows.Forms.PictureBox();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numCounter)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlPDF.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkipEnglishWords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHelpShowTextLineByLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHelpRepairText)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            label6.BackColor = System.Drawing.Color.LightSteelBlue;
            label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = System.Drawing.Color.Black;
            label6.Location = new System.Drawing.Point(0, 684);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(991, 20);
            label6.TabIndex = 31;
            label6.Text = "Developed by Rana";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pb1
            // 
            this.pb1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb1.Location = new System.Drawing.Point(0, 175);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(348, 12);
            this.pb1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb1.TabIndex = 7;
            // 
            // rtbxPDFText
            // 
            this.rtbxPDFText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbxPDFText.BackColor = System.Drawing.Color.Ivory;
            this.rtbxPDFText.Location = new System.Drawing.Point(363, 535);
            this.rtbxPDFText.Name = "rtbxPDFText";
            this.rtbxPDFText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rtbxPDFText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbxPDFText.Size = new System.Drawing.Size(616, 141);
            this.rtbxPDFText.TabIndex = 17;
            this.rtbxPDFText.Text = "";
            this.rtbxPDFText.TextChanged += new System.EventHandler(this.rtbxPDFText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "PDF File:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Page No.";
            // 
            // numCounter
            // 
            this.numCounter.Location = new System.Drawing.Point(64, 115);
            this.numCounter.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numCounter.Name = "numCounter";
            this.numCounter.Size = new System.Drawing.Size(94, 20);
            this.numCounter.TabIndex = 22;
            // 
            // btnTextWithFont
            // 
            this.btnTextWithFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTextWithFont.ForeColor = System.Drawing.Color.Green;
            this.btnTextWithFont.Location = new System.Drawing.Point(222, 104);
            this.btnTextWithFont.Name = "btnTextWithFont";
            this.btnTextWithFont.Size = new System.Drawing.Size(113, 36);
            this.btnTextWithFont.TabIndex = 21;
            this.btnTextWithFont.Text = "Extract Text";
            this.btnTextWithFont.UseVisualStyleBackColor = true;
            this.btnTextWithFont.Click += new System.EventHandler(this.btnTextWithFont_Click);
            // 
            // lblHelp1
            // 
            this.lblHelp1.ForeColor = System.Drawing.Color.Teal;
            this.lblHelp1.Location = new System.Drawing.Point(162, 29);
            this.lblHelp1.Name = "lblHelp1";
            this.lblHelp1.Size = new System.Drawing.Size(181, 142);
            this.lblHelp1.TabIndex = 13;
            this.lblHelp1.Text = " ان پیج یا ایم ایس ورڈ سے  متن کاپی کریں اور کہیں پیسٹ کرنے سے پہلے مطلوبہ بٹن دب" +
    "ا دیں۔";
            this.lblHelp1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnInpageToUni
            // 
            this.btnInpageToUni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInpageToUni.ForeColor = System.Drawing.Color.Green;
            this.btnInpageToUni.Location = new System.Drawing.Point(11, 41);
            this.btnInpageToUni.Name = "btnInpageToUni";
            this.btnInpageToUni.Size = new System.Drawing.Size(147, 41);
            this.btnInpageToUni.TabIndex = 12;
            this.btnInpageToUni.Text = "Inpage To Unicode";
            this.btnInpageToUni.UseVisualStyleBackColor = true;
            this.btnInpageToUni.Click += new System.EventHandler(this.btnInpageToUni_Click);
            // 
            // btnUniToInpageExcel
            // 
            this.btnUniToInpageExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUniToInpageExcel.Location = new System.Drawing.Point(11, 108);
            this.btnUniToInpageExcel.Name = "btnUniToInpageExcel";
            this.btnUniToInpageExcel.Size = new System.Drawing.Size(147, 38);
            this.btnUniToInpageExcel.TabIndex = 11;
            this.btnUniToInpageExcel.Text = "Unicode To Inpage";
            this.btnUniToInpageExcel.UseVisualStyleBackColor = true;
            this.btnUniToInpageExcel.Click += new System.EventHandler(this.btnUniToInpageExcel_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pbxSkipEnglishWords);
            this.panel2.Controls.Add(this.chkSkipEnglishWords);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnBrowse);
            this.panel2.Controls.Add(this.pbxHelpShowTextLineByLine);
            this.panel2.Controls.Add(this.pbxHelpRepairText);
            this.panel2.Controls.Add(this.chkLineFeed);
            this.panel2.Controls.Add(this.chkSwapText);
            this.panel2.Controls.Add(this.lblPDF);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnTextWithFont);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.numCounter);
            this.panel2.Location = new System.Drawing.Point(7, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 310);
            this.panel2.TabIndex = 28;
            // 
            // chkSkipEnglishWords
            // 
            this.chkSkipEnglishWords.AutoSize = true;
            this.chkSkipEnglishWords.Checked = true;
            this.chkSkipEnglishWords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSkipEnglishWords.Location = new System.Drawing.Point(9, 233);
            this.chkSkipEnglishWords.Name = "chkSkipEnglishWords";
            this.chkSkipEnglishWords.Size = new System.Drawing.Size(118, 17);
            this.chkSkipEnglishWords.TabIndex = 37;
            this.chkSkipEnglishWords.Text = "Skip English Words";
            this.chkSkipEnglishWords.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSpecialMethod);
            this.groupBox1.Controls.Add(this.rbEncodingDefault);
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(197, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 73);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extraction Method";
            this.groupBox1.Visible = false;
            // 
            // rbSpecialMethod
            // 
            this.rbSpecialMethod.AutoSize = true;
            this.rbSpecialMethod.ForeColor = System.Drawing.Color.Black;
            this.rbSpecialMethod.Location = new System.Drawing.Point(13, 44);
            this.rbSpecialMethod.Name = "rbSpecialMethod";
            this.rbSpecialMethod.Size = new System.Drawing.Size(99, 17);
            this.rbSpecialMethod.TabIndex = 37;
            this.rbSpecialMethod.TabStop = true;
            this.rbSpecialMethod.Text = "Special Method";
            this.rbSpecialMethod.UseVisualStyleBackColor = true;
            // 
            // rbEncodingDefault
            // 
            this.rbEncodingDefault.AutoSize = true;
            this.rbEncodingDefault.Checked = true;
            this.rbEncodingDefault.ForeColor = System.Drawing.Color.Black;
            this.rbEncodingDefault.Location = new System.Drawing.Point(13, 21);
            this.rbEncodingDefault.Name = "rbEncodingDefault";
            this.rbEncodingDefault.Size = new System.Drawing.Size(98, 17);
            this.rbEncodingDefault.TabIndex = 36;
            this.rbEncodingDefault.TabStop = true;
            this.rbEncodingDefault.Text = "Default Method";
            this.rbEncodingDefault.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(222, 56);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(113, 33);
            this.btnBrowse.TabIndex = 33;
            this.btnBrowse.Text = "Select PDF";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkLineFeed
            // 
            this.chkLineFeed.AutoSize = true;
            this.chkLineFeed.Checked = true;
            this.chkLineFeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLineFeed.Location = new System.Drawing.Point(9, 210);
            this.chkLineFeed.Name = "chkLineFeed";
            this.chkLineFeed.Size = new System.Drawing.Size(138, 17);
            this.chkLineFeed.TabIndex = 30;
            this.chkLineFeed.Text = "Show Text Line By Line";
            this.chkLineFeed.UseVisualStyleBackColor = true;
            this.chkLineFeed.CheckedChanged += new System.EventHandler(this.chkLineFeed_CheckedChanged);
            // 
            // chkSwapText
            // 
            this.chkSwapText.AutoSize = true;
            this.chkSwapText.Location = new System.Drawing.Point(9, 187);
            this.chkSwapText.Name = "chkSwapText";
            this.chkSwapText.Size = new System.Drawing.Size(81, 17);
            this.chkSwapText.TabIndex = 28;
            this.chkSwapText.Text = "Repair Text";
            this.chkSwapText.UseVisualStyleBackColor = true;
            // 
            // lblPDF
            // 
            this.lblPDF.AutoSize = true;
            this.lblPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPDF.ForeColor = System.Drawing.Color.Navy;
            this.lblPDF.Location = new System.Drawing.Point(62, 65);
            this.lblPDF.Name = "lblPDF";
            this.lblPDF.Size = new System.Drawing.Size(0, 15);
            this.lblPDF.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.CadetBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(-1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(350, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "PDF Text Extraction Tools";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnInpageToUni);
            this.panel4.Controls.Add(this.btnUniToInpageExcel);
            this.panel4.Controls.Add(this.lblHelp1);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.pb1);
            this.panel4.Location = new System.Drawing.Point(7, 340);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(350, 189);
            this.panel4.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(-1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(350, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Unicode Conversion Tools";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPDF
            // 
            this.pnlPDF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPDF.BackColor = System.Drawing.Color.Olive;
            this.pnlPDF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPDF.Controls.Add(this.webBrowser1);
            this.pnlPDF.Controls.Add(this.label5);
            this.pnlPDF.Location = new System.Drawing.Point(363, 27);
            this.pnlPDF.Name = "pnlPDF";
            this.pnlPDF.Size = new System.Drawing.Size(616, 502);
            this.pnlPDF.TabIndex = 30;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(614, 475);
            this.webBrowser1.TabIndex = 32;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.CadetBlue;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(614, 25);
            this.label5.TabIndex = 28;
            this.label5.Text = "PDF View";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(991, 24);
            this.menuStrip1.TabIndex = 33;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuidToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userGuidToolStripMenuItem
            // 
            this.userGuidToolStripMenuItem.Name = "userGuidToolStripMenuItem";
            this.userGuidToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.userGuidToolStripMenuItem.Text = "User Guid";
            this.userGuidToolStripMenuItem.Click += new System.EventHandler(this.userGuidToolStripMenuItem_Click);
            // 
            // lblHelp
            // 
            this.lblHelp.BackColor = System.Drawing.SystemColors.Info;
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelp.ForeColor = System.Drawing.Color.Purple;
            this.lblHelp.Location = new System.Drawing.Point(11, 526);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblHelp.Size = new System.Drawing.Size(254, 11);
            this.lblHelp.TabIndex = 34;
            this.lblHelp.Text = resources.GetString("lblHelp.Text");
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHelp.Visible = false;
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            this.lblHelp.MouseLeave += new System.EventHandler(this.lblHelp_MouseLeave);
            // 
            // pbxSkipEnglishWords
            // 
            this.pbxSkipEnglishWords.BackColor = System.Drawing.Color.Transparent;
            this.pbxSkipEnglishWords.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbxSkipEnglishWords.Image = global::UnicodeToInpage.Properties.Resources.Help01;
            this.pbxSkipEnglishWords.Location = new System.Drawing.Point(133, 230);
            this.pbxSkipEnglishWords.Name = "pbxSkipEnglishWords";
            this.pbxSkipEnglishWords.Size = new System.Drawing.Size(25, 20);
            this.pbxSkipEnglishWords.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxSkipEnglishWords.TabIndex = 38;
            this.pbxSkipEnglishWords.TabStop = false;
            this.pbxSkipEnglishWords.Click += new System.EventHandler(this.pbxHelp_Click);
            // 
            // pbxHelpShowTextLineByLine
            // 
            this.pbxHelpShowTextLineByLine.BackColor = System.Drawing.Color.Transparent;
            this.pbxHelpShowTextLineByLine.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbxHelpShowTextLineByLine.Image = global::UnicodeToInpage.Properties.Resources.Help01;
            this.pbxHelpShowTextLineByLine.Location = new System.Drawing.Point(153, 206);
            this.pbxHelpShowTextLineByLine.Name = "pbxHelpShowTextLineByLine";
            this.pbxHelpShowTextLineByLine.Size = new System.Drawing.Size(25, 20);
            this.pbxHelpShowTextLineByLine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxHelpShowTextLineByLine.TabIndex = 32;
            this.pbxHelpShowTextLineByLine.TabStop = false;
            this.pbxHelpShowTextLineByLine.Click += new System.EventHandler(this.pbxHelp_Click);
            // 
            // pbxHelpRepairText
            // 
            this.pbxHelpRepairText.BackColor = System.Drawing.Color.Transparent;
            this.pbxHelpRepairText.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbxHelpRepairText.Image = global::UnicodeToInpage.Properties.Resources.Help01;
            this.pbxHelpRepairText.Location = new System.Drawing.Point(95, 183);
            this.pbxHelpRepairText.Name = "pbxHelpRepairText";
            this.pbxHelpRepairText.Size = new System.Drawing.Size(25, 20);
            this.pbxHelpRepairText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxHelpRepairText.TabIndex = 31;
            this.pbxHelpRepairText.TabStop = false;
            this.pbxHelpRepairText.Click += new System.EventHandler(this.pbxHelp_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 704);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(label6);
            this.Controls.Add(this.pnlPDF);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.rtbxPDFText);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naseem Inpage PDF Text Extraction Software  - Version 1.3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCounter)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.pnlPDF.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkipEnglishWords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHelpShowTextLineByLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHelpRepairText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb1;
        private System.Windows.Forms.RichTextBox rtbxPDFText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCounter;
        private System.Windows.Forms.Button btnTextWithFont;
        private System.Windows.Forms.Button btnInpageToUni;
        private System.Windows.Forms.Button btnUniToInpageExcel;
        private System.Windows.Forms.Label lblHelp1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPDF;
        private System.Windows.Forms.Panel pnlPDF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkSwapText;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuidToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkLineFeed;
        private System.Windows.Forms.PictureBox pbxHelpRepairText;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.PictureBox pbxHelpShowTextLineByLine;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSpecialMethod;
        private System.Windows.Forms.RadioButton rbEncodingDefault;
        private System.Windows.Forms.PictureBox pbxSkipEnglishWords;
        private System.Windows.Forms.CheckBox chkSkipEnglishWords;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}


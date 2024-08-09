namespace ScanReader
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            txtDest = new TextBox();
            txtTessData = new TextBox();
            cmdDest = new Button();
            cmdTessData = new Button();
            cmdStart = new Button();
            lstAllFiles = new ListBox();
            lstFindedFiles = new ListBox();
            mnuFinded = new ContextMenuStrip(components);
            mnuCopy = new ToolStripMenuItem();
            mnuMove = new ToolStripMenuItem();
            folderBrowserDialog1 = new FolderBrowserDialog();
            label3 = new Label();
            txtForSearch = new TextBox();
            txtOCR = new TextBox();
            cboLang = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            mnuFinded.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 5);
            label1.Name = "label1";
            label1.Size = new Size(147, 15);
            label1.TabIndex = 0;
            label1.Text = "Каталог со скриншотами";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 60);
            label2.Name = "label2";
            label2.Size = new Size(187, 15);
            label2.TabIndex = 1;
            label2.Text = "Каталог со словарями (TessData)";
            // 
            // txtDest
            // 
            txtDest.Location = new Point(16, 23);
            txtDest.Name = "txtDest";
            txtDest.Size = new Size(671, 23);
            txtDest.TabIndex = 2;
            // 
            // txtTessData
            // 
            txtTessData.Location = new Point(16, 78);
            txtTessData.Name = "txtTessData";
            txtTessData.Size = new Size(671, 23);
            txtTessData.TabIndex = 3;
            // 
            // cmdDest
            // 
            cmdDest.Location = new Point(694, 23);
            cmdDest.Name = "cmdDest";
            cmdDest.Size = new Size(75, 23);
            cmdDest.TabIndex = 4;
            cmdDest.Text = "Выбрать";
            cmdDest.UseVisualStyleBackColor = true;
            cmdDest.Click += cmdDest_Click;
            // 
            // cmdTessData
            // 
            cmdTessData.Location = new Point(693, 78);
            cmdTessData.Name = "cmdTessData";
            cmdTessData.Size = new Size(75, 23);
            cmdTessData.TabIndex = 5;
            cmdTessData.Text = "Выбрать";
            cmdTessData.UseVisualStyleBackColor = true;
            cmdTessData.Click += cmdTessData_Click;
            // 
            // cmdStart
            // 
            cmdStart.Location = new Point(317, 167);
            cmdStart.Name = "cmdStart";
            cmdStart.Size = new Size(127, 38);
            cmdStart.TabIndex = 6;
            cmdStart.Text = "Start";
            cmdStart.UseVisualStyleBackColor = true;
            cmdStart.Click += cmdStart_Click;
            // 
            // lstAllFiles
            // 
            lstAllFiles.FormattingEnabled = true;
            lstAllFiles.ItemHeight = 15;
            lstAllFiles.Location = new Point(16, 238);
            lstAllFiles.Name = "lstAllFiles";
            lstAllFiles.Size = new Size(361, 274);
            lstAllFiles.TabIndex = 7;
            // 
            // lstFindedFiles
            // 
            lstFindedFiles.ContextMenuStrip = mnuFinded;
            lstFindedFiles.FormattingEnabled = true;
            lstFindedFiles.ItemHeight = 15;
            lstFindedFiles.Location = new Point(391, 238);
            lstFindedFiles.Name = "lstFindedFiles";
            lstFindedFiles.Size = new Size(377, 274);
            lstFindedFiles.TabIndex = 8;
            // 
            // mnuFinded
            // 
            mnuFinded.Items.AddRange(new ToolStripItem[] { mnuCopy, mnuMove });
            mnuFinded.Name = "contextMenuStrip1";
            mnuFinded.Size = new Size(242, 48);
            mnuFinded.Text = "Скопировать файлы в каталог";
            // 
            // mnuCopy
            // 
            mnuCopy.Name = "mnuCopy";
            mnuCopy.Size = new Size(241, 22);
            mnuCopy.Text = "Скопировать файлы в каталог";
            mnuCopy.Click += mnuCopy_Click;
            // 
            // mnuMove
            // 
            mnuMove.Name = "mnuMove";
            mnuMove.Size = new Size(241, 22);
            mnuMove.Text = "Переместить файлы в каталог";
            mnuMove.Click += mnuMove_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 121);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 9;
            label3.Text = "Текст для поиска";
            // 
            // txtForSearch
            // 
            txtForSearch.Location = new Point(123, 118);
            txtForSearch.Name = "txtForSearch";
            txtForSearch.Size = new Size(199, 23);
            txtForSearch.TabIndex = 10;
            // 
            // txtOCR
            // 
            txtOCR.Location = new Point(16, 527);
            txtOCR.Multiline = true;
            txtOCR.Name = "txtOCR";
            txtOCR.ReadOnly = true;
            txtOCR.ScrollBars = ScrollBars.Both;
            txtOCR.Size = new Size(753, 147);
            txtOCR.TabIndex = 11;
            // 
            // cboLang
            // 
            cboLang.FormattingEnabled = true;
            cboLang.Location = new Point(383, 118);
            cboLang.Name = "cboLang";
            cboLang.Size = new Size(121, 23);
            cboLang.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(343, 121);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 13;
            label4.Text = "Язык";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 220);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 14;
            label5.Text = "Все файлы";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(393, 220);
            label6.Name = "label6";
            label6.Size = new Size(111, 15);
            label6.TabIndex = 15;
            label6.Text = "Найденные файлы";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1 });
            statusStrip1.Location = new Point(0, 677);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(780, 22);
            statusStrip1.TabIndex = 17;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(300, 16);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 699);
            Controls.Add(statusStrip1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(cboLang);
            Controls.Add(txtOCR);
            Controls.Add(txtForSearch);
            Controls.Add(label3);
            Controls.Add(lstFindedFiles);
            Controls.Add(lstAllFiles);
            Controls.Add(cmdStart);
            Controls.Add(cmdTessData);
            Controls.Add(cmdDest);
            Controls.Add(txtTessData);
            Controls.Add(txtDest);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Поиск текста в скриншотах";
            mnuFinded.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtDest;
        private TextBox txtTessData;
        private Button cmdDest;
        private Button cmdTessData;
        private Button cmdStart;
        private ListBox lstAllFiles;
        private ListBox lstFindedFiles;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label3;
        private TextBox txtForSearch;
        private TextBox txtOCR;
        private ComboBox cboLang;
        private Label label4;
        private Label label5;
        private Label label6;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ContextMenuStrip mnuFinded;
        private ToolStripMenuItem mnuCopy;
        private ToolStripMenuItem mnuMove;
    }
}

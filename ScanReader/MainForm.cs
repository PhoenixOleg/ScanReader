using System.Linq;
using System.Linq.Expressions;
using Tesseract;

namespace ScanReader
{
    public partial class MainForm : Form
    {
        private string imagePath;
        private string tessDataPath;

        public MainForm()
        {
            InitializeComponent();

            cboLang.Items.Add("Eng");
            cboLang.Items.Add("Rus");
            cboLang.Items.Add("Eng+Rus");

            cboLang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLang.SelectedIndex = 0;

            txtDest.Text = "C:\\C#_ActualProjects\\ScanReader\\Dest";
            txtTessData.Text = "C:\\C#_ActualProjects\\ScanReader\\TessData";
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            lstAllFiles.Items.Clear();
            lstFindedFiles.Items.Clear();

            imagePath = txtDest.Text;
            tessDataPath = txtTessData.Text;
            txtOCR.Clear();

            toolStripProgressBar1.Value = 0;

            string lang = cboLang.SelectedItem.ToString();

            if (!Directory.Exists(imagePath))
            {
                MessageBox.Show("Каталог " + imagePath + " не существует", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(tessDataPath))
            {
                MessageBox.Show("Каталог " + tessDataPath + " не существует", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var files = Directory.GetFiles(imagePath);
            if (files.Length > 0)
            {
                lstAllFiles.Items.AddRange(files);
            }
            else
            {
                MessageBox.Show("Отсутствуют файлы в каталоге " + imagePath, "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Application.DoEvents();
            // Создание нового движка Tesseract OCR
            try
            {
                using (var engine = new TesseractEngine(tessDataPath, lang, EngineMode.TesseractAndLstm))
                {
                    int idx = 0;
                    foreach (var file in files)
                    {
                        // Загрузка изображения
                        using (var img = Pix.LoadFromFile(file))
                        {
                            // Обработка изображения и распознавание текста
                            using (var page = engine.Process(img))
                            {
                                string text = page.GetText();
                                if (text.Contains(txtForSearch.Text))
                                {
                                    lstFindedFiles.Items.Add(file);

                                    txtOCR.AppendText(file + (Environment.NewLine));
                                    txtOCR.AppendText("Распознанный текст:" + (Environment.NewLine) + text + (Environment.NewLine) + (Environment.NewLine));
                                }
                            }
                            idx++;
                            toolStripProgressBar1.Value = (int)Math.Round((double)idx / files.Length * 100, MidpointRounding.AwayFromZero);
                        }
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка с кодом " + ex.HResult + Environment.NewLine + ex.Message, "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("Completed!", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmdDest_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDest.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void cmdTessData_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.OkRequiresInteraction = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTessData.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            string destFolder;

            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (!(folderBrowserDialog1.ShowDialog() == DialogResult.Cancel))
            {
                destFolder = folderBrowserDialog1.SelectedPath;

                try
                {
                    if (Directory.Exists(destFolder))
                    {

                        foreach (var file in lstFindedFiles.Items)
                        {
                            if (File.Exists(file.ToString()))
                            {
                                string fileName = new FileInfo(file.ToString()).Name;
                                File.Copy(file.ToString(), Path.Combine(destFolder, fileName));
                            }
                        }

                        MessageBox.Show("Copying is complete!", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Каталог " + destFolder + " не существует", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка с кодом " + ex.HResult + Environment.NewLine + ex.Message, "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void mnuMove_Click(object sender, EventArgs e)
        {
            string destFolder;

            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (!(folderBrowserDialog1.ShowDialog() == DialogResult.Cancel))
            {
                destFolder = folderBrowserDialog1.SelectedPath;

                try
                {
                    if (Directory.Exists(destFolder))
                    {
                        foreach (var file in lstFindedFiles.Items)
                        {
                            if (File.Exists(file.ToString()))
                            {
                                string fileName = new FileInfo(file.ToString()).Name;
                                File.Move(file.ToString(), Path.Combine(destFolder, fileName), true);
                            }
                        }
                        MessageBox.Show("The move is complete!", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Каталог " + destFolder + " не существует", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка с кодом " + ex.HResult + Environment.NewLine + ex.Message, "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

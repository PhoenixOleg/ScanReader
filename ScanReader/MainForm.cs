using System.Linq;
using System.Linq.Expressions;
using Tesseract;
using ScanReader.Static;
using static System.Net.Mime.MediaTypeNames;

namespace ScanReader
{
    public partial class MainForm : Form
    {
        private string imagePath = string.Empty;
        private string tessDataPath = string.Empty;
        private List<ImageFile> recognizedImages = [];

        public MainForm()
        {
            InitializeComponent();

            ConfigControls.Lang(cboLang);

            ConfigControls.CreateTextFiles(cboCreateTextFiles);

            //txtDest.Text = "C:\\C#_ActualProjects\\Images\\Source";
            //txtTessData.Text = "C:\\C#_ActualProjects\\ScanReader\\TessData";
        }

        private void CmdStart_Click(object sender, EventArgs e)
        {
            string? lang = "eng"; //Default value
            string searchText;

            lstAllFiles.Items.Clear();
            lstFindedFiles.Items.Clear();

            imagePath = txtDest.Text;
            tessDataPath = txtTessData.Text;
            txtOCR.Clear();

            recognizedImages.Clear();

            searchText = txtForSearch.Text;

            toolStripProgressBar1.Value = 0;

            if (cboLang.SelectedItem?.ToString() is not null)
            {
                lang = cboLang.SelectedItem.ToString();
            }

            if (!FileSystemActions.ValidateFolders(imagePath, tessDataPath))
            {
                return;
            }

            List<ImageFile> imageFilesList = FileSystemActions.GetFiles(imagePath);

            foreach (ImageFile imageFile in imageFilesList)
            {
                lstAllFiles.Items.Add(imageFile.FileName);
            }

            System.Windows.Forms.Application.DoEvents();
            TextSearcher(lang, searchText, imageFilesList);

            MessageBox.Show("Completed!", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TextSearcher(string? lang, string searchText, List<ImageFile> imageFilesList)
        {
            bool textIsFind;

            //Creating a new engine Tesseract OCR
            try
            {
                using (var engine = new TesseractEngine(tessDataPath, lang, EngineMode.TesseractAndLstm))
                {
                    int idx = 0;
                    foreach (ImageFile imageFile in imageFilesList)
                    {
                        txtOCR.AppendText(imageFile.FullName + " is processing..." + Environment.NewLine);
                        textIsFind = false;
                        if (FileSystemActions.FileIsImage(imageFile.FullName))
                        {
                            //Uploading an image
                            using (var img = Pix.LoadFromFile(imageFile.FullName))
                            {
                                //Image processing and text recognition
                                using (var page = engine.Process(img))
                                {
                                    string textFromImage = page.GetText();
                                    string sourceText;

                                    if (chkCaseinsensitive.Checked)
                                    {
                                        sourceText = page.GetText().ToUpper();
                                        searchText = searchText.ToUpper();
                                    }
                                    else
                                    {
                                        sourceText = textFromImage;
                                    }

                                    if (sourceText.Contains(searchText))
                                    {
                                        textIsFind = true;
                                        lstFindedFiles.Items.Add(imageFile.FullName);
                                        recognizedImages.Add(imageFile);

                                        txtOCR.AppendText("Recognized text:" + Environment.NewLine + textFromImage + Environment.NewLine + Environment.NewLine);
                                    }
                                    else
                                    {
                                        txtOCR.AppendText("The text you were looking for was not found." + Environment.NewLine + Environment.NewLine);
                                    }

                                    switch (cboCreateTextFiles.SelectedIndex)
                                    {
                                        case 0: //Do not create
                                            break;

                                        case 1: //Only for found images
                                            if (textIsFind)
                                            {
                                                FileSystemActions.WriteTextFile(imageFile.FilePath, imageFile.FileName, textFromImage);
                                            }
                                            break;

                                        case 2: //For all images
                                            FileSystemActions.WriteTextFile(imageFile.FilePath, imageFile.FileName, textFromImage);
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            txtOCR.AppendText("File is not an image or does not exist." + Environment.NewLine + Environment.NewLine);
                        }

                        idx++;
                        toolStripProgressBar1.Value = (int)Math.Round((double)idx / imageFilesList.Count * 100, MidpointRounding.AwayFromZero);

                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred with the code " + ex.HResult + Environment.NewLine + ex.Message, "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmdDest_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDest.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void CmdTessData_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                OkRequiresInteraction = true
            };

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTessData.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void MnuCopy_Click(object sender, EventArgs e)
        {
            string destFolder;

            if (recognizedImages.Count == 0)
            {
                MessageBox.Show("No recognized images", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            folderBrowserDialog1 = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            if (!(folderBrowserDialog1.ShowDialog() == DialogResult.Cancel))
            {
                destFolder = folderBrowserDialog1.SelectedPath;

                try
                {
                    if (Directory.Exists(destFolder))
                    {

                        foreach (ImageFile file in recognizedImages)
                        {
                            if (File.Exists(file.FullName))
                            {
                                string newFilePlace = Path.Combine(destFolder, file.FileName);
                                if (!File.Exists(newFilePlace))
                                {
                                    File.Copy(file.FullName, Path.Combine(destFolder, file.FileName), false);
                                    if (File.Exists(file.FullName + ".txt"))
                                    {
                                        File.Copy(file.FullName + ".txt", Path.Combine(destFolder, file.FileName + ".txt"), true);
                                    }
                                }
                                else
                                {
                                    if (MessageBox.Show("The file " + file.FullName + " is exists. Replace it?", "ScanReader", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        File.Copy(file.FullName, Path.Combine(destFolder, file.FileName), true);
                                        if (File.Exists(file.FullName + ".txt"))
                                        {
                                            File.Copy(file.FullName + ".txt", Path.Combine(destFolder, file.FileName + ".txt"), true);
                                        }
                                    }
                                }
                            }
                        }

                        MessageBox.Show("Copying is complete!", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("The " + destFolder + " folder does not exist", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred with the code " + ex.HResult + Environment.NewLine + ex.Message, "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MnuMove_Click(object sender, EventArgs e)
        {
            string destFolder;

            if (recognizedImages.Count == 0)
            {
                MessageBox.Show("No recognized images", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            folderBrowserDialog1 = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            if (!(folderBrowserDialog1.ShowDialog() == DialogResult.Cancel))
            {
                destFolder = folderBrowserDialog1.SelectedPath;

                try
                {
                    if (Directory.Exists(destFolder))
                    {
                        foreach (ImageFile file in recognizedImages)
                        {
                            if (File.Exists(file.FullName))
                            {
                                string newFilePlace = Path.Combine(destFolder, file.FileName);
                                if (!File.Exists(newFilePlace))
                                {
                                    File.Move(file.FullName, Path.Combine(destFolder, file.FileName), false);
                                    if (File.Exists(file.FullName + ".txt"))
                                    {
                                        File.Move(file.FullName + ".txt", Path.Combine(destFolder, file.FileName + ".txt"), true);
                                    }
                                }
                                else 
                                {
                                    if (MessageBox.Show("The file " + file.FullName + " is exists. Replace it?", "ScanReader", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        File.Move(file.FullName, Path.Combine(destFolder, file.FileName), true);
                                        if (File.Exists(file.FullName + ".txt"))
                                        {
                                            File.Move(file.FullName + ".txt", Path.Combine(destFolder, file.FileName + ".txt"), true);
                                        }
                                    }
                                }
                            }
                        }
                        MessageBox.Show("The move is complete!", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The " + destFolder + " folder does not exist", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred with the code " + ex.HResult + Environment.NewLine + ex.Message, "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

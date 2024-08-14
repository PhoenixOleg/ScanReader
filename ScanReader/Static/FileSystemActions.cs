using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanReader.Static;

internal static class FileSystemActions
{
    internal static Boolean ValidateFolders(string imagePath, string tessDataPath)
    {
        if (string.IsNullOrEmpty(imagePath))
        {
            MessageBox.Show("The folder with screenshots is not set", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false; 
        }

        if (!Directory.Exists(imagePath))
        {
            MessageBox.Show("The " + imagePath + " folder does not exist", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (string.IsNullOrEmpty(tessDataPath))
        {
            MessageBox.Show("Folder with dictionaries (TessData) is not set", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (!Directory.Exists(tessDataPath))
        {
            MessageBox.Show("The " + tessDataPath + " folder does not exist", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }

    internal static List<ImageFile> GetFiles(string imagePath)
    {
        List<ImageFile> filesList = [];

        string[] files = Directory.GetFiles(imagePath);
        if (files.Length > 0)
        {
            for (int i = 0; i <= files.Length - 1; i++)
            {
                FileInfo fileInfo = new(files[i]);

                ImageFile imageFile = new()
                {
                    FullName = fileInfo.FullName,
                    FileName = fileInfo.Name,
                    FilePath = fileInfo.DirectoryName
                };

                filesList.Add(imageFile);
            }
        }
        else
        {
            MessageBox.Show("There are no files in the " + imagePath + " folder", "ScanReader", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        return filesList;
    }

    internal static Boolean FileIsImage(string imagePath)
    {
        try
        {
            if (File.Exists(imagePath))
            {
                var res = Image.FromFile(imagePath);
            }
            else
            {
                return false;
            }
        }
        catch         
        {
            return false;
        }

        return true;
    }

    internal static void WriteTextFile(string filePath, string fileName, string text)
    {
        var a = File.CreateText(Path.Combine(filePath, fileName + ".txt"));
        a.WriteLine(text);
        a.Close();
    }
}

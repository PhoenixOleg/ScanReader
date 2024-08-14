using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanReader.Static;

internal static class ConfigControls
{
    internal static void Lang(ComboBox comboBox)
    {
        comboBox.Items.Add("Eng");
        comboBox.Items.Add("Rus");
        comboBox.Items.Add("Eng+Rus");

        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox.SelectedIndex = 0;
    }

    internal static void CreateTextFiles(ComboBox comboBox)
    {
        comboBox.Items.Add("Do not create");
        comboBox.Items.Add("Only for found images");
        comboBox.Items.Add("For all images");

        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox.SelectedIndex = 0;
    }
}

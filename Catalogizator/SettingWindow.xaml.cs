using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Catalogizator
{
    /// <summary>
    /// Логика взаимодействия для SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        readonly string settings;
        public SettingWindow(string settings)
        {
            InitializeComponent();
            btnOk.IsEnabled = false;
            this.settings = settings;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if(Directory.Exists(path.Text))
            {
                SaveDirectory();
                this.DialogResult = true;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path.Text);
                    SaveDirectory();
                }
                catch (Exception)
                {
                    MessageBox.Show("Неправильно указан путь к папке.\nПопытайтесь снова");
                    path.Text = "";
                    btnOk.IsEnabled = false;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (path.Text.Trim() != "")
                btnOk.IsEnabled = true;
        }

        private void openDir_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                path.Text = dialog.FileName;
            }
        }

        void SaveDirectory()
        {
            using (StreamWriter sw = File.CreateText(settings))
            {
                sw.Write(path.Text);
                sw.Close();
            }
            File.SetAttributes(settings, FileAttributes.Hidden);
        }
    }
}

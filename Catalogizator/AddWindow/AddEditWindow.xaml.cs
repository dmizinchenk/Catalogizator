using Catalogizator.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public AddEditWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void bookIsbn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (bookIsbn.Text.Length >= 13 && bookIsbn.SelectedText.Length == 0)
                e.Handled = true;
            else
                TextBox_PreviewTextInput(sender, e);
        }

        private void bookYear_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (((TextBox)sender).Text.Length >= 4 && ((TextBox)sender).SelectedText.Length == 0)
                e.Handled = true;
            else
                TextBox_PreviewTextInput(sender, e);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            foreach (var element in panel.Children)
            {
                if(element is TextBox tempText)
                {
                    if (tempText.Text.Trim() == "")
                    {
                        flag = false;
                        break;
                    }
                }
                if (element is ListBox tempList)
                {
                    if(tempList.Items.Count == 0)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if(flag)
            {
                this.DialogResult = true;
            }
            else
                MessageBox.Show("Заполните все поля или отмените операцию");
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Catalogizator.Library;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Catalogizator.LibraryManager;
using Microsoft.Win32;
using Catalogizator.AddWindow;

namespace Catalogizator
{
    internal class ViewModel : INotifyPropertyChanged
    {
        const string SETFILE = @".\setting.txt";
        string pathToLibrary = "";

        ICommand add;
        ICommand edit;
        //ICommand remove;

        public ICommand AddCommand
        {

            get; set;
            //private set
            //{

                //add = new AddCommand(
                //        () =>
                //        {
                //            if (CheckPathToCatalog())
                //            {
                //                //добавляем
                //                OpenFileDialog openFileDialog = new OpenFileDialog();
                //                openFileDialog.Filter = "Текстовые файлы (*.txt, *.doc, *.docx, *.djvu, *.pdf, *.fb2)" +
                //                "|*.txt;*.doc;*.docx;*.djvu;*.pdf;*.fb2";
                //                openFileDialog.RestoreDirectory = true;

                //                if (openFileDialog.ShowDialog() == true)
                //                {
                //                    AddViewModel model = new AddViewModel();
                //                    if (model.IsCompleteAdded)
                //                    {
                //                        //перемещаем файл в наше хранилище
                //                        string fileName = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf("\\"));
                //                        File.Move(openFileDialog.FileName, pathToLibrary + fileName);
                //                    }
                //                    else
                //                    {
                //                        MessageBox.Show("Операция отменена");
                //                    }
                //                }
                //                ShowAll();
                //            }
                //            else
                //            {
                //                MessageBox.Show("Нет каталога для хранения книг или файл настроек поврежден." +
                //                    "\nНевозможно произвести операцию.");
                //            }
                //        });
            //}
            //get => add;
        }
        public ICommand EditCommand
        {
            set; get;
            //{
            //    edit = new AddCommand(
            //            () =>
            //            {
            //                AddViewModel model = new AddViewModel(selectedBook.Id);
            //                ShowAll();
            //            },
            //            () => selectedBook != null
            //            );
            //}
            //get => edit;
        }
        public ICommand RemoveCommand
        {
            get; set;
            //set
            //{
            //remove = new AddCommand(
            //        () =>
            //        {
            //            using (LibraryContext context = new LibraryContext())
            //            {
            //                context.Books.Remove(selectedBook);
            //                context.SaveChanges();
            //            }
            //            ShowAll();
            //        },
            //        () => selectedBook != null
            //        );
            //}
            //get => remove;
            }

        Book selectedBook = null!;
        public Book Selected {
            get => selectedBook;
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public ObservableCollection<Book> MyBooks { get; set; } = new ObservableCollection<Book>();


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ViewModel()
        {       
            ShowAll();
            AddCommand = new AddCommand(
                        () =>
                        {
                            //if (CheckPathToCatalog())
                            //{
                                ////добавляем

                                //OpenFileDialog openFileDialog = new OpenFileDialog();
                                //openFileDialog.Filter = "Текстовые файлы (*.txt, *.doc, *.docx, *.djvu, *.pdf, *.fb2)" +
                                //"|*.txt;*.doc;*.docx;*.djvu;*.pdf;*.fb2";
                                //openFileDialog.RestoreDirectory = true;

                                //if (openFileDialog.ShowDialog() == true)
                                //{
                                    AddViewModel model = new AddViewModel();
                                //    if (model.IsCompleteAdded)
                                //    {
                                //        //перемещаем файл в наше хранилище
                                //        string fileName = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf("\\"));
                                //        File.Move(openFileDialog.FileName, pathToLibrary + fileName);
                                //    }
                                //    else
                                //    {
                                //        MessageBox.Show("Операция отменена");
                                //    }
                                //}
                                //ShowAll();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Нет каталога для хранения книг или файл настроек поврежден." +
                            //        "\nНевозможно произвести операцию.");
                            //}
                        });

            EditCommand = new AddCommand(
                        () =>
                        {
                            AddViewModel model = new AddViewModel(selectedBook.Id);
                            ShowAll();
                        },
                        () => Selected != null
                        );

            RemoveCommand = new AddCommand(
                        () =>
                        {
                            using (LibraryContext context = new LibraryContext())
                            {
                                context.Books.Remove(selectedBook);
                                context.SaveChanges();
                            }
                            ShowAll();
                        },
                        () => Selected != null
                        );
        }

        bool CheckPathToCatalog()
        {
            if (!Directory.Exists(pathToLibrary))
            {
                if (File.Exists(SETFILE))
                {
                    SetDirectoryToLibrary();
                }
                if (!Directory.Exists(pathToLibrary))
                {
                    SettingWindow set = new SettingWindow(SETFILE);
                    if (set.ShowDialog() == true)
                    {
                        SetDirectoryToLibrary();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Нет каталога для хранения книг или файл настроек поврежден");
                    //}
                }
            }
            return Directory.Exists(pathToLibrary);
        }

        void SetDirectoryToLibrary()
        {
            using (StreamReader sr = File.OpenText(SETFILE))
            {
                pathToLibrary = File.ReadAllLines(SETFILE)[0];
                sr.Close();
            }
        }

        void ShowAll()
        {
            using (LibraryContext context = new LibraryContext())
            {
                var tempArray = context.Books
                                .Include(book => book.Author)
                                .Include(book => book.Genres)
                                .Include(book => book.BbkCode)
                                .Include(book => book.Info)
                                .ToArray();
                MyBooks.Clear();
                foreach (var item in tempArray)
                {
                    MyBooks.Add(item);
                }
            }
        }
    }
}

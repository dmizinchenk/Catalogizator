using Catalogizator.Library;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading;
using System.Windows.Documents.DocumentStructures;

namespace Catalogizator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            //AddEditWindow w = new AddEditWindow();
            //w.Show();

            //InitializeBook();
            //DataContext = new ViewModel();
        }

        void InitializeBook()
        {
            using (LibraryContext context = new LibraryContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //Первая книга
                Genre comix = new Genre() { Name = "Комикс" };
                Genre fantastic = new Genre() { Name = "Фантастика" };

                Author garrison = new Author()
                {
                    Name = "Гарри",
                    LastName = "Гаррисон"
                };

                BookInfo biSR = new BookInfo()
                {
                    Pages = 700,
                    Publisher = "Эксмо",
                    PublishYear = 2007,
                    About = "Приключения молодого мошенника"
                };

                BbkCode bbkSr = new BbkCode() { Code = "84.(7 США)" };

                Book steelRat = new Book()
                {
                    Title = "Стальная Крыса",
                    Isbn = 9785699118496,
                    ReleaseYear = 1957,
                    BbkCode = bbkSr,
                    Author = garrison,
                    Info = biSR
                };
                steelRat.Genres.Add(comix);
                steelRat.Genres.Add(fantastic);

                context.Books.Add(steelRat);
                context.BookInfo.Add(biSR);
                context.Authors.Add(garrison);
                context.Genres.Add(fantastic);
                context.Genres.Add(comix);
                context.BbkCodes.Add(bbkSr);


                //Вторая книга
                Genre roman = new Genre() { Name = "Роман" };

                Author king = new Author()
                {
                    Name = "Стивен",
                    LastName = "Кинг"
                };

                BbkCode bbkI = bbkSr;

                BookInfo biI = new BookInfo()
                {
                    Pages = 432,
                    Publisher = "Кэдмен",
                    PublishYear = 2007,
                    About = "Сражение с детскими травмами"
                };

                Book it = new Book()
                {
                    Title = "Оно ч.1",
                    Isbn = 5857430135,
                    ReleaseYear = 1986,
                    BbkCode = bbkI,
                    Author = king,
                    Info = biI
                };
                it.Genres.Add(roman);
                context.BookInfo.Add(biI);
                context.Books.Add(it);
                context.Authors.Add(king);
                context.Genres.Add(roman);

                context.SaveChanges();
            }
        }

        void EditDB()
        {
            using (LibraryContext context = new LibraryContext())
            {
                context.Database.EnsureCreated();


                FileInfo file = new FileInfo("D:\\Обучение Шаг\\С#\\Курсовой\\Genres.txt");
                using (StreamReader sr = file.OpenText())
                {
                    Chapter chapter = null!;
                    do
                    {
                        string temp = sr.ReadLine()!;
                        if (!temp.StartsWith('\t'))
                        {   
                            if(chapter != null)
                            {
                                context.Chapters.Add(chapter);
                            }
                            chapter = new Chapter();
                            chapter.Name = temp;
                        }
                        else
                        {
                            Genre genre = new Genre();
                            genre.Name = temp.Trim();
                            chapter.Genres.Add(genre);
                            context.Genres.Add(genre);
                        }
                    } while (!sr.EndOfStream);
                    context.Chapters.Add(chapter);
                    context.SaveChanges();
                    sr.Close();
                }
            }
        }

    }
}

using Catalogizator.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.AddWindow
{
    internal class AddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Book book;
        public Book AddBook 
        {
            get => book; 
            set
            {
                book = value;
                OnPropertyChanged(nameof(AddBook));
            }
        }

        public List<Chapter> Chapters { get; set; }

        public bool IsCompleteAdded { get; private set; } = false;
        public AddViewModel(int? idBook = null!) 
        {
            using (LibraryContext context = new LibraryContext())
            {
                if (idBook == null)
                {
                    this.book = new Book()
                    {
                        Author = new Author(),
                        BbkCode = new BbkCode(),
                        Genres = new List<Genre>(),
                        Info = new BookInfo()
                    };
                }
                else
                    this.book = context.Books
                                .Include(book => book.Author)
                                //.Include(book => book.Genres)
                                .Include(book => book.BbkCode)
                                .Include(book => book.Info)
                                .FirstOrDefault(book => book.Id == idBook)!;

                Chapters = context.Chapters.Include(chapter => chapter.Genres).ToList();

                AddEditWindow window = new AddEditWindow();
                window.DataContext = this;
                if (window.ShowDialog() == true)
                {
                    if(idBook == null)
                    {
                        if(book.Info != null)
                            context.BookInfo.Add(book.Info);
                        context.Authors.Add(book.Author);
                        //foreach (var genre in book.Genres)
                        //    context.Genres.Add(genre);
                        context.BbkCodes.Add(book.BbkCode);
                        context.Books.Add(book);
                    }
                    context.SaveChanges();
                    IsCompleteAdded = true;
                } 
            }
        }
    }
}

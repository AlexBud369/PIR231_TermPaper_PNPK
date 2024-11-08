using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPaper_PNPK
{
    internal class Program
    {
        private const string BooksFilePath = "books.txt";
        private const string GenresFilePath = "genres.txt";
        private const string PublishersFilePath = "publishers.txt";
        private const string AcquisitionMethodsFilePath = "acquisition_methods.txt";

        static void Main(string[] args)
        {
            Menu();
        }

        public struct Book
        {
            public int Id;
            public string Author;
            public string Title;
            public string Genre;
            public string Publisher;
            public int Year;
            public int VolumeCount;
            public string AcquisitionMethod;
            public decimal Price;
            public string ReaderFullName;
            public string Notes;
        }

        public struct Genre
        {
            public string Name;
        }

        public struct Publisher
        {
            public string Name;
        }

        public struct AcquisitionMethod
        {
            public string Name;
        }


        public static List<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();
            using (StreamReader reader = new StreamReader(BooksFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    books.Add(new Book
                    {
                        Id = int.Parse(fields[0]),
                        Author = fields[1],
                        Title = fields[2],
                        Genre = fields[3],
                        Publisher = fields[4],
                        Year = int.Parse(fields[5]),
                        VolumeCount = int.Parse(fields[6]),
                        AcquisitionMethod = fields[7],
                        Price = decimal.Parse(fields[8]),
                        ReaderFullName = fields[9],
                        Notes = fields[10]
                    });
                }
            }
            return books;
        }

        public static void SaveBooks(List<Book> books)
        {
            using (StreamWriter writer = new StreamWriter(BooksFilePath))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Id};{book.Author};{book.Title};{book.Genre};{book.Publisher};{book.Year};{book.VolumeCount};{book.AcquisitionMethod};{book.Price};{book.ReaderFullName};{book.Notes}");
                }
            }
        }

        public static List<Genre> LoadGenres()
        {
            List<Genre> genres = new List<Genre>();
            using (StreamReader reader = new StreamReader(GenresFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    genres.Add(new Genre { Name = line });
                }
            }
            return genres;
        }

        public static void SaveGenres(List<Genre> genres)
        {
            using (StreamWriter writer = new StreamWriter(GenresFilePath))
            {
                foreach (var genre in genres)
                {
                    writer.WriteLine(genre.Name);
                }
            }
        }

        public static List<Publisher> LoadPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();
            using (StreamReader reader = new StreamReader(PublishersFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    publishers.Add(new Publisher { Name = line });
                }
            }
            return publishers;
        }

        public static void SavePublishers(List<Publisher> publishers)
        {
            using (StreamWriter writer = new StreamWriter(PublishersFilePath))
            {
                foreach (var publisher in publishers)
                {
                    writer.WriteLine(publisher.Name);
                }
            }
        }

        public static List<AcquisitionMethod> LoadAcquisitionMethods()
        {
            List<AcquisitionMethod> acquisitionMethods = new List<AcquisitionMethod>();
            using (StreamReader reader = new StreamReader(AcquisitionMethodsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    acquisitionMethods.Add(new AcquisitionMethod { Name = line });
                }
            }
            return acquisitionMethods;
        }

        public static void SaveAcquisitionMethods(List<AcquisitionMethod> acquisitionMethods)
        {
            using (StreamWriter writer = new StreamWriter(AcquisitionMethodsFilePath))
            {
                foreach (var method in acquisitionMethods)
                {
                    writer.WriteLine(method.Name);
                }
            }
        }
    



    static void TextOutput(string text)        
        {
            Console.Write(text);
        }

        static void MenuTextOutput()
        {
            TextOutput("Выберите действие для работы с книгой");
            TextOutput("Нажмите 1 для добавления данных о книге");
            TextOutput("Нажмите 2 для изменения данных о книге");
            TextOutput("Нажмите 3 для удаления данных о книге");
            TextOutput("Нажмите 4 для поиска книг");
            TextOutput("Нажмите 5 для сортировки книг");
            TextOutput("Для выхода нажмите 6");
        }

        static void Menu()
        {
            MenuTextOutput();
            int menuItem = GetIntPositiveDigit();

            switch (menuItem)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:

                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }



        }


        static int GetIntPositiveDigit()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value) || value < 1 || value > 6)
            {
                Console.WriteLine("Введена не существующая операция. Введите заново. ");
            }

            return value;
        }

    }
}

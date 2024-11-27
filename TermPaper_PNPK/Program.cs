using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static TermPaper_PNPK.Program;

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
            Book[] books = LoadBooks();
            Genre[] genres = LoadGenres();
            Publisher[] publishers = LoadPublishers();
            AcquisitionMethod[] acquisitionMethods = LoadAcquisitionMethods();
            Menu(books, genres, publishers, acquisitionMethods);

    }

  

        public struct Book
        {
            public string bookId;
            public string author;
            public string title;
            public string genre;
            public string publisher;
            public int year;
            public int volumeCount;
            public string acquisitionMethod;
            public decimal price;
            public string readerFullName;
            public string notes;
        }

        public struct Genre
        {
            public string genreName;
        }

        public struct Publisher
        {
            public string publisherName;
        }

        public struct AcquisitionMethod
        {
            public string acquisitionMethodName;
        }




        public static Book[] LoadBooks()
        {
            string[] lines = File.ReadAllLines(BooksFilePath);
            Book[] books = new Book[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(';');
                books[i] = new Book
                {
                    bookId = fields[0],
                    author = fields[1],
                    title = fields[2],
                    genre = fields[3],
                    publisher = fields[4],
                    year = int.Parse(fields[5]),
                    volumeCount = int.Parse(fields[6]),
                    acquisitionMethod = fields[7],
                    price = decimal.Parse(fields[8]),
                    readerFullName = fields[9],
                    notes = fields[10]
                };
            }
            return books;
        }

        public static void SaveBooks(Book[] books)
        {
            using (StreamWriter writer = new StreamWriter(BooksFilePath))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.bookId};{book.author};{book.title};{book.genre};{book.publisher};{book.year};{book.volumeCount};{book.acquisitionMethod};{book.price};{book.readerFullName};{book.notes}");
                }
            }
        }

        public static Genre[] LoadGenres()
        {
            string[] lines = File.ReadAllLines(GenresFilePath);
            Genre[] genres = new Genre[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                genres[i] = new Genre { genreName = lines[i] };
            }
            return genres;
        }

        public static void SaveGenres(Genre[] genres)
        {
            using (StreamWriter writer = new StreamWriter(GenresFilePath))
            {
                foreach (var genre in genres)
                {
                    writer.WriteLine(genre.genreName);
                }
            }
        }

        public static Publisher[] LoadPublishers()
        {
            string[] lines = File.ReadAllLines(PublishersFilePath);
            Publisher[] publishers = new Publisher[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                publishers[i] = new Publisher { publisherName = lines[i] };
            }
            return publishers;
        }

        public static void SavePublishers(Publisher[] publishers)
        {
            using (StreamWriter writer = new StreamWriter(PublishersFilePath))
            {
                foreach (var publisher in publishers)
                {
                    writer.WriteLine(publisher.publisherName);
                }
            }
        }

        public static AcquisitionMethod[] LoadAcquisitionMethods()
        {
            string[] lines = File.ReadAllLines(AcquisitionMethodsFilePath);
            AcquisitionMethod[] acquisitionMethods = new AcquisitionMethod[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                acquisitionMethods[i] = new AcquisitionMethod { acquisitionMethodName = lines[i] };
            }
            return acquisitionMethods;
        }

        public static void SaveAcquisitionMethods(AcquisitionMethod[] acquisitionMethods)
        {
            using (StreamWriter writer = new StreamWriter(AcquisitionMethodsFilePath))
            {
                foreach (var method in acquisitionMethods)
                {
                    writer.WriteLine(method.acquisitionMethodName);
                }
            }
        }
    

        static void ProgramExitMethod()
        {
            Environment.Exit(0);
        }


    static void TextOutput(string text)        
        {
            Console.Write(text);
        }

        static void MenuTextOutput()
        {
            TextOutput("Выберите с каким элементом будете взаимодействовать");
            TextOutput("Нажмите 1 для выбора книг");
            TextOutput("Нажмите 2 для выбора жанров");
            TextOutput("Нажмите 3 для выбора издательств");
            TextOutput("Нажмите 4 для выбора способов приобретения");
            TextOutput("Нажмите 5 для выхода");
        }

        static void Menu(Book[] books, Genre[] genres, Publisher[] publishers, AcquisitionMethod[] acquisitionMethods)
        {
            MenuTextOutput();
            int menuItem = GetIntPositiveDigit(@"^[1-5]$");



            switch (menuItem)
            {
                case 1:
                    HandleBooks(books, genres, acquisitionMethods);
                    break;
                case 2:
                    HandleGenres();
                    break;
                case 3:
                    HandlePublishers();
                    break;
                case 4:
                    HandleAcquisitionMethods();
                    break;
                case 5:
                    ProgramExitMethod();
                    break;
               
            }



        }



        static void HandleBooks(Book[] books, Genre[] genres, AcquisitionMethod[] acequisitationMethods)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для книг:");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу");
                Console.WriteLine("3. Редактировать книгу");
                Console.WriteLine("4. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-4]$");

                switch (choice)
                {
                    case 1:
                        AddBook(books, genres, acequisitationMethods);
                        break;
                    case 2:
                        DeleteBook();
                        break;
                    case 3:
                        UpdateBook();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandleGenres()
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для жанров:");
                Console.WriteLine("1. Добавить жанр");
                Console.WriteLine("2. Удалить жанр");
                Console.WriteLine("3. Редактировать жанр");
                Console.WriteLine("4. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-4]$");

                switch (choice)
                {
                    case 1:
                        AddGenre();
                        break;
                    case 2:
                        DeleteGenre();
                        break;
                    case 3:
                        UpdateGenre();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandlePublishers()
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для издательств:");
                Console.WriteLine("1. Добавить издательство");
                Console.WriteLine("2. Удалить издательство");
                Console.WriteLine("3. Редактировать издательство");
                Console.WriteLine("4. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-4]$");

                switch (choice)
                {
                    case 1:
                        AddPublisher();
                        break;
                    case 2:
                        DeletePublisher();
                        break;
                    case 3:
                        UpdatePublisher();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandleAcquisitionMethods()
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для способов приобретения:");
                Console.WriteLine("1. Добавить способ приобретения");
                Console.WriteLine("2. Удалить способ приобретения");
                Console.WriteLine("3. Редактировать способ приобретения");
                Console.WriteLine("4. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-4]$");

                switch (choice)
                {
                    case 1:
                        AddAcquisitionMethod();
                        break;
                    case 2:
                        DeleteAcquisitionMethod();
                        break;
                    case 3:
                        UpdateAcquisitionMethod();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static string ReadStringWithRegex(string prompt, string regexPattern, params string[] validValues)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (Regex.IsMatch(input, regexPattern))
                {
                    if (validValues.Length == 0 || validValues.Contains(input))
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: введите значение из предложенного списка.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное значение.");
                }
            }
        }

        public static int ReadIntWithRegex(string prompt, string regexPattern, int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (Regex.IsMatch(input, regexPattern))
                {
                    int number = int.Parse(input);
                    if (number >= minValue && number <= maxValue)
                    {
                        return number;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: число должно быть в диапазоне от {minValue} до {maxValue}.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное значение.");
                }
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                    return result;
                Console.WriteLine("Ошибка ввода. Пожалуйста, введите число.");
            }
        }

        public static bool Confirm(string prompt)
        {
            Console.Write($"{prompt} (да/нет): ");
            string input = Console.ReadLine().ToLower();
            return input == "да" || input == "yes";
        }

      

        static void AddBook(Book[] books, Genre[] genres, AcquisitionMethod[] acquisitionMethods)
        {
            string author = ReadString("Автор: ");

            string title = ReadString("Название: ");

            Console.WriteLine("Список жанров:");
            foreach (var genr in genres)
            {
                Console.WriteLine(genr.genreName);
            }
            string genre = ReadStringWithRegex("Жанр: ", @"^[A-ZА-Я][a-zа-я]*$", genres.Select(g => g.genreName).ToArray());

            string publisher = ReadStringWithRegex("Издательство: ", @"^[A-ZА-Я][a-zа-я]*$");

            int year = ReadIntWithRegex("Год издания: ", @"^\d{4}$", 1850, 2024);

            int volumeCount = ReadIntWithRegex("Количество томов: ", @"^\d+$", 1, 100);


            Console.WriteLine("Список способов приобретения:");
            foreach (var method in acquisitionMethods)
            {
                Console.WriteLine(method.acquisitionMethodName);
            }
            string acquisitionMethod = ReadStringWithRegex("Способ приобретения: ", @"^[A-ZА-Я][a-zа-я]*$");
            if (!acquisitionMethods.Any(m => m.acquisitionMethodName == acquisitionMethod))
            {
                Array.Resize(ref acquisitionMethods, acquisitionMethods.Length + 1);
                acquisitionMethods[acquisitionMethods.Length - 1] = new AcquisitionMethod { acquisitionMethodName = acquisitionMethod };
            }


            decimal price = ReadDecimal("Цена: ");

            string readerFullName = ReadString("ФИО читателя: ");

            string notes = ReadString("Примечания: ");


            string uniqueId = GenerateUniqueId(books, title, year);


            Book newBook = new Book
            {
                bookId = uniqueId,
                author = author,
                title = title,
                genre = genre,
                publisher = publisher,
                year = year,
                volumeCount = volumeCount,
                acquisitionMethod = acquisitionMethod,
                price = price,
                readerFullName = readerFullName,
                notes = notes
            };

            int bookCount = books.Length;

            books[bookCount] = newBook;
            bookCount++;

            Console.WriteLine("Книга успешно добавлена.");

            if (Confirm("Сохранить изменения?"))
            {
                SaveBooks(books.Take(bookCount).ToArray());
                SaveAcquisitionMethods(acquisitionMethods);
            }
        }

        static string GenerateUniqueId(Book[] books, string title, int year)
        {
            // Удаление пробелов и специальных символов из названия книги
            string cleanedTitle = Regex.Replace(title, @"[^a-zA-Z0-9]", "");
            string id = $"{cleanedTitle}_{year}";

            // Проверка на уникальность ID
            int counter = 1;
            while (books.Any(b => b.bookId == id))
            {
                id = $"{cleanedTitle}_{year}_{counter}";
                counter++;
            }

            return id;
        }


        static void DeleteBook()
        {
            // Логика удаления книги
            Console.WriteLine("Удаление книги...");
        }

        static void UpdateBook()
        {
            // Логика редактирования книги
            Console.WriteLine("Редактирование книги...");
        }

        static void AddGenre()
        {
            // Логика добавления жанра
            Console.WriteLine("Добавление жанра...");
        }

        static void DeleteGenre()
        {
            // Логика удаления жанра
            Console.WriteLine("Удаление жанра...");
        }

        static void UpdateGenre()
        {
            // Логика редактирования жанра
            Console.WriteLine("Редактирование жанра...");
        }

        static void AddPublisher()
        {
            // Логика добавления издательства
            Console.WriteLine("Добавление издательства...");
        }

        static void DeletePublisher()
        {
            // Логика удаления издательства
            Console.WriteLine("Удаление издательства...");
        }

        static void UpdatePublisher()
        {
            // Логика редактирования издательства
            Console.WriteLine("Редактирование издательства...");
        }

        static void AddAcquisitionMethod()
        {
            // Логика добавления способа приобретения
            Console.WriteLine("Добавление способа приобретения...");
        }

        static void DeleteAcquisitionMethod()
        {
            // Логика удаления способа приобретения
            Console.WriteLine("Удаление способа приобретения...");
        }

        static void UpdateAcquisitionMethod()
        {
            // Логика редактирования способа приобретения
            Console.WriteLine("Редактирование способа приобретения...");
        }


        /*static int GetIntPositiveDigit()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value) || value < 1 || value > 6)
            {
                Console.WriteLine("Введена не существующая операция. Введите заново. ");
            }

            return value;
        }
        */
        static int GetIntPositiveDigit(string pattern)
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value) || !(Regex.IsMatch(Convert.ToString(value), pattern)))
            {
                Console.WriteLine("Введена не существующая операция. Введите заново. ");
            }

            return value;
        }


       


    }
}

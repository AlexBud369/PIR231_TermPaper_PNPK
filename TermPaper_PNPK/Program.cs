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
            public string year;
            public string volumeCount;
            public string acquisitionMethod;
            public string price;
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
                    year = fields[5],
                    volumeCount = fields[6],
                    acquisitionMethod = fields[7],
                    price = fields[8],
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

         static Genre[] LoadGenres()
        {
            string[] lines = File.ReadAllLines(GenresFilePath);
            Genre[] genres = new Genre[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                genres[i] = new Genre { genreName = lines[i] };
            }
            return genres;
        }

        static void SaveGenres(Genre[] genres)
        {
            File.WriteAllLines(GenresFilePath, genres.Select(g => g.genreName));
        }

        static Publisher[] LoadPublishers()
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
            Console.WriteLine(text);
        }

        static void ClearMenu() 
        { 
            Console.Clear();
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

        static void Menu(Book[] books, Genre[] genres, Publisher[] publishers, AcquisitionMethod[] acquisitionMethod)
        {
            MenuTextOutput();
            int menuItem = GetIntPositiveDigit(@"^[1-5]$", "Введена не существующая операция. Введите заново. ");



            switch (menuItem)
            {
                case 1:
                    ClearMenu();
                    HandleBooks(books, genres, acquisitionMethod);
                    break;
                case 2:
                    ClearMenu();
                    HandleGenres(genres);
                    break;
                case 3:
                    ClearMenu();
                    HandlePublishers(publishers);
                    break;
                case 4:
                    ClearMenu();
                    HandleAcquisitionMethods(acquisitionMethod);
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

                int choice = GetIntPositiveDigit(@"^[1-4]$", "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        AddBook(books, genres, acequisitationMethods);
                        ClearMenu();
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

        static void HandleGenres(Genre[] genres)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для жанров:");
                Console.WriteLine("1. Добавить жанр");
                Console.WriteLine("2. Удалить жанр");
                Console.WriteLine("3. Редактировать жанр");
                Console.WriteLine("4. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-4]$", "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        
                        AddGenre(genres);
                        
                        break;
                    case 2:
                        ClearMenu();
                        DeleteGenre(genres);
                        break;
                    case 3:
                        ClearMenu();
                        UpdateGenre(genres);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandlePublishers(Publisher[] publishers)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для издательств:");
                Console.WriteLine("1. Добавить издательство");
                Console.WriteLine("2. Удалить издательство");
                Console.WriteLine("3. Редактировать издательство");
                Console.WriteLine("4. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-4]$" , "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        AddPublisher(publishers);
                        break;
                    case 2:
                        DeletePublisher(publishers);
                        break;
                    case 3:
                        UpdatePublisher(publishers);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandleAcquisitionMethods(AcquisitionMethod[] acquisitionMethod)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для способов приобретения:");
                Console.WriteLine("1. Добавить способ приобретения");
                Console.WriteLine("2. Удалить способ приобретения");
                Console.WriteLine("3. Редактировать способ приобретения");
                Console.WriteLine("4. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-4]$" , "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        AddAcquisitionMethod(acquisitionMethod);
                        break;
                    case 2:
                        DeleteAcquisitionMethod(acquisitionMethod);
                        break;
                    case 3:
                        UpdateAcquisitionMethod(acquisitionMethod);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }


        static int GetIntPositiveDigit(string pattern, string promt)
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value) || !(Regex.IsMatch(Convert.ToString(value), pattern)))
            {
                Console.WriteLine(promt);
            }

            return value;
        }


        static string ReadStringWithValidation(string prompt, string regexPattern, params string[] validValues)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ошибка: поле не может быть пустым.");
                    continue;
                }

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

      



        static string ReadNonEmptyString(string prompt, string regexPattern)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                if (Regex.IsMatch(input, regexPattern))
                {
                    return input;
                }

                else
                {
                    Console.WriteLine("Ошибка: поле не может быть пустым.");
                }
            }
        }


        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double result) && result > 0)
                { 
                    return result;
                }

                Console.WriteLine("Ошибка ввода. Пожалуйста, введите число.");
            }
        }

        static bool Confirm(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (да/нет): ");
                string input = Console.ReadLine().ToLower();

                if (input == "да" || input == "нет")
                {
                    return input == "да";
                }
                else
                {
                    Console.WriteLine("Ошибка: введите 'да' или 'нет'.");
                }
            }
        }



        static void AddBook(Book[] books, Genre[] genres, AcquisitionMethod[] acquisitionMethods)
        {
            string author = ReadNonEmptyString("Автор: ", @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ]\.\s[А-ЯЁ]\.$");

            string title = ReadNonEmptyString("Название: ", @"^[А-ЯЁA-Za-z0-9\s-]+$");

            Console.WriteLine("Список жанров:");
            foreach (var genr in genres)
            {
                Console.WriteLine(" - " + genr.genreName);
            }
            string genre = ReadStringWithValidation("Жанр: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$", genres.Select(g => g.genreName).ToArray());

            string publisher = ReadStringWithValidation("Издательство: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");



            TextOutput("Год издания: ");
            int year = GetIntPositiveDigit(@"^(18[5-9]\d|19\d\d|20[01]\d|202[0-4])$", "Введен недопустимый год. Введите заново.");

            TextOutput("Количество томов: ");
            int volumeCount = GetIntPositiveDigit(@"^([1-9]|[1-9][0-9]|100)$", "Введено недопустимое количество томов. Введите заново. ");

            ;


            Console.WriteLine("Список способов приобретения:");
            foreach (var method in acquisitionMethods)
            {
                Console.WriteLine(" - "+method.acquisitionMethodName);
            }
            string acquisitionMethod = ReadStringWithValidation("Способ приобретения: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");
            if (!acquisitionMethods.Any(m => m.acquisitionMethodName == acquisitionMethod))
            {
                Array.Resize(ref acquisitionMethods, acquisitionMethods.Length + 1);
                acquisitionMethods[acquisitionMethods.Length - 1] = new AcquisitionMethod { acquisitionMethodName = acquisitionMethod };
            }


            double price = ReadDouble("Цена: ");

            string readerFullName = ReadNonEmptyString("ФИО читателя: ", @"^[A-ZА-Я][a-zа-я]*([\s-][A-ZА-Я][a-zа-я]*)*$");

            string notes = ReadNonEmptyString ("Примечания: ", @"^[А-ЯЁA-Za-z0-9\s-]+$");


            string uniqueId = GenerateUniqueId(books, title, author);


            Book newBook = new Book
            {
                bookId = uniqueId,
                author = author,
                title = title,
                genre = genre,
                publisher = publisher,
                year = Convert.ToString(year),
                volumeCount = Convert.ToString(volumeCount),
                acquisitionMethod = acquisitionMethod,
                price = Convert.ToString(price),
                readerFullName = readerFullName,
                notes = notes
            };

           
            Array.Resize(ref books, books.Length + 1);
            books[books.Length - 1] = newBook; 


            if (Confirm("Сохранить изменения?"))
            {
                SaveBooks(books);
                SaveAcquisitionMethods(acquisitionMethods);
            }

            
        }

        static string GenerateUniqueId(Book[] books, string title, string author)
        {
            // Удаление пробелов и специальных символов из названия книги и автора
            string cleanedTitle = Regex.Replace(title, @"[^а-яА-Я0-9]", "");
            string cleanedAuthor = Regex.Replace(author, @"[^а-яА-Я0-9]", "");
            string id = $"ID_{cleanedTitle}_{cleanedAuthor}";

            // Проверка на уникальность ID
            int counter = 1;
            while (books.Any(b => b.bookId == id))
            {
                id = $"ID_{cleanedTitle}_{cleanedAuthor}_{counter}";
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


        static bool GenreExists(Genre[] genres, string genreName)
        {
            if (genres.Any(g => g.genreName.Equals(genreName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Такой жанр уже существует. Пожалуйста, введите другой жанр.");
                return true;
            }
            return false;
        }

        static bool PublisherExists(Publisher[] publisher, string publisherName)
        {
            if (publisher.Any(g => g.publisherName.Equals(publisherName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Такой издатель уже существует. Пожалуйста, введите другого издателя.");
                return true;
            }
            return false;
        }

        static bool AcquisitionMethodExists(AcquisitionMethod[] acquisitionMethod, string acquisitionMethodName)
        {
            if (acquisitionMethod.Any(g => g.acquisitionMethodName.Equals(acquisitionMethodName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Такой способ приобретения уже существует. Пожалуйста, введите другой способ приобретения.");
                return true;
            }
            return false;
        }


        static void AddGenre(Genre[] genres)
        {
            string genreName;


            do
            {
                genreName = ReadNonEmptyString("Название жанра ", @"^[A-ZА-Я][a-zа-я]*$"); ;
            } while (GenreExists(genres, genreName));

            if (Confirm("Сохранить изменения?"))
            {
                Genre newGenre = new Genre { genreName = genreName };
                Genre[] updatedGenres = new Genre[genres.Length + 1];

                // Копируем старые жанры в новый массив
                for (int i = 0; i < genres.Length; i++)
                {
                    updatedGenres[i] = genres[i];
                }

                // Добавляем новый жанр в конец массива
                updatedGenres[genres.Length] = newGenre;

                Console.WriteLine($"Жанр '{genreName}' добавлен.");
                SaveGenres(updatedGenres);


            }
        }

        static void DeleteGenre(Genre[] genres)
        {
            // Логика удаления жанра
            Console.WriteLine("Удаление жанра...");
        }

        static void UpdateGenre(Genre[] genres)
        {
            // Логика редактирования жанра
            Console.WriteLine("Редактирование жанра...");
        }

        static void AddPublisher(Publisher[] publishers)
        {
            string publisherName;

            do
            {
                publisherName = ReadNonEmptyString("Название издательства: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (PublisherExists(publishers, publisherName)); // Предполагается, что у вас есть метод PublisherExists

            if (Confirm("Сохранить изменения?"))
            {
                Publisher newPublisher = new Publisher { publisherName = publisherName };
                Publisher[] updatedPublishers = new Publisher[publishers.Length + 1];

                // Копируем старые издательства в новый массив
                for (int i = 0; i < publishers.Length; i++)
                {
                    updatedPublishers[i] = publishers[i];
                }

                // Добавляем новое издательство в конец массива
                updatedPublishers[publishers.Length] = newPublisher;

                Console.WriteLine($"Издательство '{publisherName}' добавлено.");
                SavePublishers(updatedPublishers); // Предполагается, что у вас есть метод SavePublishers
            }
        }

        static void DeletePublisher(Publisher[] publishers)
        {
            // Логика удаления издательства
            Console.WriteLine("Удаление издательства...");
        }

        static void UpdatePublisher(Publisher[] publishers)
        {
            // Логика редактирования издательства
            Console.WriteLine("Редактирование издательства...");
        }

        static void AddAcquisitionMethod(AcquisitionMethod[] acquisitionMethods)
        {
            string methodName;

            do
            {
                methodName = ReadNonEmptyString("Название способа приобретения: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (AcquisitionMethodExists(acquisitionMethods, methodName)); // Предполагается, что у вас есть метод AcquisitionMethodExists

            if (Confirm("Сохранить изменения?"))
            {
                AcquisitionMethod newMethod = new AcquisitionMethod { acquisitionMethodName = methodName };
                AcquisitionMethod[] updatedMethods = new AcquisitionMethod[acquisitionMethods.Length + 1];

                // Копируем старые методы приобретения в новый массив
                for (int i = 0; i < acquisitionMethods.Length; i++)
                {
                    updatedMethods[i] = acquisitionMethods[i];
                }

                // Добавляем новый метод приобретения в конец массива
                updatedMethods[acquisitionMethods.Length] = newMethod;

                Console.WriteLine($"Способ приобретения '{methodName}' добавлен.");
                SaveAcquisitionMethods(updatedMethods); // Предполагается, что у вас есть метод SaveAcquisitionMethods
            }
        }

        static void DeleteAcquisitionMethod(AcquisitionMethod[] acquisitionMethod)
        {
            // Логика удаления способа приобретения
            Console.WriteLine("Удаление способа приобретения...");
        }

        static void UpdateAcquisitionMethod(AcquisitionMethod[] acquisitionMethod)
        {
            // Логика редактирования способа приобретения
            Console.WriteLine("Редактирование способа приобретения...");
        }


       
     

       


    }
}

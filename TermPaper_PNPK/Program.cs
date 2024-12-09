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

        /// <summary>
        /// Основной метод программы, который запускает меню.
        /// </summary>
        static void Main(string[] args)
        {
            Menu();
        }

        /// <summary>
        /// Структура, представляющая книгу с различными атрибутами.
        /// </summary>
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

        /// <summary>
        /// Структура, представляющая жанр.
        /// </summary>
        public struct Genre
        {
            public string genreName;
        }

        /// <summary>
        /// Структура, представляющая издательство.
        /// </summary>
        public struct Publisher
        {
            public string publisherName;
        }

        /// <summary>
        /// Структура, представляющая метод приобретения книги.
        /// </summary>
        public struct AcquisitionMethod
        {
            public string acquisitionMethodName;
        }



        /// <summary>
        /// Загружает книги из файла и возвращает массив структур Book.
        /// </summary>
        /// <returns>Массив структур Book.</returns>
        static Book[] LoadBooks()
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

        /// <summary>
        /// Сохраняет массив книг в файл.
        /// </summary>
        /// <param name="books">Массив структур Book для сохранения.</param>
        static void SaveBooks(Book[] books)
        {
            using (StreamWriter writer = new StreamWriter(BooksFilePath))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.bookId};{book.author};{book.title};{book.genre};{book.publisher};{book.year};{book.volumeCount};{book.acquisitionMethod};{book.price};{book.readerFullName};{book.notes}");
                }
            }
        }

        /// <summary>
        /// Загружает жанры из файла и возвращает массив структур Genre.
        /// </summary>
        /// <returns>Массив структур Genre.</returns>
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

        /// <summary>
        /// Сохраняет массив жанров в файл.
        /// </summary>
        /// <param name="genres">Массив структур Genre для сохранения.</param>
        static void SaveGenres(Genre[] genres)
        {
            File.WriteAllLines(GenresFilePath, genres.Select(g => g.genreName));
        }

        /// <summary>
        /// Загружает издательства из файла и возвращает массив структур Publisher.
        /// </summary>
        /// <returns>Массив структур Publisher.</returns>
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

        /// <summary>
        /// Сохраняет массив издательств в файл.
        /// </summary>
        /// <param name="publishers">Массив структур Publisher для сохранения.</param>
        static void SavePublishers(Publisher[] publishers)
        {
            using (StreamWriter writer = new StreamWriter(PublishersFilePath))
            {
                foreach (var publisher in publishers)
                {
                    writer.WriteLine(publisher.publisherName);
                }
            }
        }

        /// <summary>
        /// Загружает методы приобретения из файла и возвращает массив структур AcquisitionMethod.
        /// </summary>
        /// <returns>Массив структур AcquisitionMethod.</returns>
        static AcquisitionMethod[] LoadAcquisitionMethods()
        {
            string[] lines = File.ReadAllLines(AcquisitionMethodsFilePath);
            AcquisitionMethod[] acquisitionMethods = new AcquisitionMethod[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                acquisitionMethods[i] = new AcquisitionMethod { acquisitionMethodName = lines[i] };
            }
            return acquisitionMethods;
        }

        /// <summary>
        /// Сохраняет массив методов приобретения в файл.
        /// </summary>
        /// <param name="acquisitionMethods">Массив структур AcquisitionMethod для сохранения.</param>
        static void SaveAcquisitionMethods(AcquisitionMethod[] acquisitionMethods)
        {
            using (StreamWriter writer = new StreamWriter(AcquisitionMethodsFilePath))
            {
                foreach (var method in acquisitionMethods)
                {
                    writer.WriteLine(method.acquisitionMethodName);
                }
            }
        }

        /// <summary>
        /// Завершает выполнение программы.
        /// </summary>
        static void ProgramExitMethod()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Выводит текст в консоль.
        /// </summary>
        /// <param name="text">Текст для вывода.</param>
        static void TextOutput(string text)        
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Очищает консоль.
        /// </summary>
        static void ClearMenu() 
        { 
            Console.Clear();
        }

        /// <summary>
        /// Выводит сообщение и ожидает нажатия любой клавиши для возврата в главное меню.
        /// </summary>
        static void ReturnToMenu()
        {
            TextOutput("Для возвращения на главное меню нажмите любую кнопку");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        /// <summary>
        /// Выводит текст главного меню.
        /// </summary>
        static void MenuTextOutput()
        {
            TextOutput("Выберите с каким элементом будете взаимодействовать");
            TextOutput("Нажмите 1 для выбора книг");
            TextOutput("Нажмите 2 для выбора жанров");
            TextOutput("Нажмите 3 для выбора издательств");
            TextOutput("Нажмите 4 для выбора способов приобретения");
            TextOutput("Нажмите 5 для выхода");
            
        }

        /// <summary>
        /// Основное меню программы, где пользователь выбирает, с каким элементом будет взаимодействовать.
        /// </summary>
        static void Menu()
        {
            MenuTextOutput();
            int menuItem = GetIntPositiveDigit(@"^[1-5]$", "Введена не существующая операция. Введите заново. ");

            Book[] books = LoadBooks();
            Genre[] genres = LoadGenres();
            Publisher[] publishers = LoadPublishers();
            AcquisitionMethod[] acquisitionMethods = LoadAcquisitionMethods();

            switch (menuItem)
            {
                case 1:
                    ClearMenu();
                    HandleBooks(books, genres, acquisitionMethods, publishers);
                    break;
                case 2:
                    ClearMenu();
                    HandleGenres(books, genres);
                    break;
                case 3:
                    ClearMenu();
                    HandlePublishers(books, publishers);
                    break;
                case 4:
                    ClearMenu();
                    HandleAcquisitionMethods(books, acquisitionMethods);
                    break;
                case 5:
                    ProgramExitMethod();
                    break;
                

            }
        }

        /// <summary>
        /// Выводит текст меню для работы с книгами.
        /// </summary>
        static void HandleBooksTextOutput()
        {
            TextOutput("Выберите действие для книг:");
            TextOutput("1. Добавить книгу");
            TextOutput("2. Удалить книгу");
            TextOutput("3. Редактировать книгу");
            TextOutput("4. Сортировать книгу");
            TextOutput("5. Поиск книги");
            TextOutput("6. Генерация отчетов");
            TextOutput("7. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия пользователя с книгами.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="acquisitionMethods">Массив методов приобретения.</param>
        /// <param name="publishers">Массив издательств.</param>
        static void HandleBooks(Book[] books, Genre[] genres, AcquisitionMethod[] acequisitationMethods, Publisher[] publishers)
        {
            while (true)
            {

                HandleBooksTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-6]$", "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        books=AddBook(books, genres, acequisitationMethods);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveBooks(books);
                            SaveAcquisitionMethods(acequisitationMethods);
                        }
                        ReturnToMenu();
                        break;
                    case 2:
                        books = DeleteBook(books);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveBooks(books);
                        }
                        ReturnToMenu();
                        break;
                    case 3:
                        books = UpdateBook(books, genres, acequisitationMethods, publishers);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveBooks(books);
                        }
                        ReturnToMenu();
                        break;
                    case 4:
                        books = SortBooks(books);
                        ReturnToMenu();
                        break;
                    case 5:
                        SearchBooks(books);
                        ReturnToMenu();
                        break;
                    case 6:
                        HandleReports(books);
                        break;
                    case 7:
                        ClearMenu();
                        Menu();
                        break; 
                    
                }
            }
        }

        /// <summary>
        /// Выводит текст меню для генерации отчетов.
        /// </summary>
        static void HandleReportsTextOutput()
        {
            TextOutput("Нажмите 1 для генерации отчета по жанрам и издательствам");
            TextOutput("Нажмите 2 для генерации отчета по читателям и книгам");
            TextOutput("Нажмите 3 для генерации отчета по жанрам и книгам");
        }

        /// <summary>
        /// Обрабатывает действия пользователя по генерации отчетов.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void HandleReports(Book[] books)
        {
            while (true)
            {

                HandleReportsTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-3]$", "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        ClearMenu();
                        GenerateGenrePublisherReport(books);
                        TextOutput("Отчет по жанрам и издательствам создан.");
                        PrintGenrePublisherReport(books);
                        ReturnToMenu();
                        break;
                    case 2:
                        ClearMenu();
                        GeneratePersonBooksReport(books);
                        TextOutput("Отчет по читателям и книгам создан.");
                        PrintPersonBooksReport(books);
                        ReturnToMenu();
                        break;
                    case 3:
                        ClearMenu();
                        GenerateGenreBooksReport(books);
                        TextOutput("Отчет по жанрам и книгам создан.");
                        PrintGenreBooksReport(books);
                        ReturnToMenu();
                        break;
                }
            }
        }

        /// <summary>
        /// Выводит текст меню для работы с жанрами.
        /// </summary>
        static void HandleGenresTextOutput()
        {
            TextOutput("Выберите действие для жанров:");
            TextOutput("1. Добавить жанр");
            TextOutput("2. Удалить жанр");
            TextOutput("3. Редактировать жанр");
            TextOutput("4. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия пользователя с жанрами.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="genres">Массив жанров.</param>
        static void HandleGenres(Book[] books, Genre[] genres)
        {
            while (true)
            {

                HandleGenresTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-4]$", "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:

                        genres = AddGenre(genres);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveGenres(genres);
                        }
                        ReturnToMenu();
                        break;
                    case 2:
                        genres = DeleteGenre(genres, books);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveGenres(genres);
                            SaveBooks(books); 
                        }
                        ReturnToMenu();
                        break;
                    case 3:
                        genres = UpdateGenre(genres, books);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveGenres(genres);
                            SaveBooks(books); 
                        }
                        ReturnToMenu();
                        break;
                    case 4:
                        ClearMenu();
                        Menu();
                        break;
                    
                }
            }
        }
        /// <summary>
        /// Выводит текст меню для работы с издательствами.
        /// </summary>
        static void HandlePublishersTextOutput()
        {
            TextOutput("Выберите действие для издательств:");
            TextOutput("1. Добавить издательство");
            TextOutput("2. Удалить издательство");
            TextOutput("3. Редактировать издательство");
            TextOutput("4. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия пользователя с издательствами.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="publishers">Массив издательств.</param>
        static void HandlePublishers(Book[] books, Publisher[] publishers)
        {
            while (true)
            {

                HandlePublishersTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-4]$" , "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        publishers = AddPublisher(publishers);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SavePublishers(publishers);
                        }
                        ReturnToMenu();
                        break;
                    case 2:
                        publishers = DeletePublisher(publishers, books);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SavePublishers(publishers);
                            SaveBooks(books); 
                        }
                        break;
                    case 3:
                        publishers = UpdatePublisher(publishers, books);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SavePublishers(publishers);
                            SaveBooks(books); 
                        }
                        ReturnToMenu();
                        break;
                    case 4:
                        ClearMenu();
                        Menu();
                        break;
                }
            }
        }

        /// <summary>
        /// Выводит текст меню для работы со способами приобретения.
        /// </summary>
        static void HandleAcquisitionMethodsTextOutput()
        {
            TextOutput("Выберите действие для способов приобретения:");
            TextOutput("1. Добавить способ приобретения");
            TextOutput("2. Удалить способ приобретения");
            TextOutput("3. Редактировать способ приобретения");
            TextOutput("4. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия пользователя со способами приобретения.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        static void HandleAcquisitionMethods(Book[] books, AcquisitionMethod[] acquisitionMethods)
        {
            while (true)
            {

                HandleAcquisitionMethodsTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-4]$" , "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        acquisitionMethods = AddAcquisitionMethod(acquisitionMethods);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveAcquisitionMethods(acquisitionMethods);
                        }
                        ReturnToMenu();
                        break;
                    case 2:
                        acquisitionMethods = DeleteAcquisitionMethod(acquisitionMethods, books);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveAcquisitionMethods(acquisitionMethods);
                            SaveBooks(books); 
                        }
                        ReturnToMenu();
                        break;
                    case 3:
                        acquisitionMethods = UpdateAcquisitionMethod(acquisitionMethods, books);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveAcquisitionMethods(acquisitionMethods);
                            SaveBooks(books); 
                        }
                        ReturnToMenu();
                        break;
                    case 4:
                        ClearMenu();
                        Menu();
                        break;
                }
            }
        }

        /// <summary>
        /// Получает положительное целое число, соответствующее регулярному выражению.
        /// </summary>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <returns>Положительное целое число.</returns>
        static int GetIntPositiveDigit(string pattern, string promt)
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value) || !(Regex.IsMatch(Convert.ToString(value), pattern)))
            {
                Console.WriteLine(promt);
            }
            return value;
        }

        /// <summary>
        /// Получает строку, соответствующую регулярному выражению.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="regexPattern">Регулярное выражение для проверки.</param>
        /// <returns>Строка, соответствующая регулярному выражению.</returns>
        static string ReadStringWithValidation(string prompt, string regexPattern)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    TextOutput("Ошибка: поле не может быть пустым.");
                    continue;
                }

                if (Regex.IsMatch(input, regexPattern))
                {
                    return input;   
                }
                else
                {
                    TextOutput("Ошибка: введите корректное значение.");
                }
            }
        }

        /// <summary>
        /// Получает непустую строку, соответствующую регулярному выражению.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="regexPattern">Регулярное выражение для проверки.</param>
        /// <returns>Непустая строка, соответствующая регулярному выражению.</returns>
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
                    TextOutput("Ошибка: поле не может быть пустым.");
                }
            }
        }

        /// <summary>
        /// Получает положительное число с плавающей запятой.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <returns>Положительное число с плавающей запятой.</returns>
        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double result) && result > 0)
                { 
                    return result;
                }

                TextOutput("Ошибка ввода. Пожалуйста, введите число.");
            }
        }

        /// <summary>
        /// Запрашивает подтверждение у пользователя.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <returns>True, если пользователь подтвердил, иначе False.</returns>
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
                    TextOutput("Ошибка: введите 'да' или 'нет'.");
                }
            }
        }

        /// <summary>
        /// Добавляет новую книгу в массив книг.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <returns>Обновленный массив книг.</returns>
        static Book[] AddBook(Book[] books, Genre[] genres, AcquisitionMethod[] acquisitionMethods)
        {
            string author = ReadNonEmptyString("Автор: ", @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ]\.\s[А-ЯЁ]\.$");

            string title = ReadNonEmptyString("Название: ", @"^[А-ЯЁA-Za-z0-9\s-]+$");

            TextOutput("Список жанров:");
            foreach (var genr in genres)
            {
                TextOutput(" - " + genr.genreName);
            }
            string genre = ReadStringWithValidation("Жанр: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");

            string publisher = ReadStringWithValidation("Издательство: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");



            TextOutput("Год издания: ");
            int year = GetIntPositiveDigit(@"^(18[5-9]\d|19\d\d|20[01]\d|202[0-4])$", "Введен недопустимый год. Введите заново.");

            TextOutput("Количество томов: ");
            int volumeCount = GetIntPositiveDigit(@"^([1-9]|[1-9][0-9]|100)$", "Введено недопустимое количество томов. Введите заново. ");

            TextOutput("Список способов приобретения:");
            foreach (var method in acquisitionMethods)
            {
                Console.WriteLine(" - " + method.acquisitionMethodName);
            }
            string acquisitionMethod = ReadStringWithValidation("Способ приобретения: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");
            
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

            return books;
        }

        /// <summary>
        /// Генерирует уникальный ID для новой книги.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="title">Название книги.</param>
        /// <param name="author">Автор книги.</param>
        /// <returns>Уникальный ID книги.</returns>
        static string GenerateUniqueId(Book[] books, string title, string author)
        {
            string cleanedTitle = Regex.Replace(title, @"\s+", "");
            string cleanedAuthor = Regex.Replace(author, @"\s+", "");
            string baseId = $"ID_{cleanedTitle}_{cleanedAuthor}";

            // Проверка на наличие свободных номеров
            int counter = 1;
            while (books.Any(b => b.bookId == $"{baseId}_{counter}"))
            {
                counter++;
            }

            return $"{baseId}_{counter}";
        }

        /// <summary>
        /// Выводит список книг.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void BookListOutput(Book[] books)
        {
            foreach (Book book in books)
            {
                TextOutput($"ID: {book.bookId}");
                TextOutput($"Автор: {book.author}");
                TextOutput($"Название: {book.title}");
                TextOutput($"Жанр: {book.genre}");
                TextOutput($"Издательство: {book.publisher}");
                TextOutput($"Год издания: {book.year}");
                TextOutput($"Количество томов: {book.volumeCount}");
                TextOutput($"Способ приобретения: {book.acquisitionMethod}");
                TextOutput($"Цена: {book.price}");
                TextOutput($"ФИО читателя: {book.readerFullName}");
                TextOutput($"Примечания: {book.notes}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Удаляет книгу из массива книг.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <returns>Обновленный массив книг.</returns>
        static Book[] DeleteBook(Book[] books)
        {
            TextOutput("Список книг:");
            BookListOutput(books);

            string bookIdToDelete;
            do
            {
                bookIdToDelete = ReadNonEmptyString("Введите ID книги, которую хотите удалить: ", @"^[a-zA-Z0-9\-_]+$");
            } while (!BookExists(books, bookIdToDelete));

            Book[] updatedBooks = books.Where(book => book.bookId != bookIdToDelete).ToArray();

            TextOutput($"Книга с ID '{bookIdToDelete}' удалена.");

            return updatedBooks;
        }

        /// <summary>
        /// Проверяет, существует ли книга с указанным ID.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="bookId">ID книги для проверки.</param>
        /// <returns>True, если книга существует, иначе False.</returns>
        static bool BookExists(Book[] books, string bookId)
        {
            return books.Any(book => book.bookId == bookId);
        }

        /// <summary>
        /// Обновляет информацию о книге.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <param name="publishers">Массив издательств.</param>
        /// <returns>Обновленный массив книг.</returns>
        static Book[] UpdateBook(Book[] books, Genre[] genres, AcquisitionMethod[] acquisitionMethods, Publisher[] publishers)
        {
            Console.WriteLine("Список книг:");
            BookListOutput(books);

            string bookIdToUpdate;
            do
            {
                bookIdToUpdate = ReadNonEmptyString("Введите ID книги, которую хотите изменить: ", @"^[a-zA-Z0-9\-_]+$");
            } while (!BookExists(books, bookIdToUpdate));

            Book bookToUpdate = books.First(book => book.bookId == bookIdToUpdate);

            TextOutput("Введите новые данные (если не хотите изменять поле, нажмите Enter):");

            string author = ReadNonEmptyStringOrSkip("Новый автор: ", @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ]\.\s[А-ЯЁ]\.$", bookToUpdate.author);
            string title = ReadNonEmptyStringOrSkip("Новое название: ", @"^[а-яА-Яa-zA-Z0-9\s-]+$", bookToUpdate.title);

            TextOutput("Список жанров:");
            foreach (var genr in genres)
            {
                TextOutput($" - {genr.genreName}");
            }
            string genre = ReadStringWithValidationOrSkip("Новый жанр: ", @"^[а-яА-Яa-zA-Z\s-]+$", genres.Select(g => g.genreName).ToArray(), bookToUpdate.genre);

            TextOutput("Список издательств:");
            foreach (var publishe in publishers)
            {
                TextOutput($" - {publishe.publisherName}");
            }
            string publisher = ReadStringWithValidationOrSkip("Новое издательство: ", @"^[а-яА-Яa-zA-Z\s-]+$", publishers.Select(p => p.publisherName).ToArray(), bookToUpdate.publisher);

            string year = ReadNonEmptyStringOrSkip("Новый год издания: ", @"^(18[5-9]\d|19\d\d|20[01]\d|202[0-4])$", bookToUpdate.year);
            string volumeCount = ReadNonEmptyStringOrSkip("Новое количество томов: ", @"^([1-9]|[1-9][0-9]|100)$", bookToUpdate.volumeCount);

            TextOutput("Список способов приобретения:");
            foreach (var method in acquisitionMethods)
            {
                TextOutput($" - {method.acquisitionMethodName}");
            }
            string acquisitionMethod = ReadStringWithValidationOrSkip("Новый способ приобретения: ", @"^[а-яА-Яa-zA-Z\s-]+$", acquisitionMethods.Select(m => m.acquisitionMethodName).ToArray(), bookToUpdate.acquisitionMethod);

            string price = ReadNonEmptyStringOrSkip("Новая цена: ", @"^\d+(\.\d{2})?$", bookToUpdate.price);
            string readerFullName = ReadNonEmptyStringOrSkip("Новое ФИО читателя: ", @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ]\.\s[А-ЯЁ]\.$", bookToUpdate.readerFullName);
            string notes = ReadNonEmptyStringOrSkip("Новые примечания: ", @"^[а-яА-Яa-zA-Z0-9\s-]+$", bookToUpdate.notes);

            
            string newBookId = GenerateUniqueId(books, title, author);

            Book updatedBook = new Book
            {
                bookId = newBookId,
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

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].bookId == bookIdToUpdate)
                {
                    books[i] = updatedBook;
                    break;
                }
            }

            TextOutput($"Книга с ID '{bookIdToUpdate}' обновлена. Новый ID: '{newBookId}'.");

            return books;
        }

        /// <summary>
        /// Получает непустую строку или возвращает значение по умолчанию, если пользователь нажал Enter.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Введенная строка или значение по умолчанию.</returns>
        static string ReadNonEmptyStringOrSkip(string prompt, string pattern, string defaultValue)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return defaultValue;
                }
            } while (!Regex.IsMatch(input, pattern));

            return input;
        }

        /// <summary>
        /// Получает строку, соответствующую регулярному выражению, или возвращает значение по умолчанию, если пользователь нажал Enter.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="validValues">Допустимые значения.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Введенная строка или значение по умолчанию.</returns>
        static string ReadStringWithValidationOrSkip(string prompt, string pattern, string[] validValues, string defaultValue)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return defaultValue;
                }
            } while (!Regex.IsMatch(input, pattern) || (validValues != null && !validValues.Contains(input)));

            return input;
        }

        /// <summary>
        /// Сортирует книги по выбранным полям.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <returns>Отсортированный массив книг.</returns>
        static Book[] SortBooks(Book[] books)
        {
            TextOutput("Выберите поля для сортировки (можно выбрать несколько, введите номера полей через запятую):");
            TextOutput("1. ФИО автора");
            TextOutput("2. Название");
            TextOutput("3. Жанр");
            TextOutput("4. Издательство");
            TextOutput("5. Год издания");
            TextOutput("6. Цена");
            TextOutput("7. ФИО читателя");

            string choice = Console.ReadLine();
            string[] fieldChoices = choice.Split(',');
            List<int> sortFields = new List<int>();

            foreach (string fieldChoice in fieldChoices)
            {
                if (int.TryParse(fieldChoice.Trim(), out int field) && field >= 1 && field <= 7)
                {
                    sortFields.Add(field);
                }
                else
                {
                    TextOutput($"Неверный выбор: {fieldChoice.Trim()}. Попробуйте снова.");
                }
            }

            if (sortFields.Count == 0)
            {
                TextOutput("Не выбрано ни одного поля для сортировки.");
                return books;
            }

            IOrderedEnumerable<Book> sortedBooks = null;
            foreach (int field in sortFields)
            {
                if (sortedBooks == null)
                {
                    sortedBooks = SortBooksByField(books, field);
                }
                else
                {
                    sortedBooks = SortBooksByField(sortedBooks, field);
                }
            }

            TextOutput("Отсортированный список книг:");
            TextOutput(new string('-', 120));
            TextOutput($" {"Автор",-20} | {"Название",-30} | {"Жанр",-15} | {"Издательство",-15} | {"Год издания",-12} | {"Цена",-10} | {"ФИО читателя",-20} | {"Примечания",-30}");
            TextOutput(new string('-', 120));

            foreach (Book book in sortedBooks)
            {
                TextOutput($"{book.author,-20} | {book.title,-30} | {book.genre,-15} | {book.publisher,-15} | {book.year,-12} | {book.price,-10} | {book.readerFullName,-20} | {book.notes,-30}");
            }

            TextOutput(new string('-', 120));

            return sortedBooks.ToArray();
        }

        /// <summary>
        /// Сортирует книги по указанному полю.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="field">Номер поля для сортировки.</param>
        /// <returns>Отсортированный массив книг.</returns>
        static IOrderedEnumerable<Book> SortBooksByField(IEnumerable<Book> books, int field)
        {
            switch (field)
            {
                case 1:
                    return books.OrderBy(b => b.author);
                case 2:
                    return books.OrderBy(b => b.title);
                case 3:
                    return books.OrderBy(b => b.genre);
                case 4:
                    return books.OrderBy(b => b.publisher);
                case 5:
                    return books.OrderBy(b => b.year);
                case 6:
                    return books.OrderBy(b => b.price);
                case 7:
                    return books.OrderBy(b => b.readerFullName);
                default:
                    throw new ArgumentException("Неверный номер поля");
            }
        }

        /// <summary>
        /// Дополнительно сортирует уже отсортированный массив книг по указанному полю.
        /// </summary>
        /// <param name="books">Отсортированный массив книг.</param>
        /// <param name="field">Номер поля для сортировки.</param>
        /// <returns>Отсортированный массив книг.</returns>
        static IOrderedEnumerable<Book> SortBooksByField(IOrderedEnumerable<Book> books, int field)
        {
            switch (field)
            {
                case 1:
                    return books.ThenBy(b => b.author);
                case 2:
                    return books.ThenBy(b => b.title);
                case 3:
                    return books.ThenBy(b => b.genre);
                case 4:
                    return books.ThenBy(b => b.publisher);
                case 5:
                    return books.ThenBy(b => b.year);
                case 6:
                    return books.ThenBy(b => b.price);
                case 7:
                    return books.ThenBy(b => b.readerFullName);
                default:
                    throw new ArgumentException("Неверный номер поля");
            }
        }

        /// <summary>
        /// Ищет книги по заданным критериям.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void SearchBooks(Book[] books)
        {
            TextOutput("Введите данные для поиска (если не хотите искать по какому-то полю, нажмите Enter):");

            string author = ReadStringOrSkip("Автор: ");
            string title = ReadStringOrSkip("Название: ");
            string genre = ReadStringOrSkip("Жанр: ");
            string readerFullName = ReadStringOrSkip("ФИО читателя: ");

            bool found = false;

            foreach (Book book in books)
            {
                if ((string.IsNullOrEmpty(author) || book.author.IndexOf(author, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(title) || book.title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(genre) || book.genre.IndexOf(genre, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(readerFullName) || book.readerFullName.IndexOf(readerFullName, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    BookListOutput(books);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }

        /// <summary>
        /// Получает строку от пользователя или возвращает пустую строку, если пользователь нажал Enter.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <returns>Введенная строка или пустая строка.</returns>
        static string ReadStringOrSkip(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        /// <summary>
        /// Проверяет, существует ли жанр с указанным именем.
        /// </summary>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="genreName">Имя жанра для проверки.</param>
        /// <returns>True, если жанр существует, иначе False.</returns>
        static bool GenreExists(Genre[] genres, string genreName)
        {
            if (genres.Any(g => g.genreName.Equals(genreName, StringComparison.OrdinalIgnoreCase)))
            { 
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверяет, существует ли издательство с указанным именем.
        /// </summary>
        /// <param name="publishers">Массив издательств.</param>
        /// <param name="publisherName">Имя издательства для проверки.</param>
        /// <returns>True, если издательство существует, иначе False.</returns>
        static bool PublisherExists(Publisher[] publisher, string publisherName)
        {
            if (publisher.Any(g => g.publisherName.Equals(publisherName, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверяет, существует ли способ приобретения с указанным именем.
        /// </summary>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <param name="acquisitionMethodName">Имя способа приобретения для проверки.</param>
        /// <returns>True, если способ приобретения существует, иначе False.</returns>
        static bool AcquisitionMethodExists(AcquisitionMethod[] acquisitionMethod, string acquisitionMethodName)
        {
            if (acquisitionMethod.Any(g => g.acquisitionMethodName.Equals(acquisitionMethodName, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Добавляет новый жанр в массив жанров.
        /// </summary>
        /// <param name="genres">Массив жанров.</param>
        /// <returns>Обновленный массив жанров.</returns>
        static Genre[] AddGenre(Genre[] genres)
        {
            string genreName;

            do
            {
                genreName = ReadNonEmptyString("Название жанра: ", @"^[A-ZА-Я][a-zа-я]*$");
            } while (GenreExists(genres, genreName));

            Genre newGenre = new Genre { genreName = genreName };
            Genre[] updatedGenres = new Genre[genres.Length + 1];
 
            for (int i = 0; i < genres.Length; i++)
            {
                updatedGenres[i] = genres[i];
            }

            updatedGenres[genres.Length] = newGenre;

            Console.WriteLine($"Жанр '{genreName}' добавлен.");

            return updatedGenres;
        }

        /// <summary>
        /// Удаляет жанр из массива жанров.
        /// </summary>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="books">Массив книг.</param>
        /// <returns>Обновленный массив жанров.</returns>
        static Genre[] DeleteGenre(Genre[] genres, Book[] books)
        {
            Console.WriteLine("Список жанров:");
            foreach (Genre genre in genres)
            {
                Console.WriteLine($" - {genre.genreName}");
            }

            string genreNameToDelete;
            do
            {
                genreNameToDelete = ReadNonEmptyString("Введите название жанра, который хотите удалить: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (!GenreExists(genres, genreNameToDelete));

            Genre[] updatedGenres = genres.Where(genre => genre.genreName != genreNameToDelete).ToArray();

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].genre == genreNameToDelete)
                {
                    books[i].genre = "неопределён";
                }
            }

            Console.WriteLine($"Жанр '{genreNameToDelete}' удален.");

            return updatedGenres;
        }

        /// <summary>
        /// Обновляет информацию о жанре.
        /// </summary>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="books">Массив книг.</param>
        /// <returns>Обновленный массив жанров.</returns>
        static Genre[] UpdateGenre(Genre[] genres, Book[] books)
        {
            Console.WriteLine("Список жанров:");
            foreach (Genre genre in genres)
            {
                Console.WriteLine($" - {genre.genreName}");
            }

            string genreNameToUpdate;
            do
            {
                genreNameToUpdate = ReadNonEmptyString("Введите название жанра, который хотите изменить: ", @"^[а-яА-Яa-zA-Z\s-]+$");
            } while (!GenreExists(genres, genreNameToUpdate));

            string newGenreName;
            do
            {
                newGenreName = ReadNonEmptyString("Введите новое название жанра: ", @"^[а-яА-Яa-zA-Z\s-]+$");
            } while (GenreExists(genres, newGenreName));

            for (int i = 0; i < genres.Length; i++)
            {
                if (genres[i].genreName == genreNameToUpdate)
                {
                    genres[i].genreName = newGenreName;
                    break;
                }
            }

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].genre == genreNameToUpdate)
                {
                    books[i].genre = newGenreName;
                }
            }

            Console.WriteLine($"Жанр '{genreNameToUpdate}' изменен на '{newGenreName}'.");

            return genres;
        }

        /// <summary>
        /// Добавляет новое издательство в массив издательств.
        /// </summary>
        /// <param name="publishers">Массив издательств.</param>
        /// <returns>Обновленный массив издательств.</returns>
        static Publisher[] AddPublisher(Publisher[] publishers)
        {
            string publisherName;

            do
            {
                publisherName = ReadNonEmptyString("Название издательства: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (PublisherExists(publishers, publisherName));

            Publisher newPublisher = new Publisher { publisherName = publisherName };
            Publisher[] updatedPublishers = new Publisher[publishers.Length + 1];

            for (int i = 0; i < publishers.Length; i++)
            {
                updatedPublishers[i] = publishers[i];
            }

            updatedPublishers[publishers.Length] = newPublisher;

            Console.WriteLine($"Издательство '{publisherName}' добавлено.");

            return updatedPublishers;
        }

        /// <summary>
        /// Удаляет издательство из массива издательств.
        /// </summary>
        /// <param name="publishers">Массив издательств.</param>
        /// <param name="books">Массив книг.</param>
        /// <returns>Обновленный массив издательств.</returns>
        static Publisher[] DeletePublisher(Publisher[] publishers, Book[] books)
        {
            Console.WriteLine("Список издателей:");
            foreach (Publisher publisher in publishers)
            {
                Console.WriteLine($" - {publisher.publisherName}");
            }

            string publisherNameToDelete;
            do
            {
                publisherNameToDelete = ReadNonEmptyString("Введите название издателя, которого хотите удалить: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (!PublisherExists(publishers, publisherNameToDelete));

            Publisher[] updatedPublishers = publishers.Where(publisher => publisher.publisherName != publisherNameToDelete).ToArray();

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].publisher == publisherNameToDelete)
                {
                    books[i].publisher = "неопределён";
                }
            }

            Console.WriteLine($"Издатель '{publisherNameToDelete}' удален.");

            return updatedPublishers;
        }

        /// <summary>
        /// Обновляет информацию об издательстве.
        /// </summary>
        /// <param name="publishers">Массив издательств.</param>
        /// <param name="books">Массив книг.</param>
        /// <returns>Обновленный массив издательств.</returns>
        static Publisher[] UpdatePublisher(Publisher[] publishers, Book[] books)
        {
            Console.WriteLine("Список издателей:");
            foreach (Publisher publisher in publishers)
            {
                Console.WriteLine($" - {publisher.publisherName}");
            }

            string publisherNameToUpdate;
            do
            {
                publisherNameToUpdate = ReadNonEmptyString("Введите название издателя, которого хотите изменить: ", @"^[а-яА-Яa-zA-Z\s-]+$");
            } while (!PublisherExists(publishers, publisherNameToUpdate));

            string newPublisherName;
            do
            {
                newPublisherName = ReadNonEmptyString("Введите новое название издателя: ", @"^[а-яА-Яa-zA-Z\s-]+$");
            } while (PublisherExists(publishers, newPublisherName));

            for (int i = 0; i < publishers.Length; i++)
            {
                if (publishers[i].publisherName == publisherNameToUpdate)
                {
                    publishers[i].publisherName = newPublisherName;
                    break;
                }
            }

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].publisher == publisherNameToUpdate)
                {
                    books[i].publisher = newPublisherName;
                }
            }

            Console.WriteLine($"Издатель '{publisherNameToUpdate}' изменен на '{newPublisherName}'.");

            return publishers;
        }


        /// <summary>
        /// Добавляет новый способ приобретения в массив способов приобретения.
        /// </summary>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <returns>Обновленный массив способов приобретения.</returns>
        static AcquisitionMethod[] AddAcquisitionMethod(AcquisitionMethod[] acquisitionMethods)
        {
            string methodName;

            do
            {
                methodName = ReadNonEmptyString("Название способа приобретения: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (AcquisitionMethodExists(acquisitionMethods, methodName));

            AcquisitionMethod newMethod = new AcquisitionMethod { acquisitionMethodName = methodName };
            AcquisitionMethod[] updatedMethods = new AcquisitionMethod[acquisitionMethods.Length + 1];

            for (int i = 0; i < acquisitionMethods.Length; i++)
            {
                updatedMethods[i] = acquisitionMethods[i];
            }

            updatedMethods[acquisitionMethods.Length] = newMethod;

            Console.WriteLine($"Способ приобретения '{methodName}' добавлен.");

            return updatedMethods;
        }

        /// <summary>
        /// Удаляет способ приобретения из массива способов приобретения.
        /// </summary>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <param name="books">Массив книг.</param>
        /// <returns>Обновленный массив способов приобретения.</returns>
        static AcquisitionMethod[] DeleteAcquisitionMethod(AcquisitionMethod[] acquisitionMethods, Book[] books)
        {
            Console.WriteLine("Список методов приобретения:");
            foreach (AcquisitionMethod method in acquisitionMethods)
            {
                Console.WriteLine($" - {method.acquisitionMethodName}");
            }

            string methodNameToDelete;
            do
            {
                methodNameToDelete = ReadNonEmptyString("Введите название метода приобретения, который хотите удалить: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (!AcquisitionMethodExists(acquisitionMethods, methodNameToDelete));

            AcquisitionMethod[] updatedMethods = acquisitionMethods.Where(method => method.acquisitionMethodName != methodNameToDelete).ToArray();

            
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].acquisitionMethod == methodNameToDelete)
                {
                    books[i].acquisitionMethod = "неопределён";
                }
            }

            Console.WriteLine($"Метод приобретения '{methodNameToDelete}' удален.");

            return updatedMethods;
        }

        /// <summary>
        /// Обновляет информацию о способе приобретения.
        /// </summary>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <param name="books">Массив книг.</param>
        /// <returns>Обновленный массив способов приобретения.</returns>
        static AcquisitionMethod[] UpdateAcquisitionMethod(AcquisitionMethod[] acquisitionMethods, Book[] books)
        {
            Console.WriteLine("Список способов приобретения:");
            foreach (AcquisitionMethod method in acquisitionMethods)
            {
                Console.WriteLine($" - {method.acquisitionMethodName}");
            }

            string methodNameToUpdate;
            do
            {
                methodNameToUpdate = ReadNonEmptyString("Введите название способа приобретения, который хотите изменить: ", @"^[а-яА-Яa-zA-Z\s-]+$");
            } while (!AcquisitionMethodExists(acquisitionMethods, methodNameToUpdate));

            string newMethodName;
            do
            {
                newMethodName = ReadNonEmptyString("Введите новое название способа приобретения: ", @"^[а-яА-Яa-zA-Z\s-]+$");
            } while (AcquisitionMethodExists(acquisitionMethods, newMethodName));
 
            for (int i = 0; i < acquisitionMethods.Length; i++)
            {
                if (acquisitionMethods[i].acquisitionMethodName == methodNameToUpdate)
                {
                    acquisitionMethods[i].acquisitionMethodName = newMethodName;
                    break;
                }
            }

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].acquisitionMethod == methodNameToUpdate)
                {
                    books[i].acquisitionMethod = newMethodName;
                }
            }

            Console.WriteLine($"Способ приобретения '{methodNameToUpdate}' изменен на '{newMethodName}'.");

            return acquisitionMethods;
        }

        /// <summary>
        /// Генерирует отчет по жанрам и издательствам.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void GenerateGenrePublisherReport(Book[] books)
        {
            var genrePublisherCounts = books
                .GroupBy(b => new { b.genre, b.publisher })
                .Select(g => new
                {
                    Genre = g.Key.genre,
                    Publisher = g.Key.publisher,
                    Count = g.Count()
                })
                .OrderBy(g => g.Genre)
                .ThenBy(g => g.Publisher)
                .ToList();

            using (StreamWriter writer = new StreamWriter("genre_publisher_report.txt"))
            {
                string currentGenre = null;
                int totalCount = 0;

                foreach (var item in genrePublisherCounts)
                {
                    if (item.Genre != currentGenre)
                    {
                        if (currentGenre != null)
                        {
                            writer.WriteLine($"Итого: {totalCount}");
                            writer.WriteLine();
                        }
                        currentGenre = item.Genre;
                        writer.WriteLine(currentGenre);
                        writer.WriteLine("№ п/п\tИздательство\tКоличество книг");
                        totalCount = 0;
                    }

                    writer.WriteLine($"\t{item.Publisher,-20}\t{item.Count}");
                    totalCount += item.Count;
                }

                if (currentGenre != null)
                {
                    writer.WriteLine($"Итого: {totalCount}");
                }
            }
        }

        /// <summary>
        /// Генерирует отчет по читателям и книгам.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void GeneratePersonBooksReport(Book[] books)
        {
            var personBooks = books
                .GroupBy(b => b.readerFullName)
                .OrderBy(g => g.Key)
                .ToList();

            using (StreamWriter writer = new StreamWriter("person_books_report.txt"))
            {
                foreach (var group in personBooks)
                {
                    writer.WriteLine(group.Key);
                    writer.WriteLine("№ п/п\tАвтор\tНазвание");
                    int count = 1;
                    foreach (var book in group.OrderBy(b => b.author))
                    {
                        writer.WriteLine($"{count}\t{book.author,-20}\t{book.title,-30}");
                        count++;
                    }
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// Генерирует отчет по жанрам и книгам.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void GenerateGenreBooksReport(Book[] books)
        {
            var genreBooks = books
                .GroupBy(b => b.genre)
                .OrderBy(g => g.Key)
                .ToList();

            using (StreamWriter writer = new StreamWriter("genre_books_report.txt"))
            {
                foreach (var group in genreBooks)
                {
                    writer.WriteLine(group.Key);
                    writer.WriteLine("№ п/п\tАвтор\tНазвание\tКоличество томов");
                    int count = 1;
                    foreach (var book in group.OrderBy(b => b.author))
                    {
                        writer.WriteLine($"{count}\t{book.author,-20}\t{book.title,-30}\t{book.volumeCount}");
                        count++;
                    }
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// Выводит отчет по жанрам и издательствам в консоль.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void PrintGenrePublisherReport(Book[] books)
        {
            var genrePublisherCounts = books
                .GroupBy(b => new { b.genre, b.publisher })
                .Select(g => new
                {
                    Genre = g.Key.genre,
                    Publisher = g.Key.publisher,
                    Count = g.Count()
                })
                .OrderBy(g => g.Genre)
                .ThenBy(g => g.Publisher)
                .ToList();

            string currentGenre = null;
            int totalCount = 0;

            foreach (var item in genrePublisherCounts)
            {
                if (item.Genre != currentGenre)
                {
                    if (currentGenre != null)
                    {
                        Console.WriteLine($"Итого: {totalCount}");
                        Console.WriteLine(new string('-', 50));
                    }
                    currentGenre = item.Genre;
                    Console.WriteLine(currentGenre);
                    Console.WriteLine("№ п/п\tИздательство\tКоличество книг");
                    totalCount = 0;
                }

                Console.WriteLine($"\t{item.Publisher,-20}\t{item.Count}");
                totalCount += item.Count;
            }

            if (currentGenre != null)
            {
                Console.WriteLine($"Итого: {totalCount}");
            }
        }

        /// <summary>
        /// Выводит отчет по читателям и книгам в консоль.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void PrintPersonBooksReport(Book[] books)
        {
            var personBooks = books
                .GroupBy(b => b.readerFullName)
                .OrderBy(g => g.Key)
                .ToList();

            foreach (var group in personBooks)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine("№ п/п\tАвтор\t\t\tНазвание");
                int count = 1;
                foreach (var book in group.OrderBy(b => b.author))
                {
                    Console.WriteLine($"{count}\t{book.author,-20}\t{book.title,-30}");
                    count++;
                }
                Console.WriteLine(new string('-', 50));
            }
        }

        /// <summary>
        /// Выводит отчет по жанрам и книгам в консоль.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void PrintGenreBooksReport(Book[] books)
        {
            var genreBooks = books
                .GroupBy(b => b.genre)
                .OrderBy(g => g.Key)
                .ToList();

            foreach (var group in genreBooks)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine("№ п/п\tАвтор\t\t\tНазвание\t\tКоличество томов");
                int count = 1;
                foreach (var book in group.OrderBy(b => b.author))
                {
                    Console.WriteLine($"{count}\t{book.author,-20}\t{book.title,-30}\t{book.volumeCount}");
                    count++;
                }
                Console.WriteLine(new string('-', 70));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Сохраняет список книг в файл.
        /// </summary>
        /// <param name="books">Массив книг для сохранения.</param>
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
        /// Загружает список жанров из файла.
        /// </summary>
        /// <returns>Массив жанров.</returns>
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
        /// Сохраняет список жанров в файл.
        /// </summary>
        /// <param name="genres">Массив жанров для сохранения.</param>
        static void SaveGenres(Genre[] genres)
        {
            using (StreamWriter writer = new StreamWriter(GenresFilePath))
            {
                foreach (var genre in genres)
                {
                    writer.WriteLine(genre.genreName);
                }
            }
        }

        /// <summary>
        /// Загружает список издательств из файла.
        /// </summary>
        /// <returns>Массив издательств.</returns>
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
        /// Сохраняет список издательств в файл.
        /// </summary>
        /// <param name="publishers">Массив издательств для сохранения.</param>
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
        /// Загружает список способов приобретения из файла.
        /// </summary>
        /// <returns>Массив способов приобретения.</returns>
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
        /// Сохраняет список способов приобретения в файл.
        /// </summary>
        /// <param name="acquisitionMethods">Массив способов приобретения для сохранения.</param>
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
        /// Возвращает пользователя в главное меню после нажатия любой клавиши.
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
        /// Основное меню программы.
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
            TextOutput("7. Вывести список книг");
            TextOutput("8. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия с книгами.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <param name="publishers">Массив издательств.</param>
        static void HandleBooks(Book[] books, Genre[] genres, AcquisitionMethod[] acquisitionMethods, Publisher[] publishers)
        {
            while (true)
            {
                HandleBooksTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-8]$", "Введена не существующая операция. Введите заново. ");

                switch (choice)
                {
                    case 1:
                        books = AddBook(books, genres, acquisitionMethods);
                        if (Confirm("Сохранить изменения?"))
                        {
                            SaveBooks(books);
                            SaveAcquisitionMethods(acquisitionMethods);
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
                        books = UpdateBook(books, genres, acquisitionMethods, publishers);
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
                        BookListOutput(books);
                        ReturnToMenu();
                        break;
                    case 8:
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
        /// Обрабатывает генерацию отчетов.
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
            TextOutput("4. Вывести список жанров");
            TextOutput("5. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия с жанрами.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="genres">Массив жанров.</param>
        static void HandleGenres(Book[] books, Genre[] genres)
        {
            while (true)
            {
                HandleGenresTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-5]$", "Введена не существующая операция. Введите заново. ");

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
                        GenreListOutput(genres);
                        ReturnToMenu();
                        break;

                    case 5:
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
            TextOutput("4. Вывести список издательств");
            TextOutput("5. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия с издательствами.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="publishers">Массив издательств.</param>
        static void HandlePublishers(Book[] books, Publisher[] publishers)
        {
            while (true)
            {
                HandlePublishersTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-5]$", "Введена не существующая операция. Введите заново. ");

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
                        ReturnToMenu();
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
                        PublisherListOutput(publishers);
                        ReturnToMenu();
                        break;
                    case 5:
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
            TextOutput("4. Вывести список способов приобретения");
            TextOutput("5. Вернуться в главное меню");
        }

        /// <summary>
        /// Обрабатывает действия со способами приобретения.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        static void HandleAcquisitionMethods(Book[] books, AcquisitionMethod[] acquisitionMethods)
        {
            while (true)
            {
                HandleAcquisitionMethodsTextOutput();
                int choice = GetIntPositiveDigit(@"^[1-5]$", "Введена не существующая операция. Введите заново. ");

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
                        AcquisitionMethodListOutput(acquisitionMethods);
                        ReturnToMenu();
                        break;
                    case 5:
                        ClearMenu();
                        Menu();
                        break;
                }
            }
        }

        /// <summary>
        /// Получает положительное целое число, соответствующее заданному регулярному выражению.
        /// </summary>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <returns>Положительное целое число.</returns>
        static int GetIntPositiveDigit(string pattern, string prompt)
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || !(Regex.IsMatch(Convert.ToString(value), pattern)))
            {
                Console.WriteLine(prompt);
            }
            return value;
        }

        /// <summary>
        /// Считывает строку с валидацией по заданному регулярному выражению.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="regexPattern">Регулярное выражение для проверки.</param>
        /// <returns>Валидная строка.</returns>
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
        /// Считывает непустую строку с валидацией по заданному регулярному выражению.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="regexPattern">Регулярное выражение для проверки.</param>
        /// <returns>Валидная строка.</returns>
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
        /// Считывает положительное число с плавающей запятой.
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
        /// Считывает жанр для книги с проверкой на существование.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="genres">Массив жанров.</param>
        /// <returns>Название жанра.</returns>
        static string ReadGenreForBook(string prompt, Genre[] genres)
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

                if (Array.Exists(genres, g => g.genreName.Equals(input, StringComparison.OrdinalIgnoreCase)))
                {
                    return input;
                }

                TextOutput("Ошибка: такого жанра не существует. Попробуйте снова.");
            }
        }

        /// <summary>
        /// Подтверждает действие пользователя.
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
        /// Добавляет новую книгу в список.
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
            string genre = ReadGenreForBook("Жанр: ", genres);

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

            string notes = ReadNonEmptyString("Примечания: ", @"^[А-ЯЁA-Za-z0-9\s-]+$");

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
        /// Генерирует уникальный идентификатор для книги.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="title">Название книги.</param>
        /// <param name="author">Автор книги.</param>
        /// <returns>Уникальный идентификатор.</returns>
        static string GenerateUniqueId(Book[] books, string title, string author)
        {
            string cleanedTitle = Regex.Replace(title, @"\s+", "");
            string cleanedAuthor = Regex.Replace(author, @"\s+", "");
            string baseId = $"ID_{cleanedTitle}_{cleanedAuthor}";

            int counter = 1;
            while (BookExists(books, $"{baseId}_{counter}"))
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
        /// Выводит список издательств.
        /// </summary>
        /// <param name="publishers">Массив издательств.</param>
        static void PublisherListOutput(Publisher[] publishers)
        {
            TextOutput("Список издательств:");
            foreach (var publisher in publishers)
            {
                TextOutput($" - {publisher.publisherName}");
            }
        }
        /// <summary>
        /// Выводит список способов приобретения.
        /// </summary>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        static void AcquisitionMethodListOutput(AcquisitionMethod[] acquisitionMethods)
        {
            TextOutput("Список способов приобретения:");
            foreach (var method in acquisitionMethods)
            {
                TextOutput($" - {method.acquisitionMethodName}");
            }
        }

        /// <summary>
        /// Выводит список жанров.
        /// </summary>
        /// <param name="genres">Массив жанров.</param>
        static void GenreListOutput(Genre[] genres)
        {
            TextOutput("Список жанров:");
            foreach (var genre in genres)
            {
                TextOutput($" - {genre.genreName}");
            }
        }

        /// <summary>
        /// Удаляет книгу из списка.
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

            List<Book> updatedBooksList = new List<Book>();
            foreach (var book in books)
            {
                if (book.bookId != bookIdToDelete)
                {
                    updatedBooksList.Add(book);
                }
            }

            TextOutput($"Книга с ID '{bookIdToDelete}' удалена.");

            return updatedBooksList.ToArray();
        }

        /// <summary>
        /// Проверяет, существует ли книга с указанным ID.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        /// <param name="bookId">ID книги.</param>
        /// <returns>True, если книга существует, иначе False.</returns>
        static bool BookExists(Book[] books, string bookId)
        {
            foreach (var book in books)
            {
                if (book.bookId == bookId)
                {
                    return true;
                }
            }
            return false;
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

            Book bookToUpdate = default;
            foreach (var book in books)
            {
                if (book.bookId == bookIdToUpdate)
                {
                    bookToUpdate = book;
                    break;
                }
            }

            TextOutput("Введите новые данные (если не хотите изменять поле, нажмите Enter):");

            string author = ReadNonEmptyStringOrSkip("Новый автор: ", @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ]\.\s[А-ЯЁ]\.$", bookToUpdate.author);
            string title = ReadNonEmptyStringOrSkip("Новое название: ", @"^[а-яА-Яa-zA-Z0-9\s-]+$", bookToUpdate.title);

            TextOutput("Список жанров:");
            foreach (var genr in genres)
            {
                TextOutput($" - {genr.genreName}");
            }
            string genre = ReadStringWithValidationOrSkip("Новый жанр: ", @"^[а-яА-Яa-zA-Z\s-]+$", genres, bookToUpdate.genre);

            TextOutput("Список издательств:");
            foreach (var publishe in publishers)
            {
                TextOutput($" - {publishe.publisherName}");
            }
            string publisher = ReadStringWithValidationOrSkip("Новое издательство: ", @"^[а-яА-Яa-zA-Z\s-]+$", publishers, bookToUpdate.publisher);

            string year = ReadNonEmptyStringOrSkip("Новый год издания: ", @"^(18[5-9]\d|19\d\d|20[01]\d|202[0-4])$", bookToUpdate.year);
            string volumeCount = ReadNonEmptyStringOrSkip("Новое количество томов: ", @"^([1-9]|[1-9][0-9]|100)$", bookToUpdate.volumeCount);

            TextOutput("Список способов приобретения:");
            foreach (var method in acquisitionMethods)
            {
                TextOutput($" - {method.acquisitionMethodName}");
            }
            string acquisitionMethod = ReadStringWithValidationOrSkip("Новый способ приобретения: ", @"^[а-яА-Яa-zA-Z\s-]+$", acquisitionMethods, bookToUpdate.acquisitionMethod);

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
        /// Считывает строку, которая может быть пустой, и возвращает значение по умолчанию, если строка пуста.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Валидная строка или значение по умолчанию.</returns>
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
        /// Считывает строку с валидацией по заданному регулярному выражению или возвращает значение по умолчанию.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Валидная строка или значение по умолчанию.</returns>
        static string ReadStringWithValidationOrSkip(string prompt, string pattern, Genre[] genres, string defaultValue)
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
            } while (!Regex.IsMatch(input, pattern) || !GenreExists(genres, input));

            return input;
        }

        /// <summary>
        /// Считывает строку с валидацией по заданному регулярному выражению или возвращает значение по умолчанию.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="publishers">Массив издательств.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Валидная строка или значение по умолчанию.</returns>
        static string ReadStringWithValidationOrSkip(string prompt, string pattern, Publisher[] publishers, string defaultValue)
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
            } while (!Regex.IsMatch(input, pattern) || !PublisherExists(publishers, input));

            return input;
        }

        /// <summary>
        /// Считывает строку с валидацией по заданному регулярному выражению или возвращает значение по умолчанию.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <param name="pattern">Регулярное выражение для проверки.</param>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns>Валидная строка или значение по умолчанию.</returns>
        static string ReadStringWithValidationOrSkip(string prompt, string pattern, AcquisitionMethod[] acquisitionMethods, string defaultValue)
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
            } while (!Regex.IsMatch(input, pattern) || !AcquisitionMethodExists(acquisitionMethods, input));

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

            for (int i = 0; i < books.Length - 1; i++)
            {
                for (int j = 0; j < books.Length - i - 1; j++)
                {
                    bool swapNeeded = false;

                    foreach (int field in sortFields)
                    {
                        int comparisonResult = CompareBooksByField(books[j], books[j + 1], field);
                        if (comparisonResult > 0)
                        {
                            swapNeeded = true;
                            break;
                        }
                        else if (comparisonResult < 0)
                        {
                            swapNeeded = false;
                            break;
                        }
                    }

                    if (swapNeeded)
                    {
                        Book temp = books[j];
                        books[j] = books[j + 1];
                        books[j + 1] = temp;
                    }
                }
            }

            TextOutput("Отсортированный список книг:");
            SortBookListOutput(books);

            return books;
        }

        /// <summary>
        /// Сравнивает две книги по заданному полю.
        /// </summary>
        /// <param name="book1">Первая книга.</param>
        /// <param name="book2">Вторая книга.</param>
        /// <param name="field">Поле для сравнения.</param>
        /// <returns>Результат сравнения.</returns>
        static int CompareBooksByField(Book book1, Book book2, int field)
        {
            switch (field)
            {
                case 1: 
                    return string.Compare(book1.author, book2.author, StringComparison.OrdinalIgnoreCase);
                case 2: 
                    return string.Compare(book1.title, book2.title, StringComparison.OrdinalIgnoreCase);
                case 3: 
                    return string.Compare(book1.genre, book2.genre, StringComparison.OrdinalIgnoreCase);
                case 4: 
                    return string.Compare(book1.publisher, book2.publisher, StringComparison.OrdinalIgnoreCase);
                case 5: 
                    return string.Compare(book1.year, book2.year, StringComparison.OrdinalIgnoreCase);
                case 6: 
                    return ComparePrices(book1.price, book2.price);
                case 7: 
                    return string.Compare(book1.readerFullName, book2.readerFullName, StringComparison.OrdinalIgnoreCase);
                default:
                    throw new ArgumentException("Неверный номер поля");
            }
        }

        /// <summary>
        /// Сравнивает цены двух книг.
        /// </summary>
        /// <param name="price1">Цена первой книги.</param>
        /// <param name="price2">Цена второй книги.</param>
        /// <returns>Результат сравнения.</returns>
        static int ComparePrices(string price1, string price2)
        {
            // Преобразуем строки в числа
            if (double.TryParse(price1, out double parsedPrice1) && double.TryParse(price2, out double parsedPrice2))
            {
                return parsedPrice1.CompareTo(parsedPrice2);
            }
            else
            {
                // Если преобразование не удалось, сравниваем как строки
                return string.Compare(price1, price2, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Выводит отсортированный список книг.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void SortBookListOutput(Book[] books)
        {
            TextOutput(new string('-', 150));
            TextOutput($" {"Автор",-20} | {"Название",-30} | {"Жанр",-15} | {"Издательство",-20} | {"Год издания",-20} | {"Цена",-15} | {"ФИО читателя",-20}");
            TextOutput(new string('-', 150));

            foreach (Book book in books)
            {
                TextOutput($"{book.author,-20} | {book.title,-30} | {book.genre,-15} | {book.publisher,-20} | {book.year,-20} | {book.price,-15} | {book.readerFullName,-20}");
            }

            TextOutput(new string('-', 150));
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

                bool isAuthorMatch = string.IsNullOrEmpty(author) || IsAuthorMatch(book.author, author);
                bool isTitleMatch = string.IsNullOrEmpty(title) || IsTitleMatch(book.title, title);
                bool isGenreMatch = string.IsNullOrEmpty(genre) || IsGenreMatch(book.genre, genre);
                bool isReaderMatch = string.IsNullOrEmpty(readerFullName) || IsReaderMatch(book.readerFullName, readerFullName);

                if (isAuthorMatch && isTitleMatch && isGenreMatch && isReaderMatch)
                {
                    BookListOutput(new Book[] { book });
                    found = true;
                }
            }

            if (!found)
            {
                TextOutput("Ничего не найдено.");
            }
        }

        /// <summary>
        /// Проверяет, совпадает ли автор книги с заданным значением.
        /// </summary>
        /// <param name="bookField">Автор книги.</param>
        /// <param name="searchField">Искомый автор.</param>
        /// <returns>True, если совпадает, иначе False.</returns>
        static bool IsAuthorMatch(string bookField, string searchField)
        {

            string normalizedBookField = NormalizeAuthor(bookField);
            string normalizedSearchField = NormalizeAuthor(searchField);


            return normalizedBookField.Equals(normalizedSearchField, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Нормализует строку автора.
        /// </summary>
        /// <param name="author">Строка автора.</param>
        /// <returns>Нормализованная строка.</returns>
        static string NormalizeAuthor(string author)
        {

            string normalized = Regex.Replace(author, @"\s+|\.", " ").Trim();


            string[] parts = normalized.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
                return normalized;


            return $"{parts[0]} {parts[1]} {parts[2]}";
        }

        /// <summary>
        /// Проверяет, совпадает ли название книги с заданным значением.
        /// </summary>
        /// <param name="bookTitle">Название книги.</param>
        /// <param name="searchTitle">Искомое название.</param>
        /// <returns>True, если совпадает, иначе False.</returns>
        static bool IsTitleMatch(string bookTitle, string searchTitle)
        {
            return string.IsNullOrEmpty(searchTitle) || bookTitle.Equals(searchTitle, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Проверяет, совпадает ли жанр книги с заданным значением.
        /// </summary>
        /// <param name="bookGenre">Жанр книги.</param>
        /// <param name="searchGenre">Искомый жанр.</param>
        /// <returns>True, если совпадает, иначе False.</returns>
        static bool IsGenreMatch(string bookGenre, string searchGenre)
        {
            return string.IsNullOrEmpty(searchGenre) || bookGenre.Equals(searchGenre, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Проверяет, совпадает ли ФИО читателя с заданным значением.
        /// </summary>
        /// <param name="bookReader">ФИО читателя.</param>
        /// <param name="searchReader">Искомое ФИО.</param>
        /// <returns>True, если совпадает, иначе False.</returns>
        static bool IsReaderMatch(string bookReader, string searchReader)
        {
            return string.IsNullOrEmpty(searchReader) || IsAuthorMatch(bookReader, searchReader);
        }

        /// <summary>
        /// Считывает строку, которая может быть пустой.
        /// </summary>
        /// <param name="prompt">Сообщение для пользователя.</param>
        /// <returns>Строка.</returns>
        static string ReadStringOrSkip(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        /// <summary>
        /// Проверяет, существует ли жанр с указанным названием.
        /// </summary>
        /// <param name="genres">Массив жанров.</param>
        /// <param name="genreName">Название жанра.</param>
        /// <returns>True, если жанр существует, иначе False.</returns>
        static bool GenreExists(Genre[] genres, string genreName)
        {
            foreach (var genre in genres)
            {
                if (genre.genreName.Equals(genreName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет, существует ли издательство с указанным названием.
        /// </summary>
        /// <param name="publishers">Массив издательств.</param>
        /// <param name="publisherName">Название издательства.</param>
        /// <returns>True, если издательство существует, иначе False.</returns>
        static bool PublisherExists(Publisher[] publishers, string publisherName)
        {
            foreach (var publisher in publishers)
            {
                if (publisher.publisherName.Equals(publisherName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет, существует ли способ приобретения с указанным названием.
        /// </summary>
        /// <param name="acquisitionMethods">Массив способов приобретения.</param>
        /// <param name="acquisitionMethodName">Название способа приобретения.</param>
        /// <returns>True, если способ приобретения существует, иначе False.</returns>
        static bool AcquisitionMethodExists(AcquisitionMethod[] acquisitionMethods, string acquisitionMethodName)
        {
            foreach (var method in acquisitionMethods)
            {
                if (method.acquisitionMethodName.Equals(acquisitionMethodName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Добавляет новый жанр в список.
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
        /// Удаляет жанр из списка.
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

            List<Genre> updatedGenresList = new List<Genre>();
            foreach (var genre in genres)
            {
                if (genre.genreName != genreNameToDelete)
                {
                    updatedGenresList.Add(genre);
                }
            }

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].genre == genreNameToDelete)
                {
                    books[i].genre = "неопределён";
                }
            }

            Console.WriteLine($"Жанр '{genreNameToDelete}' удален.");

            return updatedGenresList.ToArray();
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
        /// Добавляет новое издательство в список.
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
        /// Удаляет издательство из списка.
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

            List<Publisher> updatedPublishersList = new List<Publisher>();
            foreach (var publisher in publishers)
            {
                if (publisher.publisherName != publisherNameToDelete)
                {
                    updatedPublishersList.Add(publisher);
                }
            }

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].publisher == publisherNameToDelete)
                {
                    books[i].publisher = "неопределён";
                }
            }

            Console.WriteLine($"Издатель '{publisherNameToDelete}' удален.");

            return updatedPublishersList.ToArray();
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
        /// Добавляет новый способ приобретения в список.
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
        /// Удаляет способ приобретения из списка.
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

            List<AcquisitionMethod> updatedMethodsList = new List<AcquisitionMethod>();
            foreach (var method in acquisitionMethods)
            {
                if (method.acquisitionMethodName != methodNameToDelete)
                {
                    updatedMethodsList.Add(method);
                }
            }

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].acquisitionMethod == methodNameToDelete)
                {
                    books[i].acquisitionMethod = "неопределён";
                }
            }

            Console.WriteLine($"Метод приобретения '{methodNameToDelete}' удален.");

            return updatedMethodsList.ToArray();
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
            Dictionary<string, Dictionary<string, int>> genrePublisherCounts = new Dictionary<string, Dictionary<string, int>>();
            foreach (var book in books)
            {
                if (!genrePublisherCounts.ContainsKey(book.genre))
                {
                    genrePublisherCounts[book.genre] = new Dictionary<string, int>();
                }
                if (!genrePublisherCounts[book.genre].ContainsKey(book.publisher))
                {
                    genrePublisherCounts[book.genre][book.publisher] = 0;
                }
                genrePublisherCounts[book.genre][book.publisher]++;
            }

            using (StreamWriter writer = new StreamWriter("genre_publisher_report.txt"))
            {
                string currentGenre = null;
                int totalCount = 0;

                foreach (var genre in genrePublisherCounts.Keys)
                {
                    if (genre != currentGenre)
                    {
                        if (currentGenre != null)
                        {
                            writer.WriteLine($"Итого: {totalCount}");
                            writer.WriteLine();
                        }
                        currentGenre = genre;
                        writer.WriteLine(currentGenre);
                        writer.WriteLine("№ п/п\tИздательство\tКоличество книг");
                        totalCount = 0;
                    }

                    foreach (var publisher in genrePublisherCounts[genre].Keys)
                    {
                        writer.WriteLine($"\t{publisher,-20}\t{genrePublisherCounts[genre][publisher]}");
                        totalCount += genrePublisherCounts[genre][publisher];
                    }
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
            Dictionary<string, List<Book>> personBooks = new Dictionary<string, List<Book>>();
            foreach (var book in books)
            {
                if (!personBooks.ContainsKey(book.readerFullName))
                {
                    personBooks[book.readerFullName] = new List<Book>();
                }
                personBooks[book.readerFullName].Add(book);
            }

            using (StreamWriter writer = new StreamWriter("person_books_report.txt"))
            {
                foreach (var reader in personBooks.Keys)
                {
                    writer.WriteLine(reader);
                    writer.WriteLine("№ п/п\tАвтор\tНазвание");
                    int count = 1;
                    foreach (var book in personBooks[reader])
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
            Dictionary<string, List<Book>> genreBooks = new Dictionary<string, List<Book>>();
            foreach (var book in books)
            {
                if (!genreBooks.ContainsKey(book.genre))
                {
                    genreBooks[book.genre] = new List<Book>();
                }
                genreBooks[book.genre].Add(book);
            }

            using (StreamWriter writer = new StreamWriter("genre_books_report.txt"))
            {
                foreach (var genre in genreBooks.Keys)
                {
                    writer.WriteLine(genre);
                    writer.WriteLine("№ п/п\tАвтор\tНазвание\tКоличество томов");
                    int count = 1;
                    foreach (var book in genreBooks[genre])
                    {
                        writer.WriteLine($"{count}\t{book.author,-20}\t{book.title,-30}\t{book.volumeCount}");
                        count++;
                    }
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// Выводит отчет по жанрам и издательствам.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void PrintGenrePublisherReport(Book[] books)
        {
            Dictionary<string, Dictionary<string, int>> genrePublisherCounts = new Dictionary<string, Dictionary<string, int>>();
            foreach (var book in books)
            {
                if (!genrePublisherCounts.ContainsKey(book.genre))
                {
                    genrePublisherCounts[book.genre] = new Dictionary<string, int>();
                }
                if (!genrePublisherCounts[book.genre].ContainsKey(book.publisher))
                {
                    genrePublisherCounts[book.genre][book.publisher] = 0;
                }
                genrePublisherCounts[book.genre][book.publisher]++;
            }

            string currentGenre = null;
            int totalCount = 0;

            foreach (var genre in genrePublisherCounts.Keys)
            {
                if (genre != currentGenre)
                {
                    if (currentGenre != null)
                    {
                        Console.WriteLine($"Итого: {totalCount}");
                        Console.WriteLine(new string('-', 50));
                    }
                    currentGenre = genre;
                    Console.WriteLine(currentGenre);
                    Console.WriteLine("№ п/п\tИздательство\tКоличество книг");
                    totalCount = 0;
                }

                foreach (var publisher in genrePublisherCounts[genre].Keys)
                {
                    Console.WriteLine($"\t{publisher,-20}\t{genrePublisherCounts[genre][publisher]}");
                    totalCount += genrePublisherCounts[genre][publisher];
                }
            }

            if (currentGenre != null)
            {
                Console.WriteLine($"Итого: {totalCount}");
            }
        }

        /// <summary>
        /// Выводит отчет по читателям и книгам.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void PrintPersonBooksReport(Book[] books)
        {
            Dictionary<string, List<Book>> personBooks = new Dictionary<string, List<Book>>();
            foreach (var book in books)
            {
                if (!personBooks.ContainsKey(book.readerFullName))
                {
                    personBooks[book.readerFullName] = new List<Book>();
                }
                personBooks[book.readerFullName].Add(book);
            }

            foreach (var reader in personBooks.Keys)
            {
                Console.WriteLine(reader);
                Console.WriteLine("№ п/п\tАвтор\t\t\tНазвание");
                int count = 1;
                foreach (var book in personBooks[reader])
                {
                    Console.WriteLine($"{count}\t{book.author,-20}\t{book.title,-30}");
                    count++;
                }
                Console.WriteLine(new string('-', 50));
            }
        }

        /// <summary>
        /// Выводит отчет по жанрам и книгам.
        /// </summary>
        /// <param name="books">Массив книг.</param>
        static void PrintGenreBooksReport(Book[] books)
        {
            Dictionary<string, List<Book>> genreBooks = new Dictionary<string, List<Book>>();
            foreach (var book in books)
            {
                if (!genreBooks.ContainsKey(book.genre))
                {
                    genreBooks[book.genre] = new List<Book>();
                }
                genreBooks[book.genre].Add(book);
            }

            foreach (var genre in genreBooks.Keys)
            {
                Console.WriteLine(genre);
                Console.WriteLine("№ п/п\tАвтор\t\t\tНазвание\t\tКоличество томов");
                int count = 1;
                foreach (var book in genreBooks[genre])
                {
                    Console.WriteLine($"{count}\t{book.author,-20}\t{book.title,-30}\t{book.volumeCount}");
                    count++;
                }
                Console.WriteLine(new string('-', 70));
            }
        }
    }
}
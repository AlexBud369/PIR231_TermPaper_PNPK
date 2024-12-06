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

        static void ReturnToMenu()
        {
            TextOutput("Для возвращения на главное меню нажмите любую кнопку");
            Console.ReadKey();
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



        static void HandleBooks(Book[] books, Genre[] genres, AcquisitionMethod[] acequisitationMethods, Publisher[] publishers)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие для книг:");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу");
                Console.WriteLine("3. Редактировать книгу");
                Console.WriteLine("4. Сортировать книгу");
                Console.WriteLine("5. Вернуться в главное меню");

                int choice = GetIntPositiveDigit(@"^[1-5]$", "Введена не существующая операция. Введите заново. ");

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
                        ClearMenu();
                        Menu();
                        break; 
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandleGenres(Book[] books, Genre[] genres)
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
                            SaveBooks(books); // Сохраняем обновленные данные о книгах
                        }
                        ReturnToMenu();
                        break;
                    case 4:
                        ClearMenu();
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandlePublishers(Book[] books, Publisher[] publishers)
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
                            SaveBooks(books); // Сохраняем обновленные данные о книгах
                        }
                        ReturnToMenu();
                        break;
                    case 4:
                        ClearMenu();
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void HandleAcquisitionMethods(Book[] books, AcquisitionMethod[] acquisitionMethods)
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
                            SaveBooks(books); // Сохраняем обновленные данные о книгах
                        }
                        ReturnToMenu();
                        break;
                    case 4:
                        ClearMenu();
                        Menu();
                        break;
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


        static string ReadStringWithValidation(string prompt, string regexPattern)
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
                    return input;
                    /* if (validValues.Length == 0 || validValues.Contains(input))
                     {
                         return input;
                     }
                     else
                     {
                         Console.WriteLine("Ошибка: введите значение из предложенного списка.");
                     }
                 }*/
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



        static Book[] AddBook(Book[] books, Genre[] genres, AcquisitionMethod[] acquisitionMethods)
        {
            string author = ReadNonEmptyString("Автор: ", @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ]\.\s[А-ЯЁ]\.$");

            string title = ReadNonEmptyString("Название: ", @"^[А-ЯЁA-Za-z0-9\s-]+$");

            Console.WriteLine("Список жанров:");
            foreach (var genr in genres)
            {
                Console.WriteLine(" - " + genr.genreName);
            }
            string genre = ReadStringWithValidation("Жанр: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");

            string publisher = ReadStringWithValidation("Издательство: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");



            TextOutput("Год издания: ");
            int year = GetIntPositiveDigit(@"^(18[5-9]\d|19\d\d|20[01]\d|202[0-4])$", "Введен недопустимый год. Введите заново.");

            TextOutput("Количество томов: ");
            int volumeCount = GetIntPositiveDigit(@"^([1-9]|[1-9][0-9]|100)$", "Введено недопустимое количество томов. Введите заново. ");

            Console.WriteLine("Список способов приобретения:");
            foreach (var method in acquisitionMethods)
            {
                Console.WriteLine(" - " + method.acquisitionMethodName);
            }
            string acquisitionMethod = ReadStringWithValidation("Способ приобретения: ", @"^[A-Za-zА-Яа-я]+([\s-][A-Za-zА-Яа-я]+)*$");
            /*if (!acquisitionMethods.Any(m => m.acquisitionMethodName == acquisitionMethod))
            {
                Array.Resize(ref acquisitionMethods, acquisitionMethods.Length + 1);
                acquisitionMethods[acquisitionMethods.Length - 1] = new AcquisitionMethod { acquisitionMethodName = acquisitionMethod };
            }*/


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

        static string GenerateUniqueId(Book[] books, string title, string author)
        {
            // Удаление пробелов из названия книги и автора
            string cleanedTitle = Regex.Replace(title, @"\s+", "");
            string cleanedAuthor = Regex.Replace(author, @"\s+", "");
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

        static Book[] DeleteBook(Book[] books)
        {
            Console.WriteLine("Список книг:");
            foreach (Book book in books)
            {
                Console.WriteLine($"ID: {book.bookId}");
                Console.WriteLine($"Автор: {book.author}");
                Console.WriteLine($"Название: {book.title}");
                Console.WriteLine($"Жанр: {book.genre}");
                Console.WriteLine($"Издательство: {book.publisher}");
                Console.WriteLine($"Год издания: {book.year}");
                Console.WriteLine($"Количество томов: {book.volumeCount}");
                Console.WriteLine($"Способ приобретения: {book.acquisitionMethod}");
                Console.WriteLine($"Цена: {book.price}");
                Console.WriteLine($"ФИО читателя: {book.readerFullName}");
                Console.WriteLine($"Примечания: {book.notes}");
                Console.WriteLine();
            }

            string bookIdToDelete;
            do
            {
                bookIdToDelete = ReadNonEmptyString("Введите ID книги, которую хотите удалить: ", @"^[a-zA-Z0-9\-_]+$");
            } while (!BookExists(books, bookIdToDelete));

            Book[] updatedBooks = books.Where(book => book.bookId != bookIdToDelete).ToArray();

            Console.WriteLine($"Книга с ID '{bookIdToDelete}' удалена.");

            return updatedBooks;
        }

        static bool BookExists(Book[] books, string bookId)
        {
            return books.Any(book => book.bookId == bookId);
        }

        static Book[] UpdateBook(Book[] books, Genre[] genres, AcquisitionMethod[] acquisitionMethods, Publisher[] publishers)
        {
            Console.WriteLine("Список книг:");
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

            // Генерация нового ID, если изменился автор или название
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


        static Book[] SortBooks(Book[] books)
        {
            Console.WriteLine("Выберите поле для сортировки:");
            Console.WriteLine("1. ФИО автора");
            Console.WriteLine("2. Название");
            Console.WriteLine("3. Жанр");
            Console.WriteLine("4. Издательство");
            Console.WriteLine("5. Год издания");
            Console.WriteLine("6. Цена");
            Console.WriteLine("7. ФИО читателя");

            List<int> sortFields = new List<int>();
            while (true)
            {
                Console.Write("Введите номер поля (или 0 для завершения выбора): ");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int field) && field >= 0 && field <= 7)
                {
                    if (field == 0) break;
                    sortFields.Add(field);
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                }
            }

            if (sortFields.Count == 0)
            {
                Console.WriteLine("Не выбрано ни одного поля для сортировки.");
                return books;
            }

            Book[] sortedBooks = books;
            foreach (int field in sortFields)
            {
                sortedBooks = SortByField(sortedBooks, field);
            }

            Console.WriteLine("Отсортированный список книг:");
            foreach (Book book in sortedBooks)
            {
                Console.WriteLine($"{book.author}; {book.title}; {book.genre}; {book.publisher}; {book.year}; {book.price}; {book.readerFullName}");
            }

            return sortedBooks;
        }

        static Book[] SortByField(Book[] books, int field)
        {
            switch (field)
            {
                case 1:
                    return books.OrderBy(b => b.author).ToArray();
                case 2:
                    return books.OrderBy(b => b.title).ToArray();
                case 3:
                    return books.OrderBy(b => b.genre).ToArray();
                case 4:
                    return books.OrderBy(b => b.publisher).ToArray();
                case 5:
                    return books.OrderBy(b => b.year).ToArray();
                case 6:
                    return books.OrderBy(b => b.price).ToArray();
                case 7:
                    return books.OrderBy(b => b.readerFullName).ToArray();
                default:
                    throw new ArgumentException("Неверный номер поля");
            }
        }

        static bool GenreExists(Genre[] genres, string genreName)
        {
            if (genres.Any(g => g.genreName.Equals(genreName, StringComparison.OrdinalIgnoreCase)))
            {
                
                return true;
            }
            return false;
        }

        static bool PublisherExists(Publisher[] publisher, string publisherName)
        {
            if (publisher.Any(g => g.publisherName.Equals(publisherName, StringComparison.OrdinalIgnoreCase)))
            {
                //Console.WriteLine("Такой издатель уже существует. Пожалуйста, введите другого издателя.");
                return true;
            }
            return false;
        }

        static bool AcquisitionMethodExists(AcquisitionMethod[] acquisitionMethod, string acquisitionMethodName)
        {
            if (acquisitionMethod.Any(g => g.acquisitionMethodName.Equals(acquisitionMethodName, StringComparison.OrdinalIgnoreCase)))
            {
               // Console.WriteLine("Такой способ приобретения уже существует. Пожалуйста, введите другой способ приобретения.");
                return true;
            }
            return false;
        }


        static Genre[] AddGenre(Genre[] genres)
        {
            string genreName;

            do
            {
                genreName = ReadNonEmptyString("Название жанра: ", @"^[A-ZА-Я][a-zа-я]*$");
            } while (GenreExists(genres, genreName));

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

            return updatedGenres;
        }

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

            // Обновляем данные о книгах, у которых был удаленный жанр
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

            // Обновляем данные о жанрах
            for (int i = 0; i < genres.Length; i++)
            {
                if (genres[i].genreName == genreNameToUpdate)
                {
                    genres[i].genreName = newGenreName;
                    break;
                }
            }

            // Обновляем данные о книгах, у которых был измененный жанр
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

        static Publisher[] AddPublisher(Publisher[] publishers)
        {
            string publisherName;

            do
            {
                publisherName = ReadNonEmptyString("Название издательства: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (PublisherExists(publishers, publisherName));

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

            return updatedPublishers;
        }


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

            // Обновляем данные о книгах, у которых был удаленный издатель
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

            // Обновляем данные об издателях
            for (int i = 0; i < publishers.Length; i++)
            {
                if (publishers[i].publisherName == publisherNameToUpdate)
                {
                    publishers[i].publisherName = newPublisherName;
                    break;
                }
            }

            // Обновляем данные о книгах, у которых был измененный издатель
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



        static AcquisitionMethod[] AddAcquisitionMethod(AcquisitionMethod[] acquisitionMethods)
        {
            string methodName;

            do
            {
                methodName = ReadNonEmptyString("Название способа приобретения: ", @"^[A-ZА-Я][a-zа-я]*([s-][A-ZА-Я][a-zа-я]*)*$");
            } while (AcquisitionMethodExists(acquisitionMethods, methodName));

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

            return updatedMethods;
        }
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

            // Обновляем данные о книгах, у которых был удаленный метод приобретения
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

            // Обновляем данные о способах приобретения
            for (int i = 0; i < acquisitionMethods.Length; i++)
            {
                if (acquisitionMethods[i].acquisitionMethodName == methodNameToUpdate)
                {
                    acquisitionMethods[i].acquisitionMethodName = newMethodName;
                    break;
                }
            }

            // Обновляем данные о книгах, у которых был измененный способ приобретения
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




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPaper_PNPK
{
    internal class Program
    {
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

        public struct AcquisitionMethod
        {
            public string Name;
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

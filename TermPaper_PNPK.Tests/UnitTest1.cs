using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using TermPaper_PNPK;
using NUnit.Framework;

namespace TermPaper_PNPK.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGenerateUniqueId()
        {
            Book[] books = new Book[]
           {
                new Book { bookId = "ID_Title1_Author1_1", title = "Title1", author = "Author1" },
                new Book { bookId = "ID_Title2_Author2_1", title = "Title2", author = "Author2" }
           };

            string title = "Title1";
            string author = "Author1";

            // Act
            string uniqueId = .GenerateUniqueId(books, title, author);

            // Assert
            Assert.AreEqual("ID_Title1_Author1_2", uniqueId);
        }
    }
}
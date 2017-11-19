using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wpfMozaiq.Models;
using System.IO;

namespace wpfMozaiqTests
{
    [TestClass]
    public class CatalogTests
    {

        Catalog DefaultCatalog =new
        Catalog("Bisazza", 10,
        Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
        .Replace("Tests", ""));
        

        [TestMethod]
        public void CreateCorrectCatalog()
        {
            Catalog newCatalog = new Catalog("Bisazza",20, 
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
                .Replace("Tests",""));

            Assert.AreNotEqual(newCatalog.Mozaics.Count, 0);
        }

        [TestMethod]
        public void CreateIncorrectCatalog()
        {
            Catalog newCatalog = new Catalog("Incorrect", 20, 
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
                .Replace("Tests", ""));

            Assert.AreEqual(newCatalog.Mozaics.Count, 0);
        }


        [TestMethod]
        public void DisableMozaic()
        {
            
            int size = DefaultCatalog.Mozaics.Count;

            DefaultCatalog.DisableMozaic("10.02(4)-1.bmp", "LeGemme");
            int newSize = DefaultCatalog.Mozaics.Count;

            Assert.AreEqual(size-1, newSize);
        }


        [TestMethod]
        public void EnableMozaic()
        {

            DefaultCatalog.Mozaics.Clear();
            DefaultCatalog.EnableMozaic("10.02(4)-1.bmp", "LeGemme");
            
            Assert.AreEqual(DefaultCatalog.Mozaics[0].Name, "10.02(4)-1.bmp");
        }


    }
}

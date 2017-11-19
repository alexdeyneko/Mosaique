using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wpfMozaiq.Models;
using System.IO;
using System.Collections.Generic;

namespace wpfMozaiqTests
{
    [TestClass]
    public class CatalogTests
    {

        private string PathToCatalog =
            Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
                .Replace("Tests", "");

        [TestMethod]
        public void CreateCorrectCatalog()
        {
            Catalog catalog = new Catalog("Bisazza",20, PathToCatalog);

            Assert.AreNotEqual(catalog.Mozaics.Count, 0);
        }

        [TestMethod]
        public void CreateIncorrectCatalog()
        {
            Catalog catalog = new Catalog("Incorrect", 20, PathToCatalog);
            
            Assert.AreEqual(catalog.Mozaics.Count, 0);
        }


        [TestMethod]
        public void DisableMozaic()
        {
            string name = "10.02(4)-1.bmp";
            string subCatalog = "";

            Catalog catalog = new Catalog("", 10, new List<Mozaic>());
            Mozaic mozaic = new Mozaic();
            mozaic.Name = name;
            mozaic.SubCatalog = subCatalog;
            catalog.Mozaics.Add(mozaic);

            int size = catalog.Mozaics.Count;
            catalog.DisableMozaic(name,subCatalog);
            int newSize = catalog.Mozaics.Count;

            Assert.AreEqual(size-1, newSize);
        }


        [TestMethod]
        public void EnableMozaic()
        {
            Catalog catalog = new Catalog("", 10, new List<Mozaic>());
            Mozaic mozaic = new Mozaic();
            catalog.EnableMozaic(mozaic.Name, mozaic.SubCatalog);

        }


    }
}

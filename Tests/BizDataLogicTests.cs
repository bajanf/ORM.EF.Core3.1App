using ConsoleApp;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class BizDataLogicTests
    {
        [TestMethod]
        public void AddMultipleSamuraiReturnsCorrectNumberOfInsertedRows()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("addmultiplesamurais");

            using(var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BussinessDataLogic(context);
                var namelist = new string[] { "kiki", "ricki", "miki" };
                var result = bizlogic.AddMultipleSamurais(namelist);
                Assert.AreEqual(namelist.Count(), result);
            }
        }


        [TestMethod]
        public void CanInsertSingleSamurai()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("addmultiplesamurais");

            using (var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BussinessDataLogic(context);
                bizlogic.InsertNewSamurai(new Samurai());
            }
            using(var context2 = new SamuraiContext(builder.Options))
            {
                Assert.AreEqual(1, context2.Samurais.Count());
            }
        }
        }
    
        
       
    
}

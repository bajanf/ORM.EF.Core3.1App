using Data;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            using(var context=new SamuraiContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                Debug.WriteLine($"Before save: {samurai.Id}");

                context.SaveChanges();
                Debug.WriteLine($"After save: {samurai.Id}");

                Assert.AreNotEqual(0, samurai.Id);
            }
        }
    }
}

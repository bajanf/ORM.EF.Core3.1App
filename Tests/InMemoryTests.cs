using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    class InMemoryTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("caninsertsamurai");

            using (var context = new Data.SamuraiContext(builder.Options))
            {
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                Assert.AreNotEqual(EntityState.Added, context.Entry(samurai).State);
            }
        }
    }
}

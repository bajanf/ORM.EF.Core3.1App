using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiContextNoTracking _context = new SamuraiContextNoTracking();
        static void Main(string[] args)
        {
            //context.Database.EnsureCreated();
            //AddSamurais();
            //InsertMultipleSamurais();
            //GetSamuraisSimpler();
            //QueryFilters();
            GetSamurais("before:");
            //RetriveAndUpdaqteSamurai();
            //RetriveAndUpdateMultipleSamurais();
            //RetriveAndDeleteeSamurais(1);
            //InsertBattle();
            QueryAndUpdateBattle_Disconect();
            GetBattles();
            Console.WriteLine("press any key...");
            Console.ReadKey();
        }

        private static void GetBattles()
        {
            var battles = _context.Battles.ToList();
            foreach (var battle in battles)
            {
                Console.WriteLine(battle.Name);
            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconect()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 02);
            using(var newContextInstance=new SamuraiContextNoTracking())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }
        
        private static void RetriveAndDeleteeSamurais(int index)
        {
            var samurai = _context.Samurais.Find(index);
            if (samurai != null)
            {
                _context.Samurais.Remove(samurai);
                _context.SaveChanges();
            }
        }

        private static void RetriveAndUpdateMultipleSamurais()
        {
            //skip first and take the next 3 samurai from the list
            var samurais = _context.Samurais.Skip(1).Take(3).ToList();
            samurais.ForEach(s => s.Name += "Fla");
            var newSamurai= new Samurai { Name = "Ionela" };
            _context.Samurais.Add(newSamurai);
            _context.SaveChanges();
        }

        private static void RetriveAndUpdaqteSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "SAM";
            _context.SaveChanges();
        }

        private static void QueryFilters()
        {
            //var samurais = _context.Samurais.Where(s => s.Name == "Tasha").ToList();
            //var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "S%")).ToList();
            //var samurais = _context.Samurais.Where(s => s.Name == "Tasha").FirstOrDefault();
            //var samurais = _context.Samurais.FirstOrDefault(s => s.Name == "Tasha");
            //var samurais = _context.Samurais.Find(2);
            var last = _context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == "Tasha");
        }

        public static void GetSamuraisSimpler()
        {
            //var samurais = context.Samurais.ToList();
            var query = _context.Samurais;
            var samurais = query.ToList();
            foreach (var samurai in query)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai1 = new Samurai { Name = "Sampson" };
            var samurai2 = new Samurai { Name = "Tasha" };
            var samurai3 = new Samurai { Name = "Number3" };
            var samurai4 = new Samurai { Name = "Number3" };
            _context.Samurais.AddRange(samurai1, samurai2, samurai3, samurai4);
            _context.SaveChanges();
        }

        private static void AddSamurais()
        {
            var samurai = new Samurai { Name = "Sampson" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
    }
}

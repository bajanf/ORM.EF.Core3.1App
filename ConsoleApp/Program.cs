using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            //GetSamurais("before:");
            //RetriveAndUpdaqteSamurai();
            //RetriveAndUpdateMultipleSamurais();
            //RetriveAndDeleteeSamurais(1);
            //InsertBattle();
            //QueryAndUpdateBattle_Disconect();
            //GetBattles();

            //InsertNewSamuraiWithQuotes();
            //AddQuoteToAnExistingSamuraiWhileTracked();
            //AddQuoteToAnExistingSamuraiWNotTracked(3);
            //AddQuoteToAnExistingSamuraiWNotTracked_Easy(4);
            //EagerLoadSamuraiWithQuates();
            //ProjectSomeProperties();
            //ProjectSamuraisWithQuotes();
            //ExplicitLoadQuates();
            //FilteringWithRelatedQuates();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();

            //(N:N) relashonship
            //JoinBattleAndSamurai();//that already exist
            //EnlistSamuraiIntoBattle();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            //GetSamuraiWithBattles();

            //(1:1) relashonship
            //AddNewHorseToAnExistingSamuraiUsingId();
            //AddNewHorseToAnExistingSamuraiObject();
            //GetHourseWithSamurai();
            //InsertSamuraiInNewClan();
            //InsertSamuraiInExistingClan();
            GetSamuraiWithClan();
            GetClanWithSamurais();


            Console.WriteLine("press any key...");
            Console.ReadKey();
        }

        private static void InsertSamuraiInNewClan()
        {
            var clan = new Clan { ClanName = "PowerPuff"};
            var samurai = _context.Samurais.AsNoTracking().First(s=>s.Id==10);
            samurai.Clan=clan;
            using (var newContext = new SamuraiContextNoTracking())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }
        private static void InsertSamuraiInExistingClan()
        {
            var clan = _context.Set<Clan>().Find(1);
            var samurai = _context.Samurais.First(s => s.Id == 9);
            samurai.Clan = clan;
            using (var newContext = new SamuraiContextNoTracking())
            {
                newContext.Attach(samurai);
                newContext.Update(samurai);
                newContext.SaveChanges();
            }
        }

        private static void GetClanWithSamurais()
        {
            var samuraiWithClan = _context.Samurais
                .Include(s => s.Clan)
                .Where(s => s.Clan != null)
                .ToList();
        }

        private static void GetSamuraiWithClan()
        {
            var clan = _context.Clans.Find(3);
            var samuraiForClans = _context.Samurais.Where(s => s.Clan.Id == clan.Id).ToList();
        }

        private static void GetHourseWithSamurai()
        {
            var hourseWithoutSamurai = _context.Set<Horse>().Find(1);
            var hourseWithSamurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s =>s.Horse.Id==1);
            var hourseWithSamurais = _context.Samurais
                .Where(s => s.Horse != null)
                .Select(s=> new {Horse=s.Horse, samurai=s})//can miss
                .ToList();
        }

        private static void AddNewHorseToAnExistingSamuraiObject()
        {
            var samurai = _context.Samurais.FirstOrDefault(s=>s.Id==10);
            samurai.Horse = new Horse { Name = "Pinkie" };
            using (var newContext = new SamuraiContextNoTracking())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        private static void AddNewHorseToAnExistingSamuraiUsingId()
        {
            var horse = new Horse { Name = "Gerard", SamuraiId = 11 };
            _context.Add(horse);
            _context.SaveChanges();
        }

        private static void GetSamuraiWithBattles()
        {
            var samuraisWithBattles = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(s => s.Battle)
                .ToList();
            
            var firstsamuraiWithBattle = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai=> samurai.Id==2);

            var samuraiWithBattleClener = _context.Samurais
                .Select(s => new
                {
                    Samurai = s,
                    Battles = s.SamuraiBattles.Select(sb => sb.Battle)
                })
                .FirstOrDefault();
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 11 };
            _context.Remove(join);
            _context.SaveChanges();
        }

        public static void EnlistSamuraiIntoBattle()
        {
            var battle = _context.Battles.AsTracking().FirstOrDefault();
            battle.SamuraiBattles.AddRange(new List<SamuraiBattle>
            {
                new SamuraiBattle {SamuraiId = 11 },
                new SamuraiBattle{ SamuraiId = 10 }
                    
            });
            
            _context.SaveChanges();
        }

        public static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { BattleId = 3, SamuraiId = 11 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }
        
        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.AsNoTracking()
                .Include(s => s.Quotes)
                .FirstOrDefault(s => s.Id == 11);


            var quote = samurai.Quotes[0];
            quote.Text += " again?";
            //NOK because update all quotes for that samurai
            //using (var newContext = new SamuraiContextNoTracking)
            //{
            //    newContext.Quotes.Update(quote);
            //    newContext.SaveChanges();
            //}

            using (var newContext = new SamuraiContextNoTracking())
            {
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            /*var samurais = _context.Samurais.Include(s => s.Quotes).ToList();

            var samuraiFilteredByQuates = _context.Quotes
                .Include(s => s.Samurai)
                .Where(q => EF.Functions.Like(q.Text, "%come%"))
                .ToList();

            var samuraiQuotesFilteredBySamuraiName = _context.Samurais
                .Include(s => s.Quotes)
                .Where(s => EF.Functions.Like(s.Name, "%Baggings%"))
                .ToList();*/
            var samurais = _context.Samurais.AsTracking()
                .Include(s=> s.Quotes)
                .FirstOrDefault(s=> s.Id==11) ;
            samurais.Quotes[0].Text = "Did you heard it?";
            _context.SaveChanges();
        }

        private static void FilteringWithRelatedQuates()
        {
            //fine it filters ok, but don't bring me the quotes
            var samurais = _context.Samurais.AsTracking().Where(s => s.Quotes.Any(q => q.Text.Contains("I"))).ToList();

            foreach (var samurai in samurais)
            {
                _context.Entry(samurai).Collection(s => s.Quotes).Load();
                _context.Entry(samurai).Reference(s => s.Horse).Load();
            }
        }

        private static void ExplicitLoadQuates()
        {
            var samurai2 = _context.Samurais.AsTracking().FirstOrDefault(s => s.Name.Contains("Baggings"));
            _context.Entry(samurai2).Collection(s => s.Quotes).Load();
            _context.Entry(samurai2).Reference(s => s.Horse).Load();


            //load can be applied only on Object
            var samurais = _context.Samurais.AsTracking().Where(s => s.Name.Contains("Baggings")).ToList();
            foreach (var samurai in samurais)
            {
                _context.Entry(samurai).Collection(s => s.Quotes).Load();
                _context.Entry(samurai).Reference(s => s.Horse).Load();
            }

        }

        private static void ProjectSamuraisWithQuotes()
        {
            //LEFT JOIN
            var SamuraisWithQuotes=_context.Samurais
                .Select(s => new { s.Id, s.Name, s.Quotes })
                .ToList();
            
            //LEFT JOIN
            var SamuraisWithQuotesFiltered = _context.Samurais
                .Select(s =>
                new
                {
                    s.Id,
                    s.Name,
                    FilteredQuotes = s.Quotes.Where(q => q.Text.Contains("happy"))
                })
                .ToList();
            //LEFT JOIN
            var SamuraiSWithQuotesFiltered2 = _context.Samurais
                .Select(s =>
                new 
                {
                    Samurai=s,
                    FilteredQuotes = s.Quotes.Where(q => q.Text.Contains("happy"))
                })
                .ToList();
        }

        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }
        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }
        private static void EagerLoadSamuraiWithQuates()
        {
            //inner join between samurai and quotes table
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();

            //inner join between samurai and quotes table+where clause
            var samuraiWithQuotes2 = _context.Samurais
                .Where(s=> s.Name.Contains("Sampson"))
                .Include(s => s.Quotes).ToList();
        }

        private static void AddQuoteToAnExistingSamuraiWNotTracked_Easy(int samuraiId)
        {
            var quote = new Quote { Text = "Force be with you!", SamuraiId = samuraiId };

            using (var newContext = new SamuraiContextNoTracking())
            {
                newContext.Quotes.Update(quote);
                newContext.SaveChanges();
            }
        }

        private static void AddQuoteToAnExistingSamuraiWNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            
            samurai.Quotes.AddRange(new List<Quote>
            {
                new Quote{ Text="Force be with you!"},
                new Quote{ Text="!"}
            });

            using (var newContext = new SamuraiContextNoTracking())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }
        private static void AddQuoteToAnExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote { Text = "You must pay me dinner because of saving you!" });
            _context.Samurais.Update(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Frodo Baggings",
                Quotes = new List<Quote>
                { 
                    new Quote{Text="I've come..., no ideea why!"},
                    new Quote{Text="I miss The Shire!"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
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

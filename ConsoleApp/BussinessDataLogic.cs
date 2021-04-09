using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class BussinessDataLogic
    {
        private SamuraiContext _context;
        public BussinessDataLogic(SamuraiContext context)
        {
            _context = context;
        }
        public BussinessDataLogic()
        {
            _context = new SamuraiContext();
        }
        public int AddMultipleSamurais(string [] nameList)
        {
            var samuraiList = new List<Samurai>();

            foreach (var name in nameList) 
            {
                samuraiList.Add(new Samurai { Name = name });
            }
            _context.Samurais.AddRange(samuraiList);
            
            var dbResult = _context.SaveChanges();
            return dbResult;
        }
        public int InsertNewSamurai(Samurai samurai)
        {
            _context.Samurais.Add(samurai);
            var dbResult = _context.SaveChanges();
            return dbResult;
        }

    }
}

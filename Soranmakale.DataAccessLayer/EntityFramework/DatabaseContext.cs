using Soranmakale.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Anket> Anket { get; set; }
        public DbSet<AnketinCevabi> AnketinCevabi { get; set; }
        public DbSet<AnketinCevabiniSecenler> AnketinCevabiniSecenler { get; set; }
        public DbSet<AnketinYorumu> AnketinYorumu { get; set; }
        public DbSet<FavlananAnket> FavlananAnket { get; set; }
        public DbSet<FavlananMakale> FavlananMakale { get; set; }
        public DbSet<FavlananSoru> FavlananSoru { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Makale> Makale { get; set; }
        public DbSet<MakaleninBegenisi> MakaleninBegenisi { get; set; }
        public DbSet<MakaleninYorumu> MakaleninYorumu { get; set; }
        public DbSet<Soru> Soru { get; set; }
        public DbSet<SorununYorumu> SorununYorumu { get; set; }
        public DbSet<SorununYorumununBegenisi> SorununYorumununBegenisi { get; set; }

        
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}

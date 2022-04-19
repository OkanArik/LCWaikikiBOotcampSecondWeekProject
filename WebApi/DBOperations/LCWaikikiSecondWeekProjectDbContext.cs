using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

// EntityFrameworkCore bir ORM aracıdır ve bu orm aracı sayesinde db deki objeler ile code daki nesneler arasında kurar ve burada biz o köprüleri kuruyoruz. 

namespace WebApi.DBOperations
{
    public class LCWaikikiSecondWeekProjectDbContext : DbContext // Database deki verilere erişmek veya üzerinde manipülasyon operasyonlarımızı gerçekleştirebilmek için LCWakikiSecondWeekProjectDbContext sınıfını Microsoft.EntityFrameworkCore isim uzayı altındaki DbContext sınfından kalıtım aldırdık.Eğer DbContext sınıfından kalıtım aldırmasaydık o zaman dbcontext sınıfı olmaz normal bir sınıf olurdu.
    {
        public LCWaikikiSecondWeekProjectDbContext(DbContextOptions<LCWaikikiSecondWeekProjectDbContext> options) : base (options){} //Kurucu fonskisyon ve kalıtım aldığımız sınıfın options larınıda base ile aldık kurucu methodda.

        public DbSet<Product> Products { get; set; } //public DbSet<EntityName> GenerateDBName {get; set;} // Burada bu context'e Product entity sini ekledik ve Products ismiyle herşeyine erişebiliriz.Buradaki product db deki Products objesinin koddan ulaşabileceğimiz karşılığıdır.

        public DbSet<Category> Categories { get; set; }

    }
}
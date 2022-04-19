using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DbGenerator
    {
        public static void Initiliaze(IServiceProvider serviceProvider) //System isim uzayı altından gelen IServiceProvider tipinde bir serviceprovider'ı program.cs de yaratcam ve  Initilaize methodumu tetiklerken göndercem ve bu sayede uygulamam ne zman işlk defa ayağa kalkarsa o zaman bu method tetiklenecek o serviceprovider gönderilecek ve inmemory database imde initila value um oluşturulacak.
        {
            using (var context= new LCWaikikiSecondWeekProjectDbContext(serviceProvider.GetRequiredService<DbContextOptions<LCWaikikiSecondWeekProjectDbContext>>()))
            {
                //Bu kullanım tarzı ile context imizin bir instance(örnek)ını yarattık ve bu kullanım ile şu an yorum satırının bulunduğu scope içerisinde context imi kullanabilirim.
                 if(context.Products.Any())//Db deki Products tablosunda herhangi bir veri varsa burada return eder.Yoksa devam edip inital value olarak aşağıdaki verileri ekleyecek.
                   return;

                context.Products.AddRange(
                        new Product{
                            CategoryId=1, //Children
                            Name = "Trouser",
                            Price = 20, 
                            SizeId=1
                        },
                        new Product{
                            CategoryId=1, //Children
                            Name = "SWEATER",
                            Price = 15, 
                            SizeId=2
                        },
                        new Product{
                            CategoryId=2, //Woman
                            Name = "Trouser",
                            Price = 35, 
                            SizeId=3
                        },
                        new Product{
                            CategoryId=2, //Woman
                            Name = "SWEATER",
                            Price = 22, 
                            SizeId=5
                        },
                        new Product{
                            CategoryId=2, //Woman
                            Name = "Dress",
                            Price = 75, 
                            SizeId=4
                        },
                        new Product{
                            CategoryId=3, //Man
                            Name = "SWEATER",
                            Price = 21, 
                            SizeId=4
                        },
                        new Product{
                            CategoryId=3, //Man
                            Name = "Trouser",
                            Price = 20, 
                            SizeId=6
                        },
                        new Product{
                            CategoryId=3, //Man
                            Name = "Shirt",
                            Price = 20, 
                            SizeId=3
                        },
                        new Product{
                            CategoryId=3, //Man
                            Name = "Shoe",
                            Price = 20, 
                            SizeId=5
                        }
                );
                context.SaveChanges();
                
                 if(context.Categories.Any())
                   return;

                context.Categories.AddRange(
                    new Category{
                        Name="Children",
                    },
                    new Category{
                        Name="Woman"
                    },
                    new Category{
                        Name="Man"
                    }
                );

                context.SaveChanges();//Bu komut ile yaptığım değişiklerin Db ye kaydedilmesini sağlarız.
                    
            }
        }
    }
}
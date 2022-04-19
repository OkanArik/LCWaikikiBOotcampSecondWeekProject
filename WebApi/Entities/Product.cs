using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // System.ComponentModel.DataAnnotations.Schema isim uzayından gelen bu attribute ile Id integerının otoincrement olarak tanımladım.Bu sayede Id verisi her eklenen Product resource ı için otomatik olarak 1 er artarak eklenecektir.
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SizeId { get; set; }
        public int Price { get; set; }
    }
}
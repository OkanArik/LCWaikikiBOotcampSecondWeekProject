using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.ProductOperations.Commands.UpdateProduct
{
    public class UpdateProductCommand
    {
        public UpdateProductModel Model { get; set; }
        public int ProductId { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;

        public UpdateProductCommand(LCWaikikiSecondWeekProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var product= _dbContext.Products.SingleOrDefault(x=> x.Id==ProductId);

            if(product is null)
               throw new InvalidOperationException("Güncellemek istediğiniz ürün mevcut değil!");
            
            product.Name= Model.Name==string.Empty?product.Name:Model.Name;
            product.Price=Model.Price==default?product.Price:Model.Price;
            product.SizeId=Model.SizeId==default?product.SizeId:Model.SizeId;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateProductModel
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int SizeId { get; set; }
        }
}
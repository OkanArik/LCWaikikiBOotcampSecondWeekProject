using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.ProductOperations.Commands.DeleteProduct
{
    public class DeleteProductCommand
    {
        public int ProductId { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;

        public DeleteProductCommand(LCWaikikiSecondWeekProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle ()
        {
            var product= _dbContext.Products.SingleOrDefault(x=> x.Id==ProductId);

            if(product is null)
               throw new InvalidOperationException("Silmek istediğiniz ürün mevcut değil!");

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }
    }
}
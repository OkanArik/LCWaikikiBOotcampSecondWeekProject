using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.CategoryOperations.Commands.DeleteCategory
{
    public class DeleteCategoryCommand
    {
        public int CategoryId { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;

        public DeleteCategoryCommand(LCWaikikiSecondWeekProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var category = _dbContext.Categories.SingleOrDefault(x=> x.Id==CategoryId);
            
            if(category is null)
              throw new InvalidOperationException($"Silmek istediğiniz {CategoryId} 'li kategory mevcut değil!");
            
            if(_dbContext.Products.Any(x=> x.CategoryId==CategoryId))
              throw new InvalidOperationException($"{CategoryId} id'li kategoriye ait ürün bulunmaktadır bu kategoriyi silemezsiniz!");
            
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }
    }
}
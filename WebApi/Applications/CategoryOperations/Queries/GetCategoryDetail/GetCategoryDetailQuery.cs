using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.CategoryOperations.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQuery
    {
        public int CategoryId { get; set; }
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCategoryDetailQuery(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CategoryDetailViewModel Handle()
        {
            var category= _dbContext.Categories.SingleOrDefault(x=> x.Id==CategoryId);

            if(category is null)
              throw new InvalidOperationException($"{CategoryId} id'li kategori mevcut değil!");

            var result= _mapper.Map<CategoryDetailViewModel>(category);
            return result;
        }
    }

    public class CategoryDetailViewModel
    {
        public string Name { get; set; }
    }
}
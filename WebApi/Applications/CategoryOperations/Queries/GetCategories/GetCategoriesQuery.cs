using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.CategoryOperations.Queries.GetCategories
{
    public class GetCategoriesQuery
    {
        private readonly LCWaikikiSecondWeekProjectDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCategoriesQuery(LCWaikikiSecondWeekProjectDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CategoriesViewModel> Handle()
        {
            var categoryList= _dbContext.Categories.OrderBy(x=> x.Id).ToList();
            var resultList= _mapper.Map<List<CategoriesViewModel>>(categoryList);
            return resultList;
        }
    }

    public class CategoriesViewModel
    {
        public string Name { get; set; }
    }
}
using BLL.Entities;
using Common.Repositories;
using BLL.Mappers;
using D=DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CocktailService : ICocktailRepository<Cocktail>
    {
        private ICocktailRepository<DAL.Entities.Cocktail> _service;

        public CocktailService(ICocktailRepository<DAL.Entities.Cocktail> cocktailService) 
        {
            _service = cocktailService;
        }


        public IEnumerable<Cocktail> Get()
        {
            return _service.Get().Select(dal => dal.ToBLL());
        }

        public Cocktail Get(Guid id)
        {
            return _service.Get(id).ToBLL();
        }

        public Guid Insert(Cocktail cocktail)
        {
           return _service.Insert(cocktail.ToDAL());
        }

        public void Update(Guid id, Cocktail cocktail)
        {
            _service.Update(id, cocktail.ToDAL());

        }
        public void Delete(Guid id)
        {
            _service?.Delete(id);
        }
    }
}

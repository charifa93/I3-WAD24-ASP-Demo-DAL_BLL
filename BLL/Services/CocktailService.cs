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
        private ICocktailRepository<DAL.Entities.Cocktail> _cocktailService;
        private IUserRepository<DAL.Entities.User> _userService;

        public CocktailService(ICocktailRepository<DAL.Entities.Cocktail> cocktailService , IUserRepository<DAL.Entities.User> userService ) 
        {
            _cocktailService = cocktailService;
            _userService = userService;
        }


        public IEnumerable<Cocktail> Get()
        {
            IEnumerable<Cocktail> cocktails = _cocktailService.Get().Select(dal => dal.ToBLL());
            foreach (Cocktail cocktail in cocktails)
            {
                if (cocktail.CreatedBy is not null)
                {
                    cocktail.Creator = _userService.Get((Guid)cocktail.CreatedBy).ToBLL();
                }
            }
            return cocktails;

        }

        public Cocktail Get(Guid id)
        {
           Cocktail cocktail = _cocktailService.Get(id).ToBLL();
            if (cocktail.CreatedBy is not null) 
            {
                cocktail.Creator = _userService.Get((Guid)cocktail.CreatedBy).ToBLL();
            }
            return cocktail;
        }

        public Guid Insert(Cocktail cocktail)
        {
           return _cocktailService.Insert(cocktail.ToDAL());
        }

        public void Update(Guid id, Cocktail cocktail)
        {
            _cocktailService.Update(id, cocktail.ToDAL());

        }
        public void Delete(Guid id)
        {
            _cocktailService?.Delete(id);
        }

        public IEnumerable<Cocktail> GetFromUser(Guid user_id)
        {
            return _cocktailService.GetFromUser(user_id).Select(dal=> dal.ToBLL());
        }
    }
}

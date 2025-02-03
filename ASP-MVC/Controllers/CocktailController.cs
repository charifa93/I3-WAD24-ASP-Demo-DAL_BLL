using ASP_MVC.Models.Cocktail;
using ASP_MVC.Mappers;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class CocktailController : Controller
    {
        private ICocktailRepository<Cocktail> _cocktailService;

        public CocktailController(ICocktailRepository<Cocktail> cocktailService)
        {
            _cocktailService = cocktailService;

        }

        public ActionResult Index() 
        {
            try
            {
                IEnumerable<CocktailListItem> model = _cocktailService.Get().Select(bll=> bll.ToListItem());
                return View(model);


            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
               
            }
        }
        public ActionResult Details(Guid id) 
        {
            try 
            {
                CocktailDetails model = _cocktailService.Get(id).ToDetails();
                return View(model);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Create(CocktailCreateForm form)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid) throw new ArgumentException();
        //        Guid id = _cocktailService.Insert(form.ToBLL());

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

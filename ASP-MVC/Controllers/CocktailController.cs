using ASP_MVC.Models.Cocktail;
using ASP_MVC.Mappers;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using ASP_MVC.Handlers;

namespace ASP_MVC.Controllers
{
    public class CocktailController : Controller
    {
        private ICocktailRepository<Cocktail> _cocktailService;
        private SessionManager _sessionManager;

        public CocktailController(ICocktailRepository<Cocktail> cocktailService , SessionManager sessionManager)
        {
            _cocktailService = cocktailService;
            _sessionManager = sessionManager;

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

        // GET: CocktailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CocktailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CocktailCreateForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
                Guid id = _cocktailService.Insert(form.ToBLL());
                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return View();
            }
        }

        // GET: CocktailController/Edit/5
        public ActionResult Edit(Guid id)
        {
            try
            {
                CocktailEditForm model = _cocktailService.Get(id).ToEditForm();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CocktailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, CocktailEditForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
                _cocktailService.Update(id, form.ToBLL());
                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return View();
            }
        }

        // GET: CocktailController/Delete/5
        public ActionResult Delete(Guid id)
        {
            try
            {
                CocktailDeleteForm model = _cocktailService.Get(id).ToDelete();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CocktailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, CocktailDeleteForm form)
        {
            try
            {
                _cocktailService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

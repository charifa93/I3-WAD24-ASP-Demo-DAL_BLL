using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;



namespace ASP_MVC.Handlers.ActionFilters

    
{
    //le attribute touche que les Methode (Actions de controllers)
    [AttributeUsage(AttributeTargets.Method)]
    public class AnnonymousNeededAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //si le User c connecter il va retournee a la page home/index
            if (context.HttpContext.Session.GetString(nameof(SessionManager.ConnectedUser)) is not null )
            {
                context.Result = new RedirectToActionResult("Index", "Home",null);
            }
        }
    }
}

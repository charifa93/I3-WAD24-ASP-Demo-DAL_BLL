using ASP_MVC.Models.Cocktail;
using System.Text.Json;

namespace ASP_MVC.Handlers
{
    public class SessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor accessor)
        {
            _session = accessor.HttpContext.Session;
        }

        public int CountVisitedPage
        {
            get { return _session.GetInt32(nameof(CountVisitedPage)) ?? 0; }
            set { _session.SetInt32(nameof(CountVisitedPage), value); }
        }

        public ConnectedUser? ConnectedUser
        {
            get { return JsonSerializer.Deserialize<ConnectedUser?>(_session.GetString(nameof(ConnectedUser)) ?? "null"); }
            set
            {
                if (value is null)
                {
                    _session.Remove(nameof(ConnectedUser));
                }
                else
                {
                    _session.SetString(nameof(ConnectedUser), JsonSerializer.Serialize(value));
                }
            }
        }
        public void Login(ConnectedUser user)
        {
            ConnectedUser = user;
        }
        public void Logout()
        {
            ConnectedUser = null;
        }

        public IEnumerable<ListItemMin> RecentlyVisitesCocktails
        {
            get
            {
                string? json = _session.GetString(nameof(RecentlyVisitesCocktails));

                if (json is null) return new ListItemMin[0];
                return JsonSerializer.Deserialize<ListItemMin[]>(json);
            }
            private set
            {
                string json = JsonSerializer.Serialize(value);
                _session.SetString(nameof(RecentlyVisitesCocktails), json);
            }
        }
        public void AddVisitedCocktail(ListItemMin cocktail)
        {
            //ligne permettant d(inserer le cocktail 
            List<ListItemMin> cocktails = new List<ListItemMin>(RecentlyVisitesCocktails);
            ListItemMin? cocktailInList = cocktails.Where(c => c.Cocktail_Id == cocktail.Cocktail_Id).SingleOrDefault();
            if(cocktailInList is not null )
                {
                   cocktails.Remove(cocktailInList);
                }
            if(cocktails.Count == 5) 
            {
                cocktails.Remove(cocktails[5]);
            }
            cocktails.Insert(0, cocktail);
            RecentlyVisitesCocktails = cocktails;
            

        }
        public void AddVisitedCocktail(Guid cocktail_id , string cocktail_name)
        {
            ListItemMin cocktail = new ListItemMin()
            {
                Cocktail_Id = cocktail_id,
                Name = cocktail_name
            };
            AddVisitedCocktail(cocktail);
            

        }

    }
}

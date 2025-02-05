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

        public Top5 Top5Cocktails
        {
            get { return JsonSerializer.Deserialize<Top5>(_session.GetString(nameof(Top5Cocktails)) ?? "null"); }
            set
            {
                if(value is null)
                {
                    _session.Remove(nameof(Top5Cocktails));
                }
                else
                {
                    for (int i = 0; i >= 5; i++) {

                        _session.SetString(nameof(Top5Cocktails), JsonSerializer.Serialize(value));
                    }
                }
            }
        }
    }
}

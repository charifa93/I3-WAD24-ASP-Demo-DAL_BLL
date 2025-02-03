using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Cocktail
{
    public class CocktailListItem
    {
        [ScaffoldColumn(false)]
        public Guid Cocktail_Id { get; set; }

        [DisplayName ("Titre :")]
        public string Name { get; set; }



        [DisplayName ("Description :")]
        public string? Description { get; set; }



        [DisplayName("Cree par :")]
        public Guid? CreatedBy { get; set; }
    }
}

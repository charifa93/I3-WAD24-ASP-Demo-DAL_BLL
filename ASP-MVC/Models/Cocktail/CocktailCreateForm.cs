using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Cocktail
{
    public class CocktailCreateForm
    {

        [DisplayName ("Titre : ")]
        [Required (ErrorMessage = "le champ 'Titre' est obligatoire ..")]
        public string Name { get; set; }



        [DisplayName ("Description : ")]
        public string? Description { get; set; }



        [DisplayName ("Instructions : ")]
        [Required (ErrorMessage = "Le champ 'Instructions' est obligatoire .... ")]
        public string Instructions { get; set; }


        [DisplayName (" cree par  :")]

        public Guid CreatedBy { get; set; }

    }
}

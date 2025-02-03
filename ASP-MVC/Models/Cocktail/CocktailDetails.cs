using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Cocktail
{
    public class CocktailDetails
    {
        [ScaffoldColumn(false)]
        public Guid Cocktail_Id { get; set; }

        [DisplayName ("Titre :")]
        public string Name { get; set; }


        [DisplayName ("Description :" )]
        public string? Description { get; set; }

        [DisplayName ("Instructions : ")]
        public string Instruction { get; set; }


        [DisplayName ("Create At : ")]
        public DateOnly CreatedAt { get; set; }

        [DisplayName ("Create By :")]
        [DataType(DataType.Date)]
        public Guid? CreatedBy { get; set; }

    }
}

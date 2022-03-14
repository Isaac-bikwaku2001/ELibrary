using System.ComponentModel.DataAnnotations;

namespace ELibrary.Models
{
    public class Auteur
    {
        public int AuteurID { get; set; }

        [Required(ErrorMessage = "Nom de l'auteur est requis"), MaxLength(100)]
        public String Nom { get; set; }

        [Required(ErrorMessage = "Prénom de l'auteur est requis"), MaxLength(100)]
        public String Prenom { get; set; }

        [Required(ErrorMessage = "Nationalité de l'auteur est requis"), MaxLength(100)]
        public String Nationalite { get; set; }
    }
}

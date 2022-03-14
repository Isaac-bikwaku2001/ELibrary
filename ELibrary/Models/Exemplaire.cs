using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibrary.Models
{
    public class Exemplaire
    {
        public int ExemplaireID { get; set; }

        [ForeignKey("Livre")]
        public int LivreID { get; set; }
        public virtual Livre Livre { get; set; }

        [Required(ErrorMessage = "Nombre d'exemplaire est requis")]
        public int NombreExempalire { get; set; }
    }
}

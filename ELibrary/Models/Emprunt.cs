using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ELibrary.Models
{
    public class Emprunt
    {
        public int EmpruntID { get; set; }

        [ForeignKey("Livre")]
        public int LivreID { get; set; }
        public virtual Livre Livre { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; }

        [Required(ErrorMessage = "Date d'emprunt est requis")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DateEmprunt { get; set; } = DateTime.Now;
    }
}

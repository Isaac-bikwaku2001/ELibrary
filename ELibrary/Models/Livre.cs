using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELibrary.Models
{
    public class Livre
    {
        public int LivreID { get; set; }

        [Required(ErrorMessage = "ISBN est requis"), MaxLength(30)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Titre est requis"), MaxLength(100)]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Langue est requis"), MaxLength(50)]
        public string Langue { get; set; }

        [Required(ErrorMessage = "Date d'édition est requis")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DateEdition { get; set; }

        [Required(ErrorMessage = "Image est requise")]
        public string Image { get; set; }

        [ForeignKey("Auteur")]
        public int AuteurID { get; set; }
        public virtual Auteur Auteur { get; set; }

        [ForeignKey("Genre")]
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}

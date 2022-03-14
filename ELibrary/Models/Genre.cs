using System.ComponentModel.DataAnnotations;

namespace ELibrary.Models
{
    public class Genre
    {
        public int GenreID { get; set; }

        [Required(ErrorMessage = "Libelle est requis"), MaxLength(100)]
        public string Libelle { get; set; }
    }
}

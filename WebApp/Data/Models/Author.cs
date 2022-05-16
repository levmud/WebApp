using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Data.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Country { get; set; }
        public string Direction { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace bookworm.Entities
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
        public double Count { get; set; }
        public string ISBN { get; set; }
    }
}

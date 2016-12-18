using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace bookworm.Entities
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public Client Client { get; set; }
    }
}

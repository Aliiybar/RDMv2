using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Models.Entities
{
    public class Sign
    {
        [Key]
        public Guid Id { get; set; }
        public string SignName { get; set; }
    }
}

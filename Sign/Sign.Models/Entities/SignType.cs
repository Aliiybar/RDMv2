using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Models.Entities
{
    public class SignType
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}

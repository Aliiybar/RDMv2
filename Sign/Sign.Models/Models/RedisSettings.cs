using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Models.Models
{
    public class RedisSettings
    {
        public string Server { get; set; }
        public string Prefix { get; set; }
        public int DefaultExpireTimeInSeconds { get; set; }
    }
}


 
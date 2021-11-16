using Sign.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Worker
{
    public interface ISender
    {
        void UpdateSign(SignModel signModel);
    }
}

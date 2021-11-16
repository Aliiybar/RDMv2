using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Db.Initializes
{
    public static class SignSeed
    {
        public static Sign.Models.Entities.Sign[] Seed()
        {
            return new Models.Entities.Sign[]
               {
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S001"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S002"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S003"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S004"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S005"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S006"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S007"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S008"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S009"},
                    new Models.Entities.Sign{Id=Guid.NewGuid(), SignName="S010"},
               };
        }
    }
}

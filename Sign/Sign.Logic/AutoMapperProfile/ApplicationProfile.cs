using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Logic.AutoMapperProfile
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Models.Entities.Sign, Models.Models.SignModel>()
                .ReverseMap();
            CreateMap<Models.Entities.SignType, Models.Models.SignTypeModel>()
                .ReverseMap();
        }
    }
}

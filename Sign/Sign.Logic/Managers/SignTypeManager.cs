using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Sign.Db.Interfaces;
using Sign.Logic.Extensions;
using Sign.Logic.Interfaces;
using Sign.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Logic.Managers
{
    public class SignTypeManager : ISignTypeManager
    {
        private readonly ISignTypeRepository _signTypeRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public SignTypeManager(
            ISignTypeRepository signTypeRepository,
            IDistributedCache distributedCache,
            IMapper mapper)
        {
            _signTypeRepository = signTypeRepository;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }


        public async Task<List<SignTypeModel>> GetAllSignTypes()
        {
            string recordKey = "SignTypeAll";
            var signTypeModel = await _distributedCache.GetRecordAsync<List<Models.Models.SignTypeModel>>(recordKey);
            if (signTypeModel is null)
            {
                var signTypes = await _signTypeRepository.GetAllAsync();
                signTypeModel = _mapper.Map<List<Models.Models.SignTypeModel>>(signTypes);
                await _distributedCache.SetRecordAsync(recordKey, signTypeModel);
            }
            return signTypeModel;
        }
    }
}

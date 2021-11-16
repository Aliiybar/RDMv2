using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Sign.Db.Interfaces;
using Sign.Logic.Extensions;
using Sign.Logic.Interfaces;
using Sign.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sign.Logic.Managers
{
    public class SignManager : ISignManager
    {
        private readonly ISignRepository _signRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        
        public SignManager(
            ISignRepository signRepository,
            IDistributedCache distributedCache,
            IMapper mapper)
        {
            _signRepository = signRepository;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }

           
        public async Task<List<SignModel>> GetAllSigns()
        {
            string recordKey = "SignsAll";
            var signModel = await _distributedCache.GetRecordAsync<List<Models.Models.SignModel>>(recordKey);
            if (signModel is null)
            {
                var signs = await _signRepository.GetAllAsync();
                signModel = _mapper.Map<List<Models.Models.SignModel>>(signs);
                await _distributedCache.SetRecordAsync(recordKey, signModel);
            }
            return signModel;
        }

        public async Task UpdateSign(SignModel signModel)
        {
            var signEntity = await _signRepository.GetAsync(signModel.Id);
            if (signEntity == null) return;

            signEntity.SignName = signModel.SignName;
            await _signRepository.UpdateAsync(signEntity);
            await _distributedCache.RemoveAsync("SignsAll"); // As the cache is now invalid, simply remove it
        }

    }
}

using Sign.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sign.Logic.Interfaces
{
    public interface ISignManager
    {
        Task<List<SignModel>> GetAllSigns();
        Task UpdateSign(SignModel signModel);
    }
}
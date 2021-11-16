using Sign.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sign.Logic.Interfaces
{
    public interface ISignTypeManager
    {
        Task<List<SignTypeModel>> GetAllSignTypes();
    }
}
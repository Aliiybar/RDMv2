using Microsoft.EntityFrameworkCore;
using Sign.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sign.Db.Repositories
{
 
    public class SignTypeRepository : BaseRepository<Models.Entities.SignType>, ISignTypeRepository
    {
        private readonly SignContext _signContext;
        public SignTypeRepository(IDbContextFactory<SignContext> signContextFactory) : base(signContextFactory)
        {
            _signContext = signContextFactory.CreateDbContext();
        }
    }
}

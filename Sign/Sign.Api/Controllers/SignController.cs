using Microsoft.AspNetCore.Mvc;
using Sign.Logic.Interfaces;
using Sign.Models.Models;
using Sign.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sign.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignController : ControllerBase
    {
        private ISignManager _signManager;
        private ISender _sender;

        public SignController(ISignManager signManager, ISender sender)
        {
            _signManager = signManager;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IEnumerable<SignModel>> Get()
        {
            return await _signManager.GetAllSigns();
        }


        [HttpPost]
        public async Task Post(SignModel model)
        {
            _sender.UpdateSign(model);
        }

    }


}

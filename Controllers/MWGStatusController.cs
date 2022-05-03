using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pickflicks2.Models;
using pickflicks2.Services;

namespace pickflicks2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MWGStatusController : ControllerBase
    {
        private readonly MWGStatusService _data;

        public MWGStatusController(MWGStatusService dataFromService) {
            _data = dataFromService;
        }

        [HttpPost("AddMWGStatus/{MWGId}")]
        public bool AddMWGStatus(int MWGId)
        {
           return _data.AddMWGStatus(MWGId);
        }

        [HttpGet("GetAllMWGStatus")]
        public IEnumerable<MWGStatusModel> GetAllMWGStatus()
        {
            return _data.GetAllMWGStatus();
        }

        [HttpGet("GetMWGStatusById/{id}")]
        public MWGStatusModel GetMWGStatusById(int id)
        {
            return _data.GetMWGStatusById(id);
        }
        [HttpGet("GetMWGStatusByMWGId/{MWGId}")]
        public IEnumerable<MWGStatusModel> GetMWGStatusByMWGId(int MWGId)
        {
            return _data.GetMWGStatusByMWGId(MWGId);
        }

        [HttpGet("GetMWGStatusByUserId/{UserId}")]
        public IEnumerable<MWGStatusModel> GetMWGStatusByUserId(int UserId)
        {
            return _data.GetMWGStatusByUserId(UserId);
        }

        [HttpPost("UpdateGenreRanking/{MWGId}/{UserId}")]
        public bool UpdateGenreRanking(int MWGId, int UserId)
        {
            return _data.UpdateGenreRanking(MWGId, UserId);
        }
        [HttpPost("UpdateSwipings/{MWGId}/{UserId}")]
        public bool UpdateSwipings(int MWGId, int UserId)
        {
            return _data.UpdateSwipings(MWGId, UserId);
        }
        [HttpPost("ResetMWGStatusbyMWGId/{MWGId}")]
        public bool ResetMWGStatusbyMWGId(int MWGId)
        {
            return _data.ResetMWGStatusbyMWGId(MWGId);
        }
    }
}
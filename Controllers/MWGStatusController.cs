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

        [HttpPost("AddMWGStatus")]
        public bool AddMWGStatus(MWGStatusModel newMWGStatus)
        {
           return _data.AddMWGStatus(newMWGStatus);
        }

        public IEnumerable<MWGStatusModel> GetAllMWGStatus()
        {
            return _data.GetAllMWGStatus();
        }

        [HttpGet("GetMWGStatusById/{id}")]
        public MWGModel GetMWGStatusById(int id)
        {
            return _data.GetMWGStatusById(id);
        }
        [HttpGet("GetMWGStatusByMWGId/{MWGId}")]
        public MWGModel GetMWGById(int MWGId)
        {
            return _data.GetMWGById(MWGId);
        }

        [HttpGet("GetMWGStatusByUserId/{UserId}")]
        public MWGModel GetMWGStatusById(int UserId)
        {
            return _data.GetMWGStatusById(UserId);
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
        public bool UpdateSwipings(int MWGId)
        {
            return _data.UpdateSwipings(MWGId);
        }
    }
}
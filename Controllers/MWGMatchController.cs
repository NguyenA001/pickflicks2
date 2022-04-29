using Microsoft.AspNetCore.Mvc;
using pickflicks2.Models;
using pickflicks2.Services;

namespace pickflicks2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MWGMatchController : ControllerBase
    {
        private readonly MWGMatchService _data;

        public MWGMatchController(MWGMatchService dataFromService) {
            _data = dataFromService;
        }

        // Append the string of 0s, and 1s
        [HttpPost("AddLikeOrDislike")]
        public bool AddLikeOrDislike(MWGMatchModel updatedModel)
        {
            return _data.AddLikeOrDislike(updatedModel);
        }

        [HttpGet("GetMWGMatchModelsByUserId/{userId}")]
        public IEnumerable<MWGMatchModel> GetMWGMatchModelsByUserId(int userId)
        {
            return _data.GetMWGMatchModelsByUserId(userId);
        }

        [HttpGet("GetMWGMatchModelsByMWGId/{MWGId}")]
        public IEnumerable<MWGMatchModel> GetMWGMatchModelsByMWGId(int MWGId)
        {
            return _data.GetMWGMatchModelsByMWGId(MWGId);
        }

        [HttpGet("GetAllMWGMatchModels")]
        public IEnumerable<MWGMatchModel> GetAllMWGMatchModels()
        {
            return _data.GetAllMWGMatchModels();
        }
    }
}

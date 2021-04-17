using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromotionsSG.API.Feedback.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Feedback.Controllers
{
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly IFeedbackRepository _repository;

        public FeedbackController(ILogger<FeedbackController> logger, IFeedbackRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Health()
        {
            return Ok("FeedbackController Healthy at : " + DateTime.Now.ToString());
        }

        [HttpPost]
        [Route("feedback/insert")]
        public async Task<int> CreateFeedback([FromBody] Feedbacks feedbacks)
        {
            var result = await _repository.CreateFeedback(feedbacks);

            return result;
        }

        [HttpPost]
        [Route("feedback/update")]
        public async Task<int> UpdateFeedback([FromBody] Feedbacks feedbacks)
        {
            var result = await _repository.UpdateFeedback(feedbacks);

            return result;
        }

        [HttpGet]
        [Route("feedback/retrieve")]
        public async Task<Feedbacks> Feedback([FromQuery] int promotionid)
        {
            var feedbackData = await _repository.Feedback(promotionid);
            return feedbackData;
        }

        [HttpGet]
        [Route("feedback/retrieveallfeedbacks")]
        public async Task<ActionResult> AllFeedbacks()
        {
            try
            {
                return Ok(await _repository.GetFeedbacks());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}

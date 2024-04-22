using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Helpers;
using trello_services.IRepository;
using trello_services.Models.Request;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        public CardController(ICardRepository cardRepository) { 
            _cardRepository = cardRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewCard(CardRequestModel request)
        {
            try
            {
                if (request.columnId == null) return BadRequest();
                var card = await _cardRepository.CreateNewCardAsync(request);
                return StatusCode(StatusCodes.Status201Created , new {message = "Created" , data = card });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{cardId}/mark-complete-dueday")]
        public async Task<IActionResult> MakeCompleteDueDay(Int64 cardId , bool isComplete)
        {
            try
            {
                var card = await _cardRepository.FindCardAsync(cardId);
                if (card == null) return BadRequest();
                await _cardRepository.MarkDueDateCompleteOrNotAsync(isComplete , cardId);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{cardId}/remove-daytime")]
        public async Task<IActionResult> RemoveDayTimeCard(Int64 cardId)
        {
            try
            {
               var card =  await _cardRepository.RemoveTimeOfCardAsync(cardId);
                if (card == null) return BadRequest();
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPut("{cardId}/update-infor-card")]
        public async Task<IActionResult> UpdateInforCard(Int64 cardId , CardRequestModel request)
        {
            try
            {
                var card = await _cardRepository.UpdateCardAsync(request , cardId);
                if (card == null) return BadRequest();
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{cardId}/update-time")]
        public async Task<IActionResult> UpdateTimeOfCard(DateTime? starDate, DateTime? endDate, Int64 cardId)
        {
            try
            {
                var card = await _cardRepository.UpdateTimeOfCardAsync(starDate, endDate, cardId);
                if (card == null) return BadRequest();
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}

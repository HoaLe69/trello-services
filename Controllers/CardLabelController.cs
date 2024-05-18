using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Helpers;
using trello_services.IRepository;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardLabelController : ControllerBase
    {
        private readonly ICardLabelRepository _cardLabelRepository;
        public CardLabelController(ICardLabelRepository cardLabelRepository)
        {
            _cardLabelRepository = cardLabelRepository;
        }
        [HttpGet("{cardId}")]
        public async Task<IActionResult> GetListLabelInCard(Guid cardId)
        {
            try
            {
                var card_labels = await _cardLabelRepository.GetAllLabelInCardByCardIdAsync(cardId);
                if (card_labels == null) return NotFound();
                return Ok(card_labels);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddLabelInCard (Guid cardId , Guid labelId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(labelId.ToString())) return BadRequest();
                var card_labels = await _cardLabelRepository.AddLabelToCardAsync(cardId, labelId);
                return StatusCode(StatusCodes.Status201Created , new {message = "created" , data = card_labels});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{cardId}/{labelId}")]
        public async Task<IActionResult> RemoveLabelFromCard (Guid cardId , Guid labelId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(labelId.ToString())) return BadRequest();
                await _cardLabelRepository.RemoveLabelFromCardAsync(cardId, labelId);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}

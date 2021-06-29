using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;
using AduabaNeptune.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AduabaNeptune.Controllers
{
    [Authorize]
    [ApiController]
    [Route("card")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerCards()
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            List<Card> customerCards = new List<Card>();
            customerCards = await _cardService.GetAllCustomerCreditCardsAsync(requesterIdentity);

            if (customerCards == null){return NoContent();}

            var cards = new List<GetCustomerCardResponse>();
            foreach (var card in customerCards)
            {
                cards.Add(card.AsCardResponseDto());
            }

            return Ok(cards);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerCardById(string id)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var customerCard = await _cardService.GetCustomerCreditCardByIdAsync(id);

            if (customerCard == null){return NotFound();}
            
            return Ok(customerCard.AsCardResponseDto());
        }


        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody]SaveCardRequest saveCardRequest)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _cardService.SaveCreditCardAsync(saveCardRequest, requesterIdentity);

            return CreatedAtAction(nameof(GetCustomerCardById), new{id = response.Id}, response.AsCardResponseDto());
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCard([FromBody]List<string> cardIds)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            if(cardIds.Count == 0){return BadRequest(new {message = "No card Id in request"});}

            await _cardService.DeleteCreditCardAsync(cardIds, requesterIdentity);
            return NoContent();
        }
    }
}
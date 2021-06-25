using System.Collections.Generic;
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
        public IActionResult GetCustomerCards()
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            List<Card> customerCards = new List<Card>();
            customerCards = _cardService.GetAllCustomerCreditCards(requesterIdentity);

            if (customerCards == null){return NoContent();}

            var cards = new List<GetCustomerCardResponse>();
            foreach (var card in customerCards)
            {
                cards.Add(new GetCustomerCardResponse
                {
                    CardId = card.Id,
                    CardHolderName = card.CardHolderName,
                    CardNumber = card.CardNumber,
                    ExpiryDate = card.ExpiryDate,
                    CCV = card.CCV
                });
            }

            return Ok(cards);
        }


        [HttpPost]
        public IActionResult AddCard([FromBody]SaveCardRequest saveCardRequest)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = _cardService.SaveCreditCard(saveCardRequest, requesterIdentity);

            return Ok();
        }


        [HttpDelete]
        public IActionResult DeleteCard([FromBody]List<string> cardIds)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            if(cardIds.Count == 0){return BadRequest(new {message = "No card Id in request"});}

            _cardService.DeleteCreditCard(cardIds, requesterIdentity);
            return Ok();
        }
    }
}
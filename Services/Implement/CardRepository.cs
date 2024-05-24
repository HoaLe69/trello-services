using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using trello_services.Data;
using trello_services.Entities;
using trello_services.IRepository;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.Services.Implement
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public CardRepository(ApplicationDBContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Card> FindCardAsync(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);
            return card;
        }
        public async Task<CardResponseVM> CreateNewCardAsync(CardRequestModel request)
        {
            Guid isSuccess;

            var isSuccessParameter = new SqlParameter("@isSuccess", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC GenerateCard @title, @description, @cover, @columnId, @startDate, @endDate, @isSuccess OUTPUT",
                new SqlParameter("@title", request.title),
                new SqlParameter("@description", request.description ?? (object)DBNull.Value),
                new SqlParameter("@cover", request.cover ?? (object)DBNull.Value),
                new SqlParameter("@columnId", request.columnId),
                new SqlParameter("@startDate", (object)DBNull.Value),
                new SqlParameter("@endDate",  (object)DBNull.Value),
                isSuccessParameter);

            isSuccess = (Guid)isSuccessParameter.Value;
            if (isSuccess != null)
            {
                return new CardResponseVM
                {
                    cardId = isSuccess,
                    columnId = (Guid)request.columnId,
                    cover = request.cover,
                    description = request.description,
                    title = request.title

                };
            }
            return null;
            //var card = new Card
            //{
            //    cardId = Guid.NewGuid(),
            //    title = request.title,
            //    columnId = (Guid)request.columnId,
            //};
            //await _context.Cards.AddAsync(card);
            //await _context.SaveChangesAsync();
            //return _mapper.Map<CardResponseVM>(card);
        }

        public async Task MarkDueDateCompleteOrNotAsync(bool isDueDateComplete, Guid cardId)
        {
            var card = await FindCardAsync(cardId);
            if (card != null)
            {
                card.isDueDayComplete = isDueDateComplete;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Card> RemoveTimeOfCardAsync(Guid cardId)
        {
            var card = await FindCardAsync(cardId);
            if (card == null) return null;
                card.isDueDayComplete = false;
                card.endDate = null;
                card.startDate = null;
                await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> UpdateCardAsync(CardRequestModel request , Guid cardId)
        {
            //var card = await FindCardAsync(cardId);
            //if (card == null) return null;
            //if (request.title != null) card.title = request.title;
            //if (request.description != null) card.description = request.description;
            //if (request.cover != null) card.cover = request.cover;
            //if (request.columnId != null) card.columnId = (Guid)request.columnId;
            //await _context.SaveChangesAsync();
            //return card;
            var parameters = new[]
       {
            new SqlParameter("@cardId", cardId),
            new SqlParameter("@title", (object)request.title ?? DBNull.Value),
            new SqlParameter("@description", (object)request.description ?? DBNull.Value),
            new SqlParameter("@columnId", (object)request.columnId ?? DBNull.Value)
        };

           var n =  await _context.Database.ExecuteSqlRawAsync("EXEC UpdateCardInfor @cardId, @title, @description, @columnId", parameters);
            if (n != null)
            {
                return new Card { };
            }
            return null;
        }   

        public async Task<Card> UpdateTimeOfCardAsync(DateTime? starDate, DateTime? endDate, Guid cardId)
        {
            var card = await FindCardAsync(cardId);
            if (card == null) return null;
            if (starDate != null) card.startDate = starDate;
            if (endDate != null) card.endDate = endDate;
            await _context.SaveChangesAsync();
            return card ;
        }

        public async Task<IList<CardResponseVM>> GetListCardByListIdAsync(Guid listId)
        {
            //var cards = await _context.Cards
            //                           .Include(c => c.Column)
            //                           .Where(c => c.Column.columnId == listId)
            //                           .ToArrayAsync();
            //List<CardResponseVM> cards_vm = new List<CardResponseVM>();
            //foreach (var card in cards)
            //{
            //    cards_vm.Add(_mapper.Map<CardResponseVM>(card));
            //}
            //return cards_vm;]
            var id = new SqlParameter("@listId", listId);
            var cards = await _context.Cards.FromSqlRaw("EXEC CardDecrypt @listId", id).ToListAsync();
            List<CardResponseVM> cards_vm = new List<CardResponseVM>();
            foreach (var card in cards)
            {
                cards_vm.Add(_mapper.Map<CardResponseVM>(card));
            }
            return cards_vm;
            //return (IList<CardResponseVM>)cards;
        }

        public async Task ChangeListOfCard(Guid cardId, CardRequestModel request)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card == null) return;
            card.columnId = (Guid)request.columnId;
            await _context.SaveChangesAsync();

        }

        public async Task<CardResponseVM> GetCardDetail(Guid cardId)
        {
            //var card_detail = await _context.Cards.FindAsync(cardId);
            //return card_detail;

            //var id = new SqlParameter("@cardId", cardId);
            //var cards = await _context.Cards.FromSqlRaw("EXEC CardDetailDecrypt @cardId", id).IgnoreQueryFilters().SingleOrDefaultAsync();

            //return _mapper.Map<CardResponseVM>(cards);
            var cardIdParam = new SqlParameter("@cardId", cardId);
            var result = await _context.Cards
               .FromSqlRaw("EXEC CardDetailDecrypt @cardId", cardIdParam)
               .ToListAsync();
            return result.Select(c => new CardResponseVM
            {
                cardId = c.cardId,
                title = c.title,
                description = c.description,
                cover = c.cover,
                columnId = c.columnId,
                startDate = c.startDate,
                endDate = c.endDate,
                isDueDayComplete = c.isDueDayComplete,
            }).First();
            
           // return result;
        }
    }
}

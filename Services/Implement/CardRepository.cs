﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Card> FindCardAsync(long id)
        {
            var card = await _context.Cards.FindAsync(id);
            return card;
        }
        public async Task<CardResponseVM> CreateNewCardAsync(CardRequestModel request)
        {
            var card = new Card
            {
                title = request.title,
                columnId = (long)request.columnId
            };
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            return _mapper.Map<CardResponseVM>(card);
        }

        public async Task MarkDueDateCompleteOrNotAsync(bool isDueDateComplete, long cardId)
        {
            var card = await FindCardAsync(cardId);
            if (card != null)
            {
                card.isDueDayComplete = isDueDateComplete;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Card> RemoveTimeOfCardAsync(long cardId)
        {
            var card = await FindCardAsync(cardId);
            if (card == null) return null;
                card.isDueDayComplete = false;
                card.endDate = null;
                card.startDate = null;
                await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> UpdateCardAsync(CardRequestModel request , long cardId)
        {
            var card = await FindCardAsync(cardId);
            if (card == null) return null;
            if (request.title != null) card.title = request.title;
            if (request.description != null) card.description = request.description;
            if (request.cover != null) card.cover = request.cover;
            if (request.columnId != null) card.columnId = (long)request.columnId;
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card> UpdateTimeOfCardAsync(DateTime? starDate, DateTime? endDate, long cardId)
        {
            var card = await FindCardAsync(cardId);
            if (card == null) return null;
            if (starDate != null) card.startDate = starDate;
            if (endDate != null) card.endDate = endDate;
            await _context.SaveChangesAsync();
            return card ;
        }

        public async Task<IList<CardResponseVM>> GetListCardByListIdAsync(long listId)
        {
            var cards = await _context.Cards
                                       .Include(c => c.Column)
                                       .Where(c => c.Column.columnId == listId)
                                       .ToArrayAsync();
            List<CardResponseVM> cards_vm = new List<CardResponseVM>();
            foreach (var card in cards)
            {
                cards_vm.Add(_mapper.Map<CardResponseVM>(card));
            }
            return cards_vm;
        }
    }
}

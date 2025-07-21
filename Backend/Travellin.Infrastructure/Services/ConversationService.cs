using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Conversation;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;

namespace Travellin.Infrastructure.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ConversationService(IConversationRepository conversationRepo, IUnitOfWork unitOfWork)
        {
            _conversationRepo = conversationRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Conversation> CreateOrGetConversationAsync(string user1Id, string user2Id)
        {
            var existing = await _conversationRepo.GetBetweenUsersAsync(user1Id, user2Id);
            if (existing != null) return existing;

            var conversation = new Conversation { User1Id = user1Id, User2Id = user2Id };
            _conversationRepo.Create(conversation);
            await _unitOfWork.SaveChangesAsync();
            return conversation;
        }

        public async Task<List<Conversation>> GetUserConversationsAsync(string userId)
        {
            return await _conversationRepo.GetUserConversationsAsync(userId);
        }
        public async Task<Conversation?> GetConversationByIdAsync(int conversationId)
        {
            return await _conversationRepo.GetByIdWithMessagesAsync(conversationId);
        }

        public async Task<bool> DeleteConversationAsync(int conversationId)
        {
            var conversation = await _conversationRepo.GetByIdAsync(conversationId);
            if (conversation == null)
                return false;

            // Step 1: Delete related messages
            var messages = await _unitOfWork.MessageRepository.GetMessagesByConversationIdAsync(conversationId);
            foreach (var message in messages)
            {
                _unitOfWork.MessageRepository.Delete(message);
            }

            // Step 2: Delete the conversation
            _conversationRepo.Delete(conversation);

            // Step 3: Save all changes
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<InboxDto>> GetInboxPreviewAsync(string userId)
        {
            var conversations = await _conversationRepo.GetInboxPreviewAsync(userId);
            return conversations.Select(c => new InboxDto
            {
                ConversationId = c.Id,
                Participant = c.User1Id == userId ? c.User2Id : c.User1Id,
                LastMessage = c.Messages.FirstOrDefault()?.Content,
                SentAt = c.Messages.FirstOrDefault()?.SentAt ?? DateTime.MinValue,
                IsUnread = c.Messages.FirstOrDefault()?.ReceiverId == userId && !(c.Messages.FirstOrDefault()?.IsRead ?? true)
            }).ToList();
        }

        public async Task<List<ConversationSearchResultDto>> SearchConversationsAsync(string userId, string query)
        {
            var conversations = await _conversationRepo.GetUserConversationsAsync(userId);
            var results = new List<ConversationSearchResultDto>();

            foreach (var convo in conversations)
            {
                var participant = convo.User1Id == userId ? convo.User2Id : convo.User1Id;
                var matchMessage = convo.Messages.FirstOrDefault(m => m.Content.Contains(query, StringComparison.OrdinalIgnoreCase));

                if (participant.Contains(query, StringComparison.OrdinalIgnoreCase) || matchMessage != null)
                {
                    results.Add(new ConversationSearchResultDto
                    {
                        ConversationId = convo.Id,
                        Participant = participant,
                        MatchedMessage = matchMessage?.Content
                    });
                }
            }

            return results;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }

}

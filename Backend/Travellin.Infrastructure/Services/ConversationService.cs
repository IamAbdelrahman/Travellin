using Microsoft.EntityFrameworkCore;
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

        public async Task<Conversation> CreateOrGetConversationWithPropertyAsync(string user1Id, string user2Id, string? propertyId = null)
        {
            try
            {
                Console.WriteLine($"CreateOrGetConversationWithPropertyAsync called with user1Id: {user1Id}, user2Id: {user2Id}, propertyId: {propertyId}");
                
                // Find existing conversation between these users
                var existing = await _conversationRepo.GetBetweenUsersAsync(user1Id, user2Id);
                Console.WriteLine($"Existing conversation found: {(existing != null ? existing.Id.ToString() : "null")}");
                
                if (existing != null)
                {
                    Console.WriteLine($"Returning existing conversation with ID: {existing.Id}");
                    // Load the conversation with proper includes
                    return await _conversationRepo.GetByIdWithMessagesAsync(existing.Id);
                }
                
                Console.WriteLine("No existing conversation found, creating new one...");
                
                // Create new conversation
                var conversation = new Conversation
                {
                    User1Id = user1Id,
                    User2Id = user2Id,
                    PropertyId = propertyId,
                    CreatedAt = DateTime.UtcNow
                };
                
                Console.WriteLine($"Created conversation object: User1Id: {conversation.User1Id}, User2Id: {conversation.User2Id}, PropertyId: {conversation.PropertyId}");
                
                await _conversationRepo.AddAsync(conversation);
                await _unitOfWork.SaveChangesAsync();
                Console.WriteLine($"Conversation saved with ID: {conversation.Id}");
                
                // Load the conversation with proper includes
                return await _conversationRepo.GetByIdWithMessagesAsync(conversation.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateOrGetConversationWithPropertyAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
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
            return conversations.Select(c =>
            {
                var otherUserId = c.User1Id == userId ? c.User2Id : c.User1Id;
                var lastMsg = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault();

                // Get the other user's information
                var otherUser = c.User1Id == userId ? c.User2 : c.User1;
                var participantName = otherUser?.UserName ?? $"User {otherUserId.Substring(0, 8)}";

                // Try to get the user profile for better display name
                if (otherUser?.UserProfile != null)
                {
                    var profile = otherUser.UserProfile;
                    if (!string.IsNullOrEmpty(profile.FirstName) || !string.IsNullOrEmpty(profile.LastName))
                    {
                        var fullName = $"{profile.FirstName ?? ""} {profile.LastName ?? ""}".Trim();
                        if (!string.IsNullOrEmpty(fullName))
                        {
                            participantName = fullName;
                        }
                    }
                }

                return new InboxDto
                {
                    ConversationId = c.Id,
                    Participant = participantName,
                    LastMessage = lastMsg?.Content,
                    SentAt = lastMsg?.SentAt ?? DateTime.MinValue,
                    IsUnread = c.Messages.Any(m => m.ReceiverId == userId && !m.IsRead)
                };
            }).OrderByDescending(dto => dto.SentAt).ToList();
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
        public async Task<bool> UserIsInConversationAsync(int conversationId, string userId)
        {
            var conversation = await _conversationRepo.GetByIdAsync(conversationId);
            if (conversation == null)
                return false;

            return conversation.User1Id == userId || conversation.User2Id == userId;
        }

        public async Task<List<Conversation>> GetAllConversationsAsync()
        {
            return await _conversationRepo.GetAllConversationsAsync();
        }

    }
}
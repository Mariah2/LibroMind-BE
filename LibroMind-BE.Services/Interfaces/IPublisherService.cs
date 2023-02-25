using LibroMind_BE.Services.Models;

namespace LibroMind_BE.Services.Interfaces
{
    public interface IPublisherService
    {
        public Task<IEnumerable<PublisherGetDTO>> FindPublisheresAsync();
        public Task<PublisherGetDTO> FindPublisherByIdAsync(int id);
        public Task AddPublisher(PublisherPostDTO PublisherToAdd);
        public Task UpdatePublisher(int id, PublisherPostDTO PublisherToUpdate);
        public Task DeletePublisher(int id);
    }
}

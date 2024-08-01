using Project1.Models;

namespace Project1.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<Contact> AddContactAsync(Contact contact);
        Task<Contact> UpdateContactAsync(int id, Contact contact);
        Task<Contact> DeleteContactAsync(int id);
    }
}

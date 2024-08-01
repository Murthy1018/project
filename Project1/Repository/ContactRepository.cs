using Microsoft.EntityFrameworkCore;
using Project1.Interfaces;
using Project1.Models;

namespace Project1.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly RegisterAPIDbContext _dbContext;

        public ContactRepository(RegisterAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            return await _dbContext.Contact.ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _dbContext.Contact.FindAsync(id);
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            _dbContext.Contact.Add(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> UpdateContactAsync(int id, Contact contact)
        {
            var existingContact = await _dbContext.Contact.FindAsync(id);
            if (existingContact == null)
            {
                return null;
            }
            existingContact.Name = contact.Name;
            existingContact.Email = contact.Email;
            existingContact.Subject = contact.Subject;
            existingContact.Message = contact.Message;
            await _dbContext.SaveChangesAsync();
            return existingContact;
        }

        public async Task<Contact> DeleteContactAsync(int id)
        {
            var existingContact = await _dbContext.Contact.FindAsync(id);
            if (existingContact == null)
            {
                return null;
            }
            _dbContext.Contact.Remove(existingContact);
            await _dbContext.SaveChangesAsync();
            return existingContact;
        }
    }
}

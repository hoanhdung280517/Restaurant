using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Contacts
{
    public class ContactService: IContactService
    {
        private readonly RSDbContext _context;
        public ContactService(RSDbContext context)
        {
            _context = context;
        }
        public async Task<List<ContactVm>> GetAll()
        {
            var contact = await _context.Contacts
                 .Select(x => new ContactVm()
                 {
                     Id = x.Id,
                     Email = x.Email,
                     UserName = x.Name,
                     PhoneNumber = x.PhoneNumber,
                     Message = x.Message,
                     ContactDate=x.ContactDate
                 }).ToListAsync();
            return contact;
        }
        public async Task<int> AddContact(ContactCreateRequest request)
        {
            if (request == null)
                throw new Exception("Table cannot be null");
            var contact = new Contact()
            {
                Name = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Message = request.Message,
                ContactDate = DateTime.Now
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }
        public async Task<List<ContactVm>> GetByDate(Time time)
        {
            var contact = await _context.Contacts.Select(r => new ContactTimeRequest()
            {
                Id = r.Id,
                UserName = r.Name,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                Message = r.Message,
                ContactDate = r.ContactDate,
            }).ToListAsync();
            var contacttime = new List<ContactVm>();
            {
                foreach (var item in contact)
                {
                    if (time.Fromdate <= item.ContactDate && time.Todate >= item.ContactDate )
                    {
                        contacttime.Add(new ContactVm()
                        {
                            Id = item.Id,
                            Email = item.Email,
                            UserName = item.UserName,
                            PhoneNumber = item.PhoneNumber,
                            Message = item.Message,
                            ContactDate = item.ContactDate
                        });
                    };
                }
            }
            return contacttime;
        }
        public async Task<ContactVm> GetById(int id)
        {
            var contact = _context.Contacts.Where(r => r.Id == id).Select(r => new ContactVm()
            {
                Id = r.Id,
                UserName = r.Name,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
                Message = r.Message,
                ContactDate = r.ContactDate,
            }).FirstOrDefault();

            return contact;
        }
    }
}

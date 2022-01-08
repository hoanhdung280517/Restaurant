using RSSolution.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Contacts
{
    public interface IContactService
    {
        Task<int> AddContact(ContactCreateRequest request);
        Task<List<ContactVm>> GetAll();
        Task<List<ContactVm>> GetByDate(Time time);
        Task<ContactVm> GetById(int id);

    }
}

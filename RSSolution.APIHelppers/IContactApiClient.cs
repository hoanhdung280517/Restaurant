using RSSolution.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelppers
{
    public interface IContactApiClient
    {
        Task<List<ContactVm>> GetAll();
        Task<List<ContactVm>> GetByDate(Time time);
        Task<ContactVm> GetById(int Id);
        Task<bool> Create(ContactCreateRequest request);
    }
}

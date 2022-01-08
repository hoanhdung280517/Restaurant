using RSSolution.ViewModels.Catalog.BookTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelppers
{
    public interface IBookTableApiClient
    {
        Task<List<BookTableViewModel>> GetAll();
        Task<List<BookTableViewModel>> GetByDate(RequestTime time);
        Task<bool> Create(BookTableRequest request);
        Task<bool> Edit(EditBookTableRequest request);
        Task<BookTableViewModel> GetById(int id);
    }
}

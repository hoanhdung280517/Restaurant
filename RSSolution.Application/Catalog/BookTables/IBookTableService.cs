using RSSolution.ViewModels.Catalog.BookTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.BookTables
{
    public interface IBookTableService
    {
        Task<int> Create(BookTableRequest request);
        Task<List<BookTableViewModel>> GetAll();
        Task<List<BookTableViewModel>> GetByDate(RequestTime time);
        Task<int> UpdateStatus(EditBookTableRequest request);
        Task<BookTableViewModel> GetById(int id);

    }
}

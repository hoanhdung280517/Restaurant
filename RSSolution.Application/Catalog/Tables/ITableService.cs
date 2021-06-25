using RSSolution.Data.Entities;
using RSSolution.ViewModels.Catalog.Table;
using RSSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Tables
{
    public interface ITableService 
    {
        Task<int> Create (TableCreateRequest request);
        Task<int> Update(TableUpdateRequest request);
        Task<int> Delete(int tableid);
        Task<List<TableVm>> GetAll();

        Task<TableVm> GetById(int id);


    }
}

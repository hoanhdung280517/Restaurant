using RSSolution.ViewModels.Catalog.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public interface ITableApiClient
    {
        Task<List<TableVm>> GetAll();
        Task<TableVm> GetById(int tableid);
        Task<bool> Create(TableCreateRequest request);
        Task<bool> Update(TableUpdateRequest request);
        Task<bool> Delete(int tableid);
    }
}
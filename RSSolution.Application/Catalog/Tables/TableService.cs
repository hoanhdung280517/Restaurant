using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Utilities.Exceptions;
using RSSolution.ViewModels.Catalog.Table;
using RSSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSolution.Data.Entities;

namespace RSSolution.Application.Catalog.Tables
{
    public class TableService: ITableService
    {
        private readonly RSDbContext _context;
        public TableService(RSDbContext context)
        {
            _context = context;
        }
        public async Task<List<TableVm>> GetAll()
        {
            var table = await _context.Tables
                 .Select(x => new TableVm()
                 {
                     Id = x.Id,
                     Name = x.Name,
                 }).ToListAsync();
            return table;
        }

        public async Task<TableVm> GetById(int id)
        {
            var table =  _context.Tables.Where(r=>r.Id == id).Select(r=>new TableVm() 
            {
                Id = r.Id,
                Name =r.Name
            }).FirstOrDefault();
            
            return table;
        }
        public async Task<int> Create (TableCreateRequest request)
        {
            if (request == null)
                throw new Exception("Table cannot be null");
            var table = new Table()
            {
                Id = request.Id,
                Name = request.Name
            };
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table.Id;
        }
        public async Task<int> Update(TableUpdateRequest request)
        {
            var table = await _context.Tables.FindAsync(request.Id);
            table.Name = request.Name;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int tableid)
        {
            var table = await _context.Tables.FindAsync(tableid);
            if (table == null) throw new RSException($"Cannot find a table: {tableid}");

            _context.Tables.Remove(table);

            return await _context.SaveChangesAsync();
        }
    }
}

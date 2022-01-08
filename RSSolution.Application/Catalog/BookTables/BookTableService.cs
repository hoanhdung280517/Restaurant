using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.Data.Enums;
using RSSolution.ViewModels.Catalog.BookTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.BookTables
{
    public class BookTableService : IBookTableService
    {
        private readonly RSDbContext _context;
        public BookTableService(RSDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookTableViewModel>> GetAll()
        {
            var booktable = await _context.BookTables
                 .Select(x => new ListBookTable()
                 {
                    Id = x.Id,
                    BookDate = x.BookDate,
                    UserId = x.UserId,
                    Note = x.Note,
                    CountAdults = x.CountAdults,
                    CountChilds =  x.CountChilds,
                    Status = x.Status
                 }).ToListAsync();
            var bookTableVm = new List<BookTableViewModel>();
            foreach (var item in booktable)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == item.UserId);
                bookTableVm.Add(new BookTableViewModel()
                {
                    Id = item.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    BookDate = item.BookDate,
                    Note = item.Note,
                    CountAdults = item.CountAdults,
                    CountChilds = item.CountChilds,
                    Status = item.Status
                });
            }
            return bookTableVm;
        }
        public async Task<int> Create(BookTableRequest request)
        {
            if (request == null)
                throw new Exception("Book cannot be null");
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            var userid = user.Id;
            var bookTable = new BookTable()
            {
                UserId = user.Id,
                BookDate = request.BookDate,
                CountAdults =request.CountAdults,
                CountChilds = request.CountChilds,
                Note = request.Note,
                Status = BookTableStatus.InProgress,
            };
            _context.BookTables.Add(bookTable);
            await _context.SaveChangesAsync();
            return bookTable.Id;
        }
        public async Task<List<BookTableViewModel>> GetByDate(RequestTime time)
        {
            var allbooktable = await _context.BookTables
                 .Select(x => new ListBookTable()
                 {
                     Id = x.Id,
                     BookDate = x.BookDate,
                     UserId = x.UserId,
                     Note = x.Note,
                     CountAdults = x.CountAdults,
                     CountChilds = x.CountChilds,
                     Status = x.Status
                 }).ToListAsync();
            var bookTableByDate = new List<BookTableViewModel>();
            foreach (var item in allbooktable)
            {
                var date = item.BookDate.ToShortDateString();
                var Time = time.Time.ToShortDateString();
                if (date == Time)
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == item.UserId);
                    bookTableByDate.Add(new BookTableViewModel()
                    {
                        Id = item.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        BookDate = item.BookDate,
                        Note = item.Note,
                        CountAdults = item.CountAdults,
                        CountChilds = item.CountChilds,
                        Status = item.Status
                    });
                }
            }
            return bookTableByDate;

        }
        public async Task<int> UpdateStatus (EditBookTableRequest request)
        {
            var booktable = await _context.BookTables.FindAsync(request.Id);
            booktable.Status = request.Status;
            return await _context.SaveChangesAsync();
        }
        public async Task<BookTableViewModel> GetById(int id)
        {
            var listbooktable = _context.BookTables.Where(r => r.Id == id).Select(r => new ListBookTable()
            {
                Id = r.Id,
                BookDate = r.BookDate,
                UserId = r.UserId,
                Note = r.Note,
                CountAdults = r.CountAdults,
                CountChilds = r.CountChilds,
                Status = r.Status
            }).FirstOrDefault();
            var user = _context.Users.FirstOrDefault(u => u.Id == listbooktable.UserId);
            var bookTableVm = new BookTableViewModel()
            {
                Id = listbooktable.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BookDate = listbooktable.BookDate,
                Note = listbooktable.Note,
                CountAdults = listbooktable.CountAdults,
                CountChilds = listbooktable.CountChilds,
                Status = listbooktable.Status
            };
            return bookTableVm;
        }
    }
}

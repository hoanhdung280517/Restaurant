using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.ViewModels.Common;
using RSSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.System.Users
{
    public class PDService : IPDService
    {
        private readonly RSDbContext _context;
        public PDService(RSDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<List<DistrictVm>>> GetAllDistrict()
        {
           var district = await _context.Districts.Select(x => new DistrictVm()
           {
               Id = x.Id,
               Name = x.Name,
               Prefix = x.Prefix
           }).ToListAsync();
            return new ApiSuccessResult<List<DistrictVm>>(district);
        }
        public async Task<ApiResult<List<ProvinceVm>>> GetAllProvince()
        {
            var province = await _context.Provinces.Select(x => new ProvinceVm()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            }).ToListAsync();
            return new ApiSuccessResult<List<ProvinceVm>>(province);
        }
    }
}

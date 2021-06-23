﻿using RSSolution.ViewModels.Common;
using RSSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.System.Users
{
    public interface IPDService
    {
        Task<ApiResult<List<DistrictVm>>> GetAllDistrict();
        Task<ApiResult<List<ProvinceVm>>> GetAllProvince();
    }
}

using RSSolution.ViewModels.Common;
using RSSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public interface IPDApiClient
    {
        Task<List<DistrictVm>> GetAllDistrict();
        Task<List<ProvinceVm>> GetAllProvince();
    }
}

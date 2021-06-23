using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RSSolution.ViewModels.Common;
using RSSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public class PDApiClient : BaseApiClient , IPDApiClient
    {
        public PDApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<DistrictVm>> GetAllDistrict()
        {
            return await GetListAsync<DistrictVm>("/api/district");
        }
        public async Task<List<ProvinceVm>> GetAllProvince()
        {
            return await GetListAsync<ProvinceVm>("/api/province");
        }
    }
}

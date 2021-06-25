using RSSolution.APIHelpers;
using RSSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RSSolution.Utilities.Constants;
using System.Net.Http.Headers;
using RSSolution.ViewModels.Catalog.Table;

namespace RSSolution.APIHelpers
{
    public class TableApiClient : BaseApiClient, ITableApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TableApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<TableVm>> GetAll()
        {
            return await GetListAsync<TableVm>("/api/table");
        }

        public async Task<TableVm> GetById( int id)
        {
            return await GetAsync<TableVm>($"/api/table/{id}");
        }
        public async Task<bool> Create(TableCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");

            var response = await client.PostAsync($"/api/table/", requestContent);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Update(TableUpdateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            //requestContent.Add(new StringContent(request.Id.ToString()), "id");

            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");

            var response = await client.PutAsync($"/api/table/" +request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(int id)
        {
            return await Delete($"/api/table/{id}");
        }
    }
}
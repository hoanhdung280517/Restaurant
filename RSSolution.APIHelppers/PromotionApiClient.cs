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
using RSSolution.ViewModels.Catalog.Promotions;

namespace RSSolution.APIHelpers
{
    public class PromotionApiClient : BaseApiClient, IPromotionApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PromotionApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<PromotionVM>> GetAll()
        {
            return await GetListAsync<PromotionVM>("/api/promotion");
        }

        public async Task<PromotionVM> GetById(int id)
        {
            return await GetAsync<PromotionVM>($"/api/promotion/{id}");
        }
        public async Task<bool> Create(PromotionCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.DiscountPercent.ToString()), "discountPercent");
            requestContent.Add(new StringContent(request.FromDate.ToString()), "fromDate");
            requestContent.Add(new StringContent(request.ToDate.ToString()), "toDate");
            requestContent.Add(new StringContent(request.Code.ToString()), "code");

            var response = await client.PostAsync($"/api/promotion/", requestContent);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Update(PromotionUpdateRequest request)
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

            requestContent.Add(new StringContent(request.DiscountPercent.ToString()), "discountPercent");
            requestContent.Add(new StringContent(request.FromDate.ToString()), "fromDate");
            requestContent.Add(new StringContent(request.ToDate.ToString()), "toDate");
            requestContent.Add(new StringContent(request.Code.ToString()), "code");

            var response = await client.PutAsync($"/api/promotion/" +request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(int id)
        {
            return await Delete($"/api/promotion/{id}");
        }
    }
}
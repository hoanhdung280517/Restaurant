using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RSSolution.APIHelpers;
using RSSolution.ViewModels.Catalog.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAdmin.Controllers
{
    public class TableController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly ITableApiClient _tableApiClient;

        public TableController(
            IConfiguration configuration,
            ITableApiClient tableApiClient)
        {
            _configuration = configuration;
            _tableApiClient = tableApiClient;
        }
        public async Task<IActionResult> Index()
        {
            var table = await _tableApiClient.GetAll();
            return View(table.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] TableCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _tableApiClient.Create(request);
            if (result)
            {
                TempData["result"] = "Add new table is successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Add new table failed");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var table = await _tableApiClient.GetById(id);
            var editVm = new TableUpdateRequest()
            {
                Id = table.Id,
                Name = table.Name,
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] TableUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _tableApiClient.Update(request);
            if (result)
            {
                TempData["result"] = "Update table successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Update table failed");
            return View(request);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new TableDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TableDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _tableApiClient.Delete(request.Id);
            if (result)
            {
                TempData["result"] = "Delete successful";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Delete failed");
            return View(request);
        }
    }
}

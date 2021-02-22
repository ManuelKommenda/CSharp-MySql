using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlDevices.Core.Contracts;
using MySqlDevices.Core.DataTransferObject;
using MySqlDevices.Core.Entities;
using MySqlDevices.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlDevices.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IList<Device> devices = await _unitOfWork.Devices.GetAllAsync();
            IList<UsageDto> deviceUsages;
            int selectDeviceId = -1;
            if (devices.Any())
            {
                selectDeviceId = devices.First().Id;
                deviceUsages = (await _unitOfWork.Usages.GetByDeviceIdAsync(selectDeviceId))
                    .Select(u => new UsageDto(u))
                    .ToList();
            }
            else
            {
                deviceUsages = new List<UsageDto>();
            }
            HomeIndexViewModel viewModel = new HomeIndexViewModel(deviceUsages, devices, selectDeviceId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexViewModel viewModel)
        {
            IList<Device> devices = await _unitOfWork.Devices.GetAllAsync();
            int selectDeviceId = viewModel.SelectedDeviceId;
            var device = await _unitOfWork.Devices.FindByIdAsync(selectDeviceId);
            IList<UsageDto> deviceUsages;
            if (device != null)
            {
                deviceUsages = (await _unitOfWork.Usages.GetByDeviceIdAsync(selectDeviceId))
                    .Select(u => new UsageDto(u))
                    .ToList();
            }
            else
            {
                deviceUsages = new List<UsageDto>();
            }

            HomeIndexViewModel vM = new HomeIndexViewModel(deviceUsages, devices, selectDeviceId);
            return View(vM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
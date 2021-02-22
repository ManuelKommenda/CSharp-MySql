using MySqlDevices.Core.DataTransferObject;
using MySqlDevices.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlDevices.Web.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<UsageDto> DeviceUsages { get; set; }
        public int SelectedDeviceId { get; set; }
        public SelectList DevicesSelectList { get; set; }

        public HomeIndexViewModel() { }

        public HomeIndexViewModel(IEnumerable<UsageDto> deviceUsages, IList<Device> devices, int selectedDeviceId)
        {
            DevicesSelectList = new SelectList(devices, "Id", "Name", selectedDeviceId);
            SelectedDeviceId = selectedDeviceId;
            DeviceUsages = deviceUsages;
        }
    }
}


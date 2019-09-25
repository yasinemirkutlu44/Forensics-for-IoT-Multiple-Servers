using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forensic
{
    public class Device
    {
        public string Owner { get; set; }
        public string User { get; set; }
        public string SerialNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string IPAddress { get; set; }
        public string IMEI { get; set; }
        public string DeviceID { get; set; } 
        public string DeviceType { get; set;}
        public string DeviceName { get; set; }
    }
}

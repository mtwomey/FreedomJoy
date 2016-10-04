using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FreedomJoy
{
    class Config
    {
        private readonly JToken _config;
        public readonly JToken MappingsSimpleButton;
        public readonly JToken MappingsVirtualAxis;
        public readonly JToken MappingsAxis;
        public readonly JToken PhysicalDevices;
        public readonly JToken VjoyDevices;
        public Config()
        {
            _config = JToken.Parse(File.ReadAllText("config.json"));
            // (string)_config["mappings"]["simpleButton"][0]["physicalDevice"]["id"]
            //(string)MappingsSimpleButton[0]["physicalDevice"]["id"]

            PhysicalDevices = _config["physicalDevices"];
            VjoyDevices = _config["vJoyDevices"];
            MappingsSimpleButton = _config["mappings"]["simpleButton"];
            MappingsVirtualAxis = _config["mappings"]["virtualAxis"];
            MappingsAxis = _config["mappings"]["axis"];
        }

        public string GetPhysicalDeviceGuidFromId(uint id)
        {
            return  PhysicalDevices.SelectToken("[?(@.id == " + id + ")]")["guid"].ToObject<string>(); // Holy shit I miss javascript...
        }
        public uint GetVjoyDeviceSystemIdFromId(uint id)
        {
            return VjoyDevices.SelectToken("[?(@.id == " + id + ")]")["systemId"].ToObject<uint>();
        }
    }
}

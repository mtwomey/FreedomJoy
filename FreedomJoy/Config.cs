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
            foreach (JToken map in MappingsSimpleButton)
            {
                int x = 10;
            }
            int stophere = 10;
        }
    }
}

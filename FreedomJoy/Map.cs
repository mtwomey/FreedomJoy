using System.Collections.Generic;
using FreedomJoy.Controllers;
using FreedomJoy.Mappings;
using FreedomJoy.vJoy;
using Newtonsoft.Json.Linq;

namespace FreedomJoy
{
    class Map
    {
        private readonly Config _config;
        private readonly Dictionary<string, Controller> _controllers;
        private readonly Dictionary<string, Vcontroller> _vcontrollers;

        public Map()
        {
            _controllers = new Dictionary<string, Controller>();
            _vcontrollers = new Dictionary<string, Vcontroller>();
            _config = new Config();
            _initPhysicalDevices();
            _initVjoyDevices();
            _initSimpleButtonMappings();
        }

        private void _initPhysicalDevices()
        {
            foreach (JToken physicalDevice in _config.PhysicalDevices)
            {
                string id = (string) physicalDevice["id"];
                int systemId = (int) physicalDevice["systemId"];
                Controller newController = new Controller(systemId);
                _controllers.Add(id, newController);
            }
        }

        private void _initVjoyDevices()
        {
            foreach (JToken vJoyDevice in _config.VjoyDevices)
            {
                string id = (string)vJoyDevice["id"];
                uint systemId = (uint)vJoyDevice["systemId"];
                Vcontroller newVcontroller = new Vcontroller(systemId);
                _vcontrollers.Add(id, newVcontroller);
            }
        }

        private void _initSimpleButtonMappings()
        {
            foreach (JToken map in _config.MappingsSimpleButton)
            {
//                SimpleButtonMapping simpleButtonMapping = new SimpleButtonMapping(
//                    
//                );
            }
        }
    }
}

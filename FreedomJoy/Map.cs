using System;
using System.Collections.Generic;
using System.Linq;
using FreedomJoy.Controllers;
using FreedomJoy.Mappings;
using FreedomJoy.vJoy;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace FreedomJoy
{
    class Map
    {
        private readonly Config _config;
        private readonly Dictionary<string, Controller> _controllers;
        private readonly Dictionary<string, Vcontroller> _vcontrollers;
        private readonly ControllerMaps _controllerMaps = new ControllerMaps();
        private readonly HashSet<Controller> _activeControllers = new HashSet<Controller>();
        private readonly HashSet<Vcontroller> _activeVcontrollers = new HashSet<Vcontroller>();
        private readonly uint _updateRate = 20;
        private bool _keepRunning = true;

        public Map()
        {
            _controllers = new Dictionary<string, Controller>();
            _vcontrollers = new Dictionary<string, Vcontroller>();
            _config = new Config();
            _initPhysicalDevices();
            _initVjoyDevices();
            _initSimpleButtonMappings();
            _initVirtualAxisMappings();
            _initAxisMappings();
        }

        private void _initPhysicalDevices()
        {
            for (int deviceNumber = 0; deviceNumber < _config.PhysicalDevices.Count(); deviceNumber++)
            {
                JToken physicalDevice = _config.PhysicalDevices[deviceNumber];
                Controller controller = ControllerFactory.GetPhysicalController((string)physicalDevice["guid"]);
                _activeControllers.Add(controller);

                for (int povNumber = 0; povNumber < physicalDevice["povs"].Count(); povNumber++)
                {
                    JToken pov = physicalDevice["povs"][povNumber];
                    if ((string) pov["type"] == "button4")
                    {
                        controller.PovsByName[(string)pov["name"]].SetType(Pov.PovType.Button4);
                    }
                    if ((string)pov["type"] == "button8")
                    {
                        controller.PovsByName[(string)pov["name"]].SetType(Pov.PovType.Button8);
                    }
                }
            }
        }

        private void _initVjoyDevices()
        {
            for (int deviceNumber = 0; deviceNumber < _config.VjoyDevices.Count(); deviceNumber++)
            {
                JToken vJoyDevice = _config.VjoyDevices[deviceNumber];
                Vcontroller vcontroller = ControllerFactory.GetvJoyController((uint)vJoyDevice["systemId"]);
                _activeVcontrollers.Add(vcontroller);
            }
        }

        private void _initSimpleButtonMappings() // Todo: abstract these _init mapping methods so we're not repeating so much stuff...
        {
            foreach (JToken mapping in _config.MappingsSimpleButton)
            {
                var requestedPhysicalDevice = (uint)mapping["physicalDevice"]["id"];
                var physicalDeviceGuid = _config.GetPhysicalDeviceGuidFromId(requestedPhysicalDevice);
                var requestedVjoyDevice = (uint)mapping["vJoyDevice"]["id"];
                var vJoyDeviceSystemId = _config.GetVjoyDeviceSystemIdFromId(requestedVjoyDevice);

                SimpleButtonMapping newSimpleButtonMapping = new SimpleButtonMapping(
                      controllerPressedButtons: mapping["physicalDevice"]["buttons"].ToObject<string[]>(),
                      controllerNotPressedButtons: mapping["physicalDevice"]["notButtons"] != null ? mapping["physicalDevice"]["notButtons"].ToObject<string[]>() : new string []{},
                      vJoyPressedButtons: mapping["vJoyDevice"]["buttons"].ToObject<string[]>(),
                      controller: ControllerFactory.GetPhysicalController(physicalDeviceGuid),
                      vcontroller: ControllerFactory.GetvJoyController(vJoyDeviceSystemId)
                );
                _controllerMaps.Add(newSimpleButtonMapping);
            }
        }

        private void _initVirtualAxisMappings()
        {
            foreach (JToken mapping in _config.MappingsVirtualAxis)
            {
                var requestedPhysicalDevice = (uint)mapping["physicalDevice"]["id"];
                var physicalDevice = ControllerFactory.GetPhysicalController(_config.GetPhysicalDeviceGuidFromId(requestedPhysicalDevice));
                var requestedVjoyDevice = (uint)mapping["vJoyDevice"]["id"];
                var vJoyDevice = ControllerFactory.GetvJoyController(_config.GetVjoyDeviceSystemIdFromId(requestedVjoyDevice));

                VirtualAxisMapping newVirtualAxisMapping = new VirtualAxisMapping(
                    increaseButtons: mapping["physicalDevice"]["increaseButtons"].ToObject<string[]>(),
                    increaseNotButtons: mapping["physicalDevice"]["increaseNotButtons"] != null ? mapping["physicalDevice"]["increaseNotButtons"].ToObject<string[]>() : new string[] {},
                    decreaseButtons: mapping["physicalDevice"]["decreaseButtons"].ToObject<string[]>(),
                    decreaseNotButtons: mapping["physicalDevice"]["decreaseNotButtons"] != null ? mapping["physicalDevice"]["decreaseNotButtons"].ToObject<string[]>() : new string[] {},
                    controller: physicalDevice,
                    vcontroller: vJoyDevice,
                    ratePerSecond: (int)mapping["settings"]["ratePerSecond"],
                    virtualAxis: vJoyDevice.AxesByName[(string)mapping["vJoyDevice"]["axis"]]
                );
                _controllerMaps.Add(newVirtualAxisMapping);
            }
        }

        private void _initAxisMappings()
        {
            foreach (JToken mapping in _config.MappingsAxis)
            {
                var requestedPhysicalDevice = (uint)mapping["physicalDevice"]["id"];
                var physicalDevice = ControllerFactory.GetPhysicalController(_config.GetPhysicalDeviceGuidFromId(requestedPhysicalDevice));
                var requestedVjoyDevice = (uint)mapping["vJoyDevice"]["id"];
                var vJoyDevice = ControllerFactory.GetvJoyController(_config.GetVjoyDeviceSystemIdFromId(requestedVjoyDevice));

                AxisMapping newAxisMapping = new AxisMapping(
                    physicalAxis: (string)mapping["physicalDevice"]["axis"],
                    vjoyAxis: (string)mapping["vJoyDevice"]["axis"],
                    controller: physicalDevice,
                    vcontroller: vJoyDevice
                );
                _controllerMaps.Add(newAxisMapping);
            }
        }

        public void Run(CancellationToken cancellationToken)
        {
            while (true)
            {
                foreach (Controller controller in _activeControllers)
                {
                    controller.Update();
                }
                _controllerMaps.Update();
                foreach (Vcontroller vcontroller in _activeVcontrollers)
                {
                    vcontroller.Update();
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                Thread.Sleep((int)_updateRate);
            }
        }
    }
}

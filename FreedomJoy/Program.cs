﻿using System;
using System.Collections.Generic;
using FreedomJoy.Controllers;
using FreedomJoy.vJoy;
using SlimDX.DirectInput;


namespace FreedomJoy
{
    class Program
    {
        static void Main(string[] args)
        {
            //ButtonReadout();
            //MappingConditions();
            //VJoyTest01();
            try
            {
                //StructTest();
                //MappingLoopTest01();
                //printVjoyStatus();

                PrintControllerInfo();
                printVjoyStatus(1);
                MappingLoopTest02(
                    controllerId: 0,
                    vJoyId: 1    
                );


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        static void PrintControllerInfo()
        {
            Console.WriteLine("\nSearching for DirectInput Devices...\n");
            DirectInput dinput = new DirectInput();
            IList<DeviceInstance> devices = dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly);
            for (int i = 0; i < devices.Count; i++)
            {
                DeviceInstance device = devices[i];
                Console.WriteLine("Device #" + i + " Found: " + device.ProductName);
            }
            Console.WriteLine("Done.\n");
        }

        static void ReinstTest()
        {
            for (int i = 0; i < 10; i++)
            {
                printVjoyStatus(1); 
            }
        }
        static void printVjoyStatus(uint vJoyId)
        {
            Vcontroller vJoy = new Vcontroller(vJoyId);
            foreach (KeyValuePair<string, string> entry in vJoy.GetInfo())
            {
                Console.WriteLine(entry.Key + ": " + entry.Value);
            }
            vJoy.Close();
        }


        static void MappingLoopTest02(int controllerId, uint vJoyId)
        {
            Controller controller = new Controller(controllerId);
            Vcontroller vcontroller = new Vcontroller(vJoyId);

            ControllerMaps controllerMaps = new ControllerMaps();


            IMapping map03 = new SimpleButtonMapping(
                controllerPressedButtons: new int[] {1},
                vJoyPressedButtons: new int[] {11, 12},
                controller: controller,
                vcontroller: vcontroller
            );

            IMapping map04 = new RapidFireButtonMapping(
                controllerPressedButtons: new int[] { 2 },
                vJoyPressedButtons: new int[] { 3 },
                controller: controller,
                vcontroller: vcontroller,
                rapidFireRate: 100
            );

            IMapping map05 = new RapidFireButtonMapping(
                controllerPressedButtons: new int[] { 0 },
                vJoyPressedButtons: new int[] { 1 },
                controller: controller,
                vcontroller: vcontroller,
                rapidFireRate: 3000
            );

            controllerMaps.Add(map03);
            controllerMaps.Add(map04);
            controllerMaps.Add(map05);

            while (true)
            {
                controller.Update();
                controllerMaps.Update();
                vcontroller.Update();
                System.Threading.Thread.Sleep(vcontroller.UpdateRate);
            }

        }

        static void MappingLoopTest01(int controllerId, uint vJoyId)
        {
            // Setup
            Controller controller = new Controller(controllerId);
            controller.ConfigurePov(0, Pov.PovType.Button4); // Make dpad 4 buttons
            Console.WriteLine(controller.Buttons.Count);
            Vcontroller vJoy = new Vcontroller(vJoyId);
            Console.WriteLine(vJoy.Buttons.Count);
            Console.WriteLine("Test ID: 4");

            // Setup mapping
            Mapping map01 = new Mapping(
                conditions: new List<ICondition>()
                {
                    new Condition(delegate()
                    {
                        return (controller.Buttons[0].State == true); // A pressed
                    }),
                     new Condition(delegate()
                    {
                        return (controller.Buttons[1].State == true); // B pressed
                    }),
                    new Condition(delegate()
                    {
                        return (controller.Buttons[2].State == false); // X NOT pressed
                    }),
                    new Condition(delegate()
                    {
                        return (controller.Buttons[3].State == false); // Y NOT pressed
                    })
                },
                trueAction: delegate()
                {
                    vJoy.Buttons[7].State = true; // Press button 8 on vJoy
                },
                falseAction: delegate()
                {
                    vJoy.Buttons[7].State = false; // Unpress button 8 on vJoy
                }
            );

            Mapping map02 = new Mapping(
                conditions: new List<ICondition>()
                {
                    new Condition(delegate()
                    {
                        return (controller.Buttons[10].State == true); // dpad up
                    })
                }, 
                trueAction: delegate()
                {
                    vJoy.Buttons[1].State = true; // Press button 2 on vJoy
                },
                falseAction: delegate()
                {
                    vJoy.Buttons[1].State = false; // Unpress button 2 on vJoy
                }
            );

            // Add mappings to list
            ControllerMaps controllerMaps = new ControllerMaps();
            controllerMaps.Add(map01);
            controllerMaps.Add(map02);

            // Here we go!
            while (true)
            {
                controller.Update();
                controllerMaps.Update();
                vJoy.Update();
                System.Threading.Thread.Sleep(20);
            }
        }


//        static void StructTest()
//        {
//            Vcontroller vJoy = new Vcontroller(1);
//            Console.WriteLine("Test #2");
//            vJoy.JoystickState joystickState = new vJoy.JoystickState();
//
//            Console.WriteLine("X Axis: " + joystickState.AxisX);
//
//            while (true)
//            {
//                vJoy.Buttons[3].State = true;
//                vJoy.Buttons[3].State = false;
//                vJoy.Buttons[4].State = true;
//                vJoy.Update();
//
//                System.Threading.Thread.Sleep(20);
//            }
//
//
//        }
        static void MapTest01()
        {
            Controller controller = new Controller(0);
            Console.WriteLine(controller.Buttons.Count);
            Vcontroller vJoy = new Vcontroller(1);
            Console.WriteLine(vJoy.Buttons.Count);
            Console.WriteLine("1");

            while (true)
            {
                controller.Update();
                vJoy.Buttons[0].State = controller.Buttons[0].State;
                System.Threading.Thread.Sleep(20);

                //vJoy.Buttons[0].State = !vJoy.Buttons[0].State;
                //System.Threading.Thread.Sleep(500);
            }

            controller.Close();
            vJoy.Close();
        }

        static void VJoyTest01()
        {
            Vcontroller vJoy = new Vcontroller(1);
            foreach (VjoyButton button in vJoy.Buttons)
            {
                button.State = true;
            }
            vJoy.Close();
        }

        //static void MappingConditions()
        //{
        //    Controller controller = new Controller(0);
        //    controller.ConfigurePov(0, Pov.PovType.Button8);
        //    controller.ConfigurePov(0, Pov.PovType.Button4);
        //    while (true)
        //    {
        //        controller.Update();
        //        Mapping mapping = new Mapping(); // A and B pressed, X and Y both NOT pressed
        //        mapping.Conditions.Add(new Condition(delegate ()
        //        {
        //            return (controller.Buttons[0].State == true);
        //        }));
        //        mapping.Conditions.Add(new Condition(delegate ()
        //        {
        //            return (controller.Buttons[1].State == true);
        //        }));
        //        mapping.Conditions.Add(new Condition(delegate ()
        //        {
        //            return (controller.Buttons[2].State == false);
        //        }));
        //        mapping.Conditions.Add(new Condition(delegate ()
        //        {
        //            return (controller.Buttons[3].State == false);
        //        }));
        //        Console.WriteLine("Check: " + mapping.State);
        //        System.Threading.Thread.Sleep(1000);
        //    }
        //}
        static void ButtonReadout()
        {
            Controller controller = new Controller(0);
            controller.ConfigurePov(0, Pov.PovType.Button8);
            controller.ConfigurePov(0, Pov.PovType.Button4); // Did this twice, just to make sure that it could "undo" itself if needed...
            while (true)
            {
                controller.Update();
                foreach (StandardButton button in controller.Buttons)
                {
                    Console.WriteLine("Button " + (button.ButtonNumber) + ": " + button.State);
                }

                foreach (Pov pov in controller.Povs)
                {
                    Console.WriteLine("POV " + (pov.PovNumber) + ": " + pov.Value);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}

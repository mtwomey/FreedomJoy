using System;
using System.Collections.Generic;
using vJoyInterfaceWrap;

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
                MappingLoopTest01();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        static void MappingLoopTest01()
        {
            // Setup
            Controller controller = new Controller(0);
            controller.ConfigurePov(0, Pov.PovType.Button4); // Make dpad 4 buttons
            Console.WriteLine(controller.Buttons.Count);
            Vcontroller vJoy = new Vcontroller(1);
            Console.WriteLine(vJoy.Buttons.Count);
            Console.WriteLine("Test ID: 4");

            // Setup mapping
            Mapping map01 = new Mapping(
                conditions: new List<Condition>()
                {
                    new Condition(delegate()
                    {
                        return (controller.Buttons[0].Value == true); // A pressed
                    }),
                     new Condition(delegate()
                    {
                        return (controller.Buttons[1].Value == true); // B pressed
                    }),
                    new Condition(delegate()
                    {
                        return (controller.Buttons[2].Value == false); // X NOT pressed
                    }),
                    new Condition(delegate()
                    {
                        return (controller.Buttons[3].Value == false); // Y NOT pressed
                    })
                },
                trueAction: delegate()
                {
                    vJoy.Buttons[7].Value = true; // Press button 8 on vJoy
                },
                falseAction: delegate()
                {
                    vJoy.Buttons[7].Value = false; // Unpress button 8 on vJoy
                }
            );

            Mapping map02 = new Mapping(
                conditions: new List<Condition>()
                {
                    new Condition(delegate()
                    {
                        return (controller.Buttons[10].Value == true); // dpad up
                    })
                }, 
                trueAction: delegate()
                {
                    vJoy.Buttons[1].Value = true; // Press button 2 on vJoy
                },
                falseAction: delegate()
                {
                    vJoy.Buttons[1].Value = false; // Unpress button 2 on vJoy
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


        static void StructTest()
        {
            Vcontroller vJoy = new Vcontroller(1);
            Console.WriteLine("Test #2");
            vJoy.JoystickState joystickState = new vJoy.JoystickState();

            Console.WriteLine("X Axis: " + joystickState.AxisX);

            while (true)
            {
                vJoy.Buttons[3].Value = true;
                vJoy.Buttons[3].Value = false;
                vJoy.Buttons[4].Value = true;
                vJoy.Update();

                System.Threading.Thread.Sleep(20);
            }


        }
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
                vJoy.Buttons[0].Value = controller.Buttons[0].Value;
                System.Threading.Thread.Sleep(20);

                //vJoy.Buttons[0].Value = !vJoy.Buttons[0].Value;
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
                button.Value = true;
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
        //            return (controller.Buttons[0].Value == true);
        //        }));
        //        mapping.Conditions.Add(new Condition(delegate ()
        //        {
        //            return (controller.Buttons[1].Value == true);
        //        }));
        //        mapping.Conditions.Add(new Condition(delegate ()
        //        {
        //            return (controller.Buttons[2].Value == false);
        //        }));
        //        mapping.Conditions.Add(new Condition(delegate ()
        //        {
        //            return (controller.Buttons[3].Value == false);
        //        }));
        //        Console.WriteLine("Check: " + mapping.Value);
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
                foreach (Button button in controller.Buttons)
                {
                    Console.WriteLine("Button " + (button.ButtonNumber) + ": " + button.Value);
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

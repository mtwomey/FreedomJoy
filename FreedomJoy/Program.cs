using System;
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
                StructTest();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
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

        static void MappingConditions()
        {
            Controller controller = new Controller(0);
            controller.ConfigurePov(0, Pov.PovType.Button8);
            controller.ConfigurePov(0, Pov.PovType.Button4);
            while (true)
            {
                controller.Update();
                Mapping mapping = new Mapping(); // A and B pressed, X and Y both NOT pressed
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (controller.Buttons[0].Value == true);
                }));
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (controller.Buttons[1].Value == true);
                }));
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (controller.Buttons[2].Value == false);
                }));
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (controller.Buttons[3].Value == false);
                }));
                Console.WriteLine("Check: " + mapping.Value);
                System.Threading.Thread.Sleep(1000);
            }
        }
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

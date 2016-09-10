using System;

namespace FreedomJoy
{
    class Program
    {
        static void Main(string[] args)
        {
            //ButtonReadout();
            //MappingConditions();
            VJoyTest01();
        }

        static void VJoyTest01()
        {
            new Vjoy().GetInfo();
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

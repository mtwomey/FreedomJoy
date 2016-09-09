using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using SlimDX;
using SlimDX.DirectInput;
using SlimDX.XInput;

namespace FreedomJoy
{
    class Program
    {
        private static Controller _controller;
        static void Main(string[] args)
        {
            _controller = new Controller(0);
            _controller.ConfigurePov(0, Pov.PovType.Button8);
            _controller.ConfigurePov(0, Pov.PovType.Button4);
            MappingConditions();
        }

        static void MappingConditions()
        {
            while (true)
            {
                _controller.Update();
                Mapping mapping = new Mapping(); // A and B pressed, X and Y both NOT pressed
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (_controller.Buttons[0].Value == true);
                }));
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (_controller.Buttons[1].Value == true);
                }));
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (_controller.Buttons[2].Value == false);
                }));
                mapping.Conditions.Add(new Condition(delegate ()
                {
                    return (_controller.Buttons[3].Value == false);
                }));
                Console.WriteLine("Check: " + mapping.Value);
                System.Threading.Thread.Sleep(1000);
            }
        }
        static void ButtonReadout()
        {
            while (true)
            {
                _controller.Update();
                foreach (Button button in _controller.Buttons)
                {
                    Console.WriteLine("Button " + (button.ButtonNumber) + ": " + button.Value);
                }

                foreach (Pov pov in _controller.Povs)
                {
                    Console.WriteLine("POV " + (pov.PovNumber) + ": " + pov.Value);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}

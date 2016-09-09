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
            loop1();
        }

        static void loop1()
        {
            int[] povs;
            while (true)
            {
                _controller.Update();
                foreach (Button button in _controller.GetButtons())
                {
                    Console.WriteLine("Button " + (button.ButtonNumber) + ": " + button.Value);
                }

                foreach (Pov pov in _controller.GetPovs())
                {
                    Console.WriteLine("POV " + (pov.PovNumber) + ": " + pov.Value);
                }

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}

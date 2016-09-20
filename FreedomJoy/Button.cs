using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreedomJoy
{
    public abstract class Button
    {
        public int ButtonNumber { get; set; }
        public Controller ParentController;
        public abstract bool State { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreedomJoy
{
    public interface IButton
    {
        int ButtonNumber { get; set; }
        bool State { get; }
    }
}

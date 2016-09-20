using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace FreedomJoy
{

    public class Condition : ICondition
    {
        public bool State
        {
            get { return _condition(); }
        }

        private readonly Func<bool> _condition;

        public Condition(Func<bool> condition)
        {
            _condition = condition;
        }
    }
}

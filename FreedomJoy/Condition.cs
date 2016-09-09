using System;

namespace FreedomJoy
{
    class Condition
    {
        public bool Value
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

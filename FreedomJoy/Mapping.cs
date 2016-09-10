using System;
using System.Collections.Generic;
using System.Data;

namespace FreedomJoy
{
    class Mapping
    {
        private List<Condition> _conditions;
        private Action _trueAction;
        private Action _falseAction;

        public bool Value // Debating whether I should name this "Value"...
        {
            get // Return true if all Conditions are true
            {
                bool result = true;
                foreach (Condition condition in _conditions)
                {
                    result &= condition.Value;
                }
                return result;
            }
        }

        public Mapping(List<Condition> conditions, Action trueAction, Action falseAction)
        {
            _conditions = conditions;
            _trueAction = trueAction;
            _falseAction = falseAction;
        }

        public void Update()
        {
            if (Value)
            {
                _trueAction();
            }
            else
            {
                _falseAction();
            }
        }
    }
}

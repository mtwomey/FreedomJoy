using System;
using System.Collections.Generic;
using System.Data;

namespace FreedomJoy
{
    class Mapping : IMapping
    {
        private List<ICondition> _conditions;
        private Action _trueAction;
        private Action _falseAction;

        public bool State // Debating whether I should name this "State"...
        {
            get // Return true if all Conditions are true
            {
                bool result = true;
                foreach (Condition condition in _conditions)
                {
                    result &= condition.State;
                }
                return result;
            }
        }

        public Mapping(List<ICondition> conditions, Action trueAction, Action falseAction)
        {
            _conditions = conditions;
            _trueAction = trueAction;
            _falseAction = falseAction;
        }

        public void Update()
        {
            if (State)
            {
                _trueAction();
            }
            else
            {
                _falseAction();
            }
        }

        public void Add(ICondition condition)
        {
            throw new NotImplementedException();
        }
    }
}

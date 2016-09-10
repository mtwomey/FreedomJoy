using System.Collections.Generic;

namespace FreedomJoy
{
    class Mapping
    {
        public List<Condition> Conditions { get; }= new List<Condition>();

        public bool Value // Debating whether I should name this "Value"...
        {
            get // Return true if all Conditions are true
            {
                bool result = true;
                foreach (Condition condition in Conditions)
                {
                    result &= condition.Value;
                }
                return result;
            }
        }
    }
}

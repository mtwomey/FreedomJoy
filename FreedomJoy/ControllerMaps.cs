using System.Collections.Generic;

namespace FreedomJoy
{
    class ControllerMaps
    {
        private readonly List<Mapping> _mappings = new List<Mapping>();

        public void Add(Mapping mapping)
        {
            _mappings.Add(mapping);
        }

        public void Update()
        {
            foreach (Mapping mapping in _mappings)
            {
                mapping.Update();
            }
        }
    }
}

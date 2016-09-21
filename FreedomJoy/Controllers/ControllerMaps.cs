using System.Collections.Generic;

namespace FreedomJoy.Controllers
{
    class ControllerMaps
    {
        private readonly List<IMapping> _mappings = new List<IMapping>();

        public void Add(IMapping mapping)
        {
            _mappings.Add(mapping);
        }

        public void Update()
        {
            foreach (IMapping mapping in _mappings)
            {
                mapping.Update();
            }
        }
    }
}

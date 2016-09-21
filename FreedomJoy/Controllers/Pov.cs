using System.Collections.Generic;

namespace FreedomJoy.Controllers
{
    public class Pov
    {
        public enum PovType
        {
            Standard,
            Button4,
            Button8
        }

        private PovType _type;
        private readonly Controller _parentController;
        private readonly List<Button> _buttonRefs = new List<Button>();
        public int PovNumber { get; set; }
        public int Value
        {
            get
            {
                return _parentController.JoystickState.GetPointOfViewControllers()[PovNumber - 1];
            }
        }

        public Pov(Controller parentController, PovType type, int povNumber)
        {
            _parentController = parentController;
            _type = type;
            PovNumber = povNumber;
        }

        public void SetType(PovType newPovType)
        {
            if (newPovType == PovType.Button4 || newPovType == PovType.Button8)
            {
                int numNewButtons = 0;
                if (newPovType == PovType.Button4) { numNewButtons = 4; }
                if (newPovType == PovType.Button8) { numNewButtons = 8; }

                // Remove any old PovButtons from controller and the refs
                foreach (StandardButton button in _buttonRefs)
                {
                    _parentController.Buttons.Remove(button);
                }
                _buttonRefs.Clear();

                // Add new "virtual" pov buttons
                for (int i = 0; i < numNewButtons; i++)
                {
                    PovButton newButton = new PovButton
                    (
                        parentController: _parentController,
                        buttonNumber: _parentController.Buttons.Count + 1,
                        onValue: (36000 / numNewButtons) * i,
                        povParent: this
                    );
                    _parentController.Buttons.Add(newButton);
                    _buttonRefs.Add(newButton); // Keep a ref so I can remove it later
                }
            }
        }
    }
}

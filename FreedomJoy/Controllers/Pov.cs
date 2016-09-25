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
        private readonly string _name;
        public int PovNumber { get; set; }
        public int Value
        {
            get
            {
                return _parentController.JoystickState.GetPointOfViewControllers()[PovNumber - 1];
            }
        }

        public Pov(Controller parentController, PovType type, string name, int povNumber)
        {
            _parentController = parentController;
            _type = type;
            _name = name;
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
                    PovButton newPovButton = new PovButton
                    (
                        parentController: _parentController,
                        name: "pov" + (i+1),
                        buttonNumber: _parentController.Buttons.Count + 1,
                        onValue: (36000 / numNewButtons) * i,
                        povParent: this
                    );
                    _parentController.Buttons.Add(newPovButton);
                    string povName = "pov" + PovNumber;
                    switch ((36000 / numNewButtons) *i) // There must be a better way...
                    {
                        case 0:
                            povName += "up";
                            break;
                        case 9000:
                            povName += "right";
                            break;
                        case 18000:
                            povName += "down";
                            break;
                        case 27000:
                            povName += "left";
                            break;
                    }

                    _parentController.ButtonsByName.Add(povName, newPovButton);
                    _buttonRefs.Add(newPovButton); // Keep a ref so I can remove it later - I don't think I need / want this anymore..
                }
            }
        }
    }
}

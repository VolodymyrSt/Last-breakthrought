using LastBreakthrought.Logic.CraftingMachine;
using UnityEngine;

namespace LastBreakthrought.UI.Windows.CraftMachineWindow
{
    public class CraftMachineWindowHandler : WindowHandler<CraftMachineWindowView>
    {
        [field:SerializeField] public CraftMachine CraftMachine {  get; private set; }

        public override void ActivateWindow() => View.ShowView();

        public override void DeactivateWindow() => View.HideView();
    }
}

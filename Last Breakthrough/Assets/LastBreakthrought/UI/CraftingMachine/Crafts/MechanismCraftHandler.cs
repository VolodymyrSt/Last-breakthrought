using LastBreakthrought.Logic.Mechanisms;
using LastBreakthrought.Logic.ShipDetail;
using System;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.UI.CraftingMachine.Crafts
{
    public class MechanismCraftHandler : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private MechanismCraftView _view;

        private ShipDetailsGeneratorUI _shipDetailsGeneratorUI;

        [Inject]
        private void Construct(ShipDetailsGeneratorUI shipDetailsGeneratorUI) =>
            _shipDetailsGeneratorUI = shipDetailsGeneratorUI;

        public void Init(Action craftAction, MechanismSO mechanism)
        {
            _view.Init(mechanism.Sprite, craftAction);

            var requiredDetails = mechanism.RequireDetails.GetRequiredShipDetails();
            _shipDetailsGeneratorUI.GenerateRequireDetails(requiredDetails, _view.GetContainer());
        }
    }
}

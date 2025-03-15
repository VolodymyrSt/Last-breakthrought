using LastBreakthrought.CrashedShip;
using LastBreakthrought.Player;
using Zenject;
using UnityEngine;

namespace LastBreakthrought.UI.SlotItem.DetailDetector
{
    public class DetailDetectorItem : Item
    {
        [SerializeField] private DetailDetectorItemView _detailDetectorItemView;

        private PlayerHandler _player;
        private CrashedShipsContainer _shipsContainer;

        [Inject]
        private void Construct(PlayerHandler player, CrashedShipsContainer shipsContainer)
        {
            _player = player;
            _shipsContainer = shipsContainer;
        }

        private void OnEnable() => UnSelect();

        private void Update()
        {
            if (IsItemSelected)
                UpdateDistance();
        }

        private void UpdateDistance()
        {
            var closestCrashedShipPosition = _shipsContainer.GetClosestCrashedShipPosition(_player.transform.position);
            var distance = (int)(_player.transform.position - closestCrashedShipPosition).magnitude;

            _detailDetectorItemView.SetDistanceUI(distance);
        }

        public override void Select()
        {
            _player.ShowDetectorItem();
            _player.SetMovingAnimation(true);

            IsItemSelected = true;
            _detailDetectorItemView.Show();
        }

        public override void UnSelect()
        {
            _player.HideDetectorItem();
            _player.SetMovingAnimation(false);

            IsItemSelected = false;
            _detailDetectorItemView.Hide();
        }
    }
}
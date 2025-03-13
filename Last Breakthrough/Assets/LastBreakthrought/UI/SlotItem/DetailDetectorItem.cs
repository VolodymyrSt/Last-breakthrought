using LastBreakthrought.Player;
using Zenject;

namespace LastBreakthrought.UI.SlotItem
{
    public class DetailDetectorItem : Item
    {
        private PlayerHandler _player;

        [Inject]
        private void Construct(PlayerHandler player)
        {
            _player = player;
        }

        public override void Select()
        {
            _player.ShowDetectorItem();
            _player.SetMovingAnimation(true);
        }

        public override void UnSelect()
        {
            _player.HideDetectorItem();
            _player.SetMovingAnimation(false);
        }
    }
}
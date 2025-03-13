using UnityEngine;

namespace LastBreakthrought.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        private static readonly int MoveHash = Animator.StringToHash("Moving");
        private static readonly int MoveWithItemHash = Animator.StringToHash("MowingWithItem");
        private static readonly int IsUsingItemHash = Animator.StringToHash("IsUsingItem");

        private bool _isUsingItem = false;

        private void Update()
        {
            if (!_isUsingItem)
                _animator.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
            else
                _animator.SetFloat(MoveWithItemHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        public void SetMoving(bool withItem)
        {
            _isUsingItem = withItem;
            _animator.SetBool(IsUsingItemHash, withItem);
        }
    }
}


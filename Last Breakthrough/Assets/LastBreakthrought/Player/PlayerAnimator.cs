using System;
using UnityEngine;

namespace LastBreakthrought.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        private static readonly int MoveHash = Animator.StringToHash("Moving");


        private void Update()
        {
            _animator.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}


using LastBreakthrought.Player;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Logic.Camera
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _distance;
        [SerializeField] private float _offSetY;

        private Transform _followTarget;
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(PlayerMovement playerMovement) => 
            _playerMovement = playerMovement;

        private void Awake() => 
            _followTarget = _playerMovement.transform;

        private void LateUpdate() => PerformFollow();

        private void PerformFollow()
        {
            var newRotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            var newPosition = newRotation * new Vector3(0, 0, _distance * (-1)) + GetFollowingPointPosition();

            transform.position = newPosition;
            transform.rotation = newRotation;
        }

        private Vector3 GetFollowingPointPosition()
        {
            Vector3 followingPosition = _followTarget.position;
            followingPosition.y += _offSetY;
            return followingPosition;
        }
    }
}

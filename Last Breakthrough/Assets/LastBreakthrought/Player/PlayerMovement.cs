using LastBreakthrought.Infrustructure.Services.ConfigProvider;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Infrustructure.Services.Input;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private float _moveSpeed;
        private float _rotationSpeed;
        private float _gravityMultiplier;
        private float _acceleration;
        private float _deceleration;

        private IInputService _inputService;
        private IEventBus _eventBus;
        private Camera _camera;

        private Vector3 _currentHorizontalVelocity;
        private Vector3 _smoothVelocityRef;
        private float _verticalVelocity;

        [Inject]
        private void Construct(IInputService inputService, IConfigProviderService configProvider, IEventBus eventBus)
        {
            //turn off because we want out player to move when the game started (when the turorial is ended)
            enabled = false;

            _inputService = inputService;
            _eventBus = eventBus;

            _moveSpeed = configProvider.PlayerConfigSO.MoveSpeed;
            _rotationSpeed = configProvider.PlayerConfigSO.RotationSpeed;
            _gravityMultiplier = configProvider.PlayerConfigSO.GravityMultiplier;
            _acceleration = configProvider.PlayerConfigSO.Acceleration;
            _deceleration = configProvider.PlayerConfigSO.Deceleration;

            _eventBus.SubscribeEvent((OnTutorialEndedSignal signal) => enabled = true);
        }

        private void Start() => _camera = Camera.main;

        private void Update()
        {
            Vector3 inputDirection = GetNormalizedMovementVector();

            ApplyGravity();
            UpdateHorizontalMovement(inputDirection);
            RotateTowardsMovement(inputDirection);
            MoveCharacter();
        }

        private Vector3 GetNormalizedMovementVector()
        {
            if (_inputService.Axis.sqrMagnitude <= Mathf.Epsilon)
                return Vector3.zero;

            Vector3 newVector = _inputService.Axis.x * _camera.transform.right + 
                _inputService.Axis.y * _camera.transform.forward;

            newVector.y = 0;
            newVector.Normalize();

            return newVector;
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded)
                _verticalVelocity = -0.5f;
            else
                _verticalVelocity += Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
        }

        private void UpdateHorizontalMovement(Vector3 targetDirection)
        {
            Vector3 targetVelocity = targetDirection * _moveSpeed;
            float smoothRate = targetDirection.magnitude > 0 ? _acceleration : _deceleration;

            _currentHorizontalVelocity = Vector3.SmoothDamp(_currentHorizontalVelocity, targetVelocity, ref _smoothVelocityRef,
                1f / smoothRate);
        }

        private void RotateTowardsMovement(Vector3 movementDirection)
        {
            if (movementDirection.magnitude <= 0.1f) return;

            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void MoveCharacter()
        {
            Vector3 movement = _currentHorizontalVelocity + Vector3.up * _verticalVelocity;
            _characterController.Move(movement * Time.deltaTime);
        }

        private void OnDestroy() => 
            _eventBus.UnSubscribeEvent((OnTutorialEndedSignal signal) => enabled = true);
    }
}

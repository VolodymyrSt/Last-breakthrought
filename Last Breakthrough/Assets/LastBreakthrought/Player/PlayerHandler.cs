using System;
using UnityEngine;

namespace LastBreakthrought.Player
{
    public class PlayerHandler : MonoBehaviour, IDamagable
    {
        public event Action<float> OnPlayerBeenAttacked;

        [SerializeField] private GameObject _detectorItemPrefab;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private void OnEnable() => HideDetectorItem();

        public void ApplyDamage(float damage) => 
            OnPlayerBeenAttacked.Invoke(damage);

        public void SetMovingAnimation(bool withItem) =>
            _playerAnimator.SetMoving(withItem);

        public void ShowDetectorItem() =>
            _detectorItemPrefab.SetActive(true);

        public void HideDetectorItem() =>
            _detectorItemPrefab.SetActive(false);
    }
}


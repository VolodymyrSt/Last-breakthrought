using System;
using UnityEngine;

namespace LastBreakthrought.Player
{
    public class PlayerHandler : MonoBehaviour, IDamagable, IEnemyTarget
    {
        public event Action<float> OnPlayerBeenAttacked;

        [SerializeField] private GameObject _wreckageDetectorItemPrefab;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private void OnEnable() => HideDetectorItem();

        public Vector3 GetPosition() => transform.position;

        public void ApplyDamage(float damage) => 
            OnPlayerBeenAttacked.Invoke(damage);

        public void SetMovingAnimation(bool withItem) =>
            _playerAnimator.SetMoving(withItem);

        public void ShowDetectorItem() =>
            _wreckageDetectorItemPrefab.SetActive(true);

        public void HideDetectorItem() =>
            _wreckageDetectorItemPrefab.SetActive(false);
    }
}


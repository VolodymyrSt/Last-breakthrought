using DG.Tweening;
using System;
using UnityEngine;

namespace LastBreakthrought.Logic.InteractionZone
{
    [RequireComponent(typeof(SphereCollider))]
    public class InterationZoneView : MonoBehaviour
    {
        public event Action OnPlayerEnter;
        public event Action OnPlayerExit;

        [SerializeField] private Vector3 _currentScale;

        private void OnValidate() => 
           _currentScale = transform.localScale;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                OnPlayerEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                OnPlayerExit?.Invoke();
        }

        public void Show()
        {
            var duration = 10f;
            gameObject.SetActive(true);
            transform.DOScale(_currentScale, duration * Time.deltaTime)
                .SetEase(Ease.Linear)
                .Play();
        }
        
        public void Hide()
        {
            var duration = 10f;
            transform.DOScale(0, duration * Time.deltaTime)
                .SetEase(Ease.Linear)
                .Play()
                .OnComplete(() => gameObject.SetActive(false));
        }

        public void HideOnInit()
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}

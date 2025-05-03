using DG.Tweening;
using LastBreakthrought.Infrustructure.Services.AudioService;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Logic.Camera;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace LastBreakthrought.Other
{
    public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float DURATIOIN = 0.2f;
        private const float UNSCALED_VALUE = 1f;

        [SerializeField] private Ease _ease;
        [SerializeField] private float _scaleValue = 1.1f;

        private IAudioService _audioService;
        private FollowCamera _followCamera;

        private bool _isSoundRestored = true;

        [Inject]
        private void Construct(IAudioService audioService, FollowCamera followCamera)
        {
            _audioService = audioService;
            _followCamera = followCamera;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlaySelectedSound();

            transform.DOScale(_scaleValue, DURATIOIN)
                .SetEase(_ease)
                .Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            transform.DOScale(UNSCALED_VALUE, DURATIOIN)
                .SetEase(_ease)
                .Play();
        }

        private void PlaySelectedSound()
        {
            if (_isSoundRestored)
            {
                _isSoundRestored = false;
                _audioService.PlayOnObject(Configs.Sound.SoundType.Selected, _followCamera, false, 0.2f);
                StartCoroutine(RestorSound());
            }
        }

        private IEnumerator RestorSound()
        {
            yield return new WaitForSeconds(0.5f);
            _isSoundRestored = true;
        }
    }
}

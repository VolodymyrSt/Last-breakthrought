using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Logic.Camera;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace LastBreakthrought.Logic.Video
{
    public class VideoPlayerHandler : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _beginningVideo;
        [SerializeField] private VideoPlayer _startExplodingVideo;
        [SerializeField] private VideoPlayer _victoryVideo;

        private UnityEngine.Camera _camera;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(UnityEngine.Camera camera, IEventBus eventBus)
        {
            _camera = camera;
            _eventBus = eventBus;
        }

        private void Awake() =>
            PlayBeginningVideo(() => _eventBus.Invoke(new OnBeginningVideoEndedSignal()));

        private void Start()
        {
            _eventBus.SubscribeEvent((OnGameEndedSignal signal) =>
                PlayStarExplodingVideo(() => _eventBus.Invoke(new OnExploededStarVideoEndedSignal())));

            _eventBus.SubscribeEvent((OnGameWonSignal signal) =>
                PlayVictoryVideo(() => _eventBus.Invoke(new OnVictoryVideoEndedSignal())));
        }

        public void PlayBeginningVideo(Action onEnded) => StartCoroutine(Play(_beginningVideo, onEnded));

        private void PlayStarExplodingVideo(Action onEnded) => StartCoroutine(Play(_startExplodingVideo, onEnded, false));

        private void PlayVictoryVideo(Action onEnded) => StartCoroutine(Play(_victoryVideo, onEnded, false));

        private IEnumerator Play(VideoPlayer video, Action onEnded, bool needToHideAtTheEnd = true)
        {
            _eventBus.Invoke(new OnVideoPlayedSignal());
            video.gameObject.SetActive(true);
            video.targetCamera = _camera;
            video.Play();
            yield return new WaitForSeconds((float)video.clip.length);
            video.gameObject.SetActive(!needToHideAtTheEnd);
            onEnded?.Invoke();
        }

        private void OnDestroy()
        {
            _eventBus?.UnSubscribeEvent((OnGameEndedSignal signal) =>
                PlayStarExplodingVideo(() => _eventBus.Invoke(new OnExploededStarVideoEndedSignal())));

            _eventBus?.UnSubscribeEvent((OnGameWonSignal signal) =>
                PlayVictoryVideo(() => _eventBus.Invoke(new OnVictoryVideoEndedSignal())));
        }
    }
}

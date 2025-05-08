using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Util;
using System.Collections;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Other
{
    public class DayLightHandler : IInitializable
    {
        private const float TIME_MULTIPLAYER = 0.003f;
        private const float MAX_LIGHT_INTENSITY = 1f;
        private const float MIN_LIGHT_INTENSITY = 0f;
        private const float WAITING_TIME = 120f;

        private readonly Light _light;
        private readonly IEventBus _eventBus;
        private readonly ICoroutineRunner _coroutineRunner;
        private bool _isDay;

        private bool _isGamePaused = false;

        public DayLightHandler(Light light, IEventBus eventBus, ICoroutineRunner coroutineRunner)
        {
            _light = light;
            _eventBus = eventBus;
            _coroutineRunner = coroutineRunner;
        }

        ~DayLightHandler() 
        {
            _eventBus?.UnSubscribeEvent((OnGamePausedSignal signal) => _isGamePaused = true);
            _eventBus?.UnSubscribeEvent((OnGameResumedSignal signal) => _isGamePaused = false);
        }

        public void Initialize()
        {
            _light.intensity = MAX_LIGHT_INTENSITY;
            _isDay = true;

            _eventBus.SubscribeEvent((OnGamePausedSignal signal) => _isGamePaused = true);
            _eventBus.SubscribeEvent((OnGameResumedSignal signal) => _isGamePaused = false);

            _coroutineRunner.PerformCoroutine(StartUpdatingLight());
        }

        private IEnumerator StartUpdatingLight()
        {
            while (true)
            {
                if (_isGamePaused) yield return null;

                if (_isDay)
                {
                    _light.intensity -= Time.deltaTime * TIME_MULTIPLAYER;
                    if (_light.intensity <= MIN_LIGHT_INTENSITY)
                    {
                        _light.intensity = MIN_LIGHT_INTENSITY;
                        _isDay = false;
                        yield return new WaitForSeconds(WAITING_TIME);
                    }
                    yield return null;
                }
                else
                {
                    _light.intensity += Time.deltaTime * TIME_MULTIPLAYER;
                    if (_light.intensity >= MAX_LIGHT_INTENSITY)
                    {
                        _light.intensity = MAX_LIGHT_INTENSITY;
                        _isDay = true;
                        yield return new WaitForSeconds(WAITING_TIME);
                    }
                    yield return null;
                }
            }
        }
    }
}

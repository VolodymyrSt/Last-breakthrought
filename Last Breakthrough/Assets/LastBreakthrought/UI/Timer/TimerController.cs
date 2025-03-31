using LastBreakthrought.Util;
using System.Collections;
using Zenject;
using UnityEngine;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using LastBreakthrought.Infrustructure.Services.ConfigProvider;

namespace LastBreakthrought.UI.Timer
{
    public class TimerController : IInitializable
    {
        private readonly TimerView _timerView;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IEventBus _eventBus;
        private readonly Light _light;

        private bool _isTimeRunUp;
        private int _currentDays;
        private int _currentMinutes;
        private int _currentSeconds;

        private float _maxLightIntensity = 1f;
        private float _minLightIntensity = 0f;
        private bool _isDay = true;

        public TimerController(TimerView timerView, ICoroutineRunner coroutineRunner, IEventBus eventBus, IConfigProviderService configProvider, Light light)
        {
            _timerView = timerView;
            _coroutineRunner = coroutineRunner;
            _eventBus = eventBus;
            _light = light;

            _currentDays = configProvider.GameConfigSO.StartedDay;
            _currentMinutes = configProvider.GameConfigSO.StartedMinute;
            _currentSeconds = configProvider.GameConfigSO.StartedSecond;

            _timerView.UpdateDay(_currentDays);
            _timerView.UpdateClock(_currentMinutes, _currentSeconds);

            _light.intensity = _maxLightIntensity;
        }

        public void Initialize()
        {
            _isTimeRunUp = false;
            _coroutineRunner.PerformCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(1);

                _currentSeconds++;

                if (_currentSeconds == 60)
                {
                    _currentSeconds = 0;
                    ChangeTime();

                }
                UpdateLight();

                _timerView.UpdateClock(_currentMinutes, _currentSeconds);

                if (_isTimeRunUp) yield break;
            }
        }

        private void UpdateLight()
        {
            if (_isDay)
            {
                _light.intensity -= Time.deltaTime;
                if (_light.intensity == _minLightIntensity)
                    _isDay = false;
            }
            else
            {
                _light.intensity += Time.deltaTime;
                if (_light.intensity == _maxLightIntensity)
                    _isDay = true;
            }      
        }

        private void ChangeTime()
        {
            _currentMinutes++;

            if (_currentMinutes == 24)
            {
                _currentMinutes = 0;

                ChangeDay();
            }
        }

        private void ChangeDay()
        {
            _currentDays++;

            if (_currentDays == 5)
            {
                _isTimeRunUp = true;
                _eventBus.Invoke(new OnGameEndedSignal());
            }

            _timerView.UpdateDay(_currentDays);
        }
    }
}

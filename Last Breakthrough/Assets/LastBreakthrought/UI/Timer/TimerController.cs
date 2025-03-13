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

        private int _currentDays;
        private int _currentMinutes;
        private int _currentSeconds;

        private bool _isTimeRunUp;

        public TimerController(TimerView timerView, ICoroutineRunner coroutineRunner, IEventBus eventBus, IConfigProviderService configProvider)
        {
            _timerView = timerView;
            _coroutineRunner = coroutineRunner;
            _eventBus = eventBus;

            _currentDays = configProvider.GameConfigSO.StartedDay;
            _currentMinutes = configProvider.GameConfigSO.StartedMinute;
            _currentSeconds = configProvider.GameConfigSO.StartedSecond;

            _timerView.UpdateDay(_currentDays);
            _timerView.UpdateClock(_currentMinutes, _currentSeconds);
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

                _timerView.UpdateClock(_currentMinutes, _currentSeconds);

                if (_isTimeRunUp) yield break;
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

using UnityEngine;
using UnityEngine.UI;

namespace LastBreakthrought.UI.PlayerStats
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _oxygenSlider;

        public void SetHealthSliderValue(float value) => _healthSlider.value = value;
        public void SetOxygeSliderValue(float value) => _oxygenSlider.value = value;

        public void SetHealthSliderMaxValueAndStartedValue(float maxValue, float startedValue)
        {
            _healthSlider.maxValue = maxValue;
            _healthSlider.value = startedValue;
        }

        public void SetOxygeSliderMaxValueAndStartedValue(float maxValue, float startedValue)
        {
            _oxygenSlider.maxValue = maxValue;
            _healthSlider.value = startedValue;
        }
    }
}


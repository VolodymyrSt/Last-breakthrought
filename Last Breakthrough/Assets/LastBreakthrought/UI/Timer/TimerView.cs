using LastBreakthrought.UI.ToolTip;
using TMPro;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.UI.Timer
{
    public class TimerView : /*ToolTipTrigger*/MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dayCounterText;
        [SerializeField] private TextMeshProUGUI _clockText;

        private ToolTipHandler _toolTipHandler;

        //[Inject]
        //private void Construct(ToolTipHandler handler) =>
        //    _toolTipHandler = handler;

        //private void OnEnable()
        //{
        //    ConfigureToolTip("Calendar", "On the fifth day, the closest sky will explode", ToolTipPosition.BottomLeft);
        //}

        public void UpdateDay(int daysPassed) =>
            _dayCounterText.text = $"{daysPassed.ToString()} Day";

        public void UpdateClock(int minutes, int seconds)
        {
            string minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
            string secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();
            _clockText.text = $"{minutesString} : {secondsString}";
        }
    }
}

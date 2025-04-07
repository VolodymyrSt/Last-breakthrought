using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.LastBreakthrought.UI.Massage
{
    public class MassageHandler
    {
        private MassageView _massageView;

        [Inject]
        private void Construct(MassageView massageView) =>
            _massageView = massageView;

        public void ShowMassage(string massage) => 
            _massageView.Show(massage);
    }
}

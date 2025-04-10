using Zenject;

namespace LastBreakthrought.Infrustructure.Services.Massage
{
    public class MassageHandlerService : IMassageHandlerService
    {
        private MassageView _massageView;

        [Inject]
        private void Construct(MassageView massageView) =>
            _massageView = massageView;

        public void ShowMassage(string massage) =>
            _massageView.Show(massage);
    }
}

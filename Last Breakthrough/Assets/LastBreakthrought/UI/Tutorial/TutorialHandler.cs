using LastBreakthrought.Infrustructure.Services.ConfigProvider;
using LastBreakthrought.Infrustructure.Services.EventBus;
using LastBreakthrought.Infrustructure.Services.EventBus.Signals;
using System;
using Zenject;

namespace LastBreakthrought.UI.Tutorial
{
    public class TutorialHandler : IInitializable, IDisposable
    {
        private readonly TutorialView _view;
        private readonly IConfigProviderService _configProviderService;
        private readonly IEventBus _eventBus;

        public TutorialHandler(TutorialView view, IConfigProviderService configProviderService, IEventBus eventBus)
        {
            _view = view;
            _configProviderService = configProviderService;
            _eventBus = eventBus;

            _view.OnDialogueEnded += EndTutorial;
        }

        public void Initialize()
        {
            var dialogueData = _configProviderService.DialogueConfigSO;
            _view.Init(dialogueData.DialogueLines, dialogueData.LineWaitTime, _eventBus);

            _view.RunCurrentDialogueLine();
        }

        private void EndTutorial() => _eventBus.Invoke(new OnTutorialEndedSignal());

        public void Dispose() => _view.OnDialogueEnded -= EndTutorial;
    }
}

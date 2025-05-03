using LastBreakthrought.Other;
using UnityEngine;
using Zenject;

namespace LastBreakthrought.Infrustructure.Installers
{
    public class MenuInstaller : MonoInstaller 
    {
        [SerializeField] private SoundHolder _soundHolder;

        public override void InstallBindings() => BindSoundHolder();

        private void BindSoundHolder() =>
            Container.Bind<SoundHolder>().FromInstance(_soundHolder).AsSingle();
    }
}


using Windows;
using Windows.CharacterInfoWindow;
using Windows.CharacterInfoWindow.Views;
using Windows.Common;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CharacterInfoWindowModel characterInfoWindowModel;
        [SerializeField] private WindowOpenManager windowOpenManager;
        [SerializeField] private CharacterInfoWindowView characterInfoWindowView;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterInfoWindowView>().FromInstance(characterInfoWindowView);
            Container.Bind<CharacterInfoWindowModel>().FromInstance(characterInfoWindowModel);
            Container.Bind<IWindowAdapter>().To<CharacterInfoWindowAdapter>().AsSingle();
            Container.Bind<WindowOpenManager>().FromInstance(windowOpenManager);
        }
    }
}
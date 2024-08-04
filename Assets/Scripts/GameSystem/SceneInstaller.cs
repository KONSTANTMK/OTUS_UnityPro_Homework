using ShootEmUp.Bullets;
using Zenject;
using CharacterComponent = ShootEmUp.Character.Character;

namespace ShootEmUp.GameSystem
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().FromComponentsInHierarchy().AsSingle();
        }
    }
}
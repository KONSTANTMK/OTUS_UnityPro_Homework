using ShootEmUp.Bullets;
using ShootEmUp.Enemy;
using Zenject;
using CharacterComponent = ShootEmUp.Character.Character;

namespace ShootEmUp.GameSystem
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.Bind<InputManager>().AsSingle();
            
            Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyDestroyer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemySpawnObserver>().FromNew().AsSingle();
        }
    }
}
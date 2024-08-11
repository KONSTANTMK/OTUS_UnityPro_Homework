using ShootEmUp.Bullets;
using ShootEmUp.Components;
using ShootEmUp.Enemy;
using ShootEmUp.Level;
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

            Container.Bind<CharacterComponent>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EnemySpawner>().FromNew().AsSingle();
            Container.Bind<EnemyDestroyer>().FromNew().AsSingle();
            Container.Bind<EnemySpawnObserver>().FromNew().AsSingle();
            Container.Bind<EnemyShootObserver>().FromNew().AsSingle();
            Container.Bind<EnemyPool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<BulletSpawner>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BulletPool>().FromComponentInHierarchy().AsSingle();

            Container.Bind<WorldTransform>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();

        }
    }
}
using ShootEmUp.Bullets;
using ShootEmUp.Character;
using ShootEmUp.Common;
using ShootEmUp.Components;
using ShootEmUp.Enemy;
using ShootEmUp.Level;
using Zenject;

namespace ShootEmUp.GameSystem
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<DiContainer>().AsCached();
            
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.Bind<InputManager>().AsSingle();

            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<EnemySpawner>().FromNew().AsSingle();
            Container.Bind<EnemyDestroyer>().FromNew().AsSingle();
            Container.Bind<EnemySpawnObserver>().FromNew().AsSingle();
            Container.Bind<EnemyShootObserver>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyPool>().FromNew().AsSingle();
            Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<BulletSpawner>().FromNew().AsSingle();
            Container.Bind<BulletDestroyer>().FromNew().AsSingle();
            Container.Bind<BulletSpawnObserver>().FromNew().AsSingle();
            Container.Bind<BulletInBoundsObserver>().FromNew().AsSingle();
            Container.Bind<BulletInBoundsCheacker>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletPool>().FromNew().AsSingle();
            
            Container.Bind<WorldTransform>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();
        }
    }
}
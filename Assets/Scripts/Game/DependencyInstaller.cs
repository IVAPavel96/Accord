using Zenject;

namespace Game
{
    public class DependencyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateManager>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<AudioManager>().FromComponentOn(gameObject).AsSingle();
        }
    }
}
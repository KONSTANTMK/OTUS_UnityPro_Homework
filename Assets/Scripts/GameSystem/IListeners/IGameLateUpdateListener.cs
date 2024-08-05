namespace ShootEmUp.GameSystem.Listeners
{
    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}
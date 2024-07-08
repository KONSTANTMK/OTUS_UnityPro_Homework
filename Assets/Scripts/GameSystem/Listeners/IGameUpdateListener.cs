namespace ShootEmUp.GameSystem.Listeners
{
    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }
}
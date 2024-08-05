namespace ShootEmUp.GameSystem.Listeners
{
    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float deltaTime);
    }
}
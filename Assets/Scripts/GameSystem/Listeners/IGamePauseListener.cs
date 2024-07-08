namespace ShootEmUp.GameSystem.Listeners
{
    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }
}
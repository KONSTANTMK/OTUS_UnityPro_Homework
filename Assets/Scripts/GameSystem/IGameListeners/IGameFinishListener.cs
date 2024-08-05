namespace ShootEmUp.GameSystem.Listeners
{
    public interface IGameFinishListener: IGameListener
    {
        void OnFinishGame();
    }
}
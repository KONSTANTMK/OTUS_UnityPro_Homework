using UniRx;

namespace Windows.CharacterInfoWindow.Presenters
{
    public interface ICharacterStatViewPresenter
    {
        string Name { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
    }
}
using UniRx;

namespace Windows.CharacterInfoWindow.Presenters
{
    public interface ICharacterExperienceViewPresenter
    {
        IReadOnlyReactiveProperty<float> XpGainPart { get; }
        IReadOnlyReactiveProperty<string> Experience { get; }
    }
}
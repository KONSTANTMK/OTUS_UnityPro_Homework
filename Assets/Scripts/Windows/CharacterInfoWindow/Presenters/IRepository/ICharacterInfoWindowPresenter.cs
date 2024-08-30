using UniRx;
using UnityEngine;

namespace Windows.CharacterInfoWindow.Presenters
{
    public interface ICharacterInfoWindowPresenter
    {
        IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters { get; }
        ICharacterExperienceViewPresenter ExperienceViewPresenter { get; }

        IReadOnlyReactiveProperty<string> CharacterName { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }

        ReactiveCommand LevelUpCommand { get; }
    }
}
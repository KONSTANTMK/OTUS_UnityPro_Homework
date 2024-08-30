using PlayerData;
using UniRx;

namespace Windows.CharacterInfoWindow.Presenters
{
    public interface ICharacterStatViewPresenter
    {
        string Name { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
    }
    
    public class CharacterStatViewPresenter : ICharacterStatViewPresenter
    {
        private readonly CharacterStat _stat;
        private readonly StringReactiveProperty _level;

        public CharacterStatViewPresenter(CharacterStat stat)
        {
            _stat = stat;

            Name = _stat.Name;
            _level = new StringReactiveProperty(_stat.Value.ToString());

            _stat.OnValueChanged += OnStatValueChanged;
        }

        ~CharacterStatViewPresenter()
        {
            _stat.OnValueChanged -= OnStatValueChanged;
        }

        public string Name { get; }
        public IReadOnlyReactiveProperty<string> Level => _level;

        private void OnStatValueChanged(int newValue)
            => _level.Value = newValue.ToString();
    }
}
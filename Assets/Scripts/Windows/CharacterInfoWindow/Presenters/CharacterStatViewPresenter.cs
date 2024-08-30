using PlayerData;
using UniRx;

namespace Windows.CharacterInfoWindow.Presenters
{
    public class CharacterStatViewPresenter : ICharacterStatViewPresenter
    {
        private readonly CharacterStat stat;
        private readonly StringReactiveProperty level;

        public CharacterStatViewPresenter(CharacterStat stat)
        {
            this.stat = stat;

            Name = this.stat.Name;
            level = new StringReactiveProperty(this.stat.Value.ToString());

            this.stat.OnValueChanged += OnStatValueChanged;
        }

        ~CharacterStatViewPresenter()
        {
            stat.OnValueChanged -= OnStatValueChanged;
        }

        public string Name { get; }
        public IReadOnlyReactiveProperty<string> Level => level;

        private void OnStatValueChanged(int newValue)
            => level.Value = newValue.ToString();
    }
}
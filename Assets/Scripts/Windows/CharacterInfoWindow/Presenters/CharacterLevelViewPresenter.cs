using PlayerData;
using UniRx;

namespace Windows.CharacterInfoWindow.Presenters
{
    public class CharacterExperienceViewPresenter : ICharacterExperienceViewPresenter
    {
        private readonly PlayerLevel playerLevel;
        private readonly FloatReactiveProperty xpGainPart;
        private readonly StringReactiveProperty currentXpValue;

        public CharacterExperienceViewPresenter(PlayerLevel playerLevel)
        {
            this.playerLevel = playerLevel;

            xpGainPart = 
                new FloatReactiveProperty(this.playerLevel.CurrentExperience / (float)this.playerLevel.RequiredExperience);
            currentXpValue =
                new StringReactiveProperty($"{this.playerLevel.CurrentExperience}/{this.playerLevel.RequiredExperience} XP");

            this.playerLevel.OnExperienceChanged += OnChangedExperience;
        }

        ~CharacterExperienceViewPresenter()
        {
            playerLevel.OnExperienceChanged -= OnChangedExperience;
        }

        public IReadOnlyReactiveProperty<float> XpGainPart => xpGainPart;
        public IReadOnlyReactiveProperty<string> Experience => currentXpValue;

        private void OnChangedExperience(int _)
        {
            xpGainPart.Value = playerLevel.CurrentExperience / (float)playerLevel.RequiredExperience;
            currentXpValue.Value = $"{playerLevel.CurrentExperience}/{playerLevel.RequiredExperience}";
        }
    }
}
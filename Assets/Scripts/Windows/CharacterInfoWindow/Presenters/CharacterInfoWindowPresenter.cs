using System.Collections.Generic;
using PlayerData;
using UniRx;
using UnityEngine;
using CharacterInfo = PlayerData.CharacterInfo;

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
    
    public class CharacterInfoWindowPresenter : ICharacterInfoWindowPresenter
    {
        private readonly UserInfo userInfo;
        private readonly CharacterInfo characterInfo;
        private readonly PlayerLevel playerLevel;

        private readonly ICharacterExperienceViewPresenter experienceViewPresenter;
        
        private readonly ReactiveCollection<ICharacterStatViewPresenter> statsPresenters = new();
        private readonly Dictionary<CharacterStat, ICharacterStatViewPresenter> statPresenterConnections = new();

        private readonly StringReactiveProperty characterName;
        private readonly StringReactiveProperty description;
        private readonly StringReactiveProperty level;
        private readonly ReactiveProperty<Sprite> icon;
        private readonly BoolReactiveProperty canLevelUp;
        private readonly ReactiveCommand levelUpCommand;

        private readonly CompositeDisposable subscriptions = new();

        public IReadOnlyReactiveCollection<ICharacterStatViewPresenter> StatViewPresenters => statsPresenters;
        public ICharacterExperienceViewPresenter ExperienceViewPresenter => experienceViewPresenter;
        public IReadOnlyReactiveProperty<string> CharacterName => characterName;
        public IReadOnlyReactiveProperty<string> Description => description;
        public IReadOnlyReactiveProperty<string> Level => level;
        public IReadOnlyReactiveProperty<Sprite> Icon => icon;
        public IReadOnlyReactiveProperty<bool> CanLevelUp => canLevelUp;
        public ReactiveCommand LevelUpCommand => levelUpCommand;

        public CharacterInfoWindowPresenter(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            this.userInfo = userInfo;
            this.characterInfo = characterInfo;
            this.playerLevel = playerLevel;

            characterName = new StringReactiveProperty(this.userInfo.Name);
            description = new StringReactiveProperty(this.userInfo.Description);
            icon = new ReactiveProperty<Sprite>(this.userInfo.Icon);
            level = new StringReactiveProperty($"Level: {this.playerLevel.CurrentLevel.ToString()}");

            canLevelUp = new BoolReactiveProperty(playerLevel.CanLevelUp());
            levelUpCommand = new ReactiveCommand(canLevelUp);

            levelUpCommand.Subscribe(OnLevelUpCommand).AddTo(subscriptions);

            this.userInfo.OnNameChanged += OnNameChanged;
            this.userInfo.OnDescriptionChanged += OnDescriptionChanged;
            this.userInfo.OnIconChanged += OnIconChanged;
            this.characterInfo.OnStatAdded += AddStatPresenter;
            this.characterInfo.OnStatRemoved += RemoveStatPresenter;
            this.playerLevel.OnLevelUp += OnChangedLevel;
            this.playerLevel.OnExperienceChanged += OnChangedExperience;

            experienceViewPresenter = new DefaultCharacterExperienceViewPresenter(playerLevel);
        }

        ~CharacterInfoWindowPresenter()
        {
            userInfo.OnNameChanged -= OnNameChanged;
            userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            userInfo.OnIconChanged -= OnIconChanged;
            characterInfo.OnStatAdded -= AddStatPresenter;
            characterInfo.OnStatRemoved -= RemoveStatPresenter;
            playerLevel.OnLevelUp -= OnChangedLevel;
            playerLevel.OnExperienceChanged -= OnChangedExperience;

            subscriptions.Dispose();
        }

        private void OnLevelUpCommand(Unit unit)
        {
            playerLevel.LevelUp();
        }

        private void AddStatPresenter(CharacterStat stat)
        {
            var statPresenter = new CharacterStatViewPresenter(stat);
            statsPresenters.Add(statPresenter);
            statPresenterConnections.Add(stat, statPresenter);
        }

        private void RemoveStatPresenter(CharacterStat stat)
        {
            var presenter = statPresenterConnections[stat];
            statsPresenters.Remove(presenter);
            statPresenterConnections.Remove(stat);
        }
        
        private void OnNameChanged(string newName) 
            => characterName.Value = $"@{newName}";
        
        private void OnDescriptionChanged(string newDescription) 
            => description.Value = newDescription;
        
        private void OnIconChanged(Sprite newIcon) 
            => icon.Value = newIcon;
        
        private void OnChangedLevel() 
            => level.Value = $"Level: {playerLevel.CurrentLevel}";

        private void OnChangedExperience(int newValue) 
            => canLevelUp.Value = playerLevel.CanLevelUp();
    }
}
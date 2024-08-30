using System;
using System.Collections.Generic;
using Windows.CharacterInfoWindow.Presenters;
using Windows.Common;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Windows.CharacterInfoWindow.Views
{
    public class CharacterInfoWindowView : ReactiveView
    {
        private readonly List<CharacterStatView> currentStats = new();
        
        [SerializeField] private TMP_Text characterName;
        [SerializeField] private Image avatar;
        [SerializeField] private TMP_Text level;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Transform statsHolder;
        
        
        [Header("Buttons")] 
        [SerializeField] private Button closeButton;
        [SerializeField] private Button levelUpButton;

        [Header("Views")]
        [SerializeField] private CharacterExperienceView experienceView;
        
        [Header("Prefabs")] 
        [SerializeField] private CharacterStatView statViewPrefab;
        
        [Header("Sprites")] 
        [SerializeField] private Sprite LevelUpGreenButtonSprite;
        [SerializeField] private Sprite LevelUpGrayButtonSprite;
        
        private ICharacterInfoWindowPresenter currentPresenter;

        public event Action CloseButtonClicked;

        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        public void Initialize(ICharacterInfoWindowPresenter presenter)
        {
            DisposeSubscriptions();

            currentPresenter = presenter;
            SubscribeToPresenter(currentPresenter);
            
            experienceView.Initialize(presenter.ExperienceViewPresenter);
            UpdateStats(presenter.StatViewPresenters);
        }

        private void UpdateStats(IEnumerable<ICharacterStatViewPresenter> statPresenters)
        {
            for (int i = 0; i < currentStats.Count; i++)
                Destroy(currentStats[i].gameObject);
            
            currentStats.Clear();

            foreach (var presenter in statPresenters)
                AddStatView(presenter);
        }

        private void AddStatView(ICharacterStatViewPresenter presenter)
        {
            var statView = Instantiate(statViewPrefab, statsHolder);
            statView.Initialize(presenter);
            currentStats.Add(statView);
        }

        private void SubscribeToPresenter(ICharacterInfoWindowPresenter presenter)
        {
            presenter.CharacterName.Subscribe(OnNameChanged).AddTo(Subscriptions);
            presenter.Description.Subscribe(OnDescriptionChanged).AddTo(Subscriptions);
            presenter.Icon.Subscribe(OnIconChanged).AddTo(Subscriptions);
            presenter.Level.Subscribe(UpdateLevelValue).AddTo(Subscriptions);
            presenter.CanLevelUp.Subscribe(OnLevelUpConditionChanged).AddTo(Subscriptions);

            presenter.StatViewPresenters.ObserveAdd().Subscribe(OnStatAdded).AddTo(Subscriptions);
            presenter.StatViewPresenters.ObserveRemove().Subscribe(OnStatRemoved).AddTo(Subscriptions);
            
            presenter.LevelUpCommand.BindTo(levelUpButton).AddTo(Subscriptions);
        }
        
        #region COLLECTION_HANDLERS

        private void OnStatRemoved(CollectionRemoveEvent<ICharacterStatViewPresenter> removeEvent) 
            => UpdateStats(currentPresenter.StatViewPresenters);

        private void OnStatAdded(CollectionAddEvent<ICharacterStatViewPresenter> addEvent) 
            => AddStatView(addEvent.Value);

        #endregion

        #region PROPERTIES_HANDLERS

        private void OnNameChanged(string newValue) 
            => characterName.text = newValue;

        private void OnDescriptionChanged(string newValue) 
            => description.text = newValue;

        private void OnIconChanged(Sprite newValue) 
            => avatar.sprite = newValue;
        
        private void UpdateLevelValue(string level) 
            => this.level.text = level;

        private void OnLevelUpConditionChanged(bool canLevelUp)
        {
            if (canLevelUp)
                levelUpButton.image.sprite = LevelUpGreenButtonSprite;
            else
                levelUpButton.image.sprite = LevelUpGrayButtonSprite;
        }

        #endregion

        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke();
        }
    }
}
using Windows.CharacterInfoWindow.Presenters;
using Windows.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Windows.CharacterInfoWindow.Views
{
    public class CharacterExperienceView : ReactiveView
    {
        [SerializeField] private Slider _xpBar;
        [SerializeField] private Image _barForeground;
        [SerializeField] private TMP_Text _xpValue;

        [Header("Sprites")] 
        [SerializeField] private Sprite _notFilledSprite;
        [SerializeField] private Sprite _filledSprite;
        
        public void Initialize(ICharacterExperienceViewPresenter presenter)
        {
            DisposeSubscriptions();
            SubscribeToPresenter(presenter);
        }

        private void SubscribeToPresenter(ICharacterExperienceViewPresenter presenter)
        {
            presenter.XpGainPart.Subscribe(UpdateSliderValue).AddTo(Subscriptions);
            presenter.Experience.Subscribe(UpdateXpValue).AddTo(Subscriptions);
        }

        private void UpdateXpValue(string newValue)
        {
            _xpValue.text = newValue;
        }

        private void UpdateSliderValue(float newValue)
        {
            _xpBar.value = newValue;

            if (newValue < 1)
                _barForeground.sprite = _notFilledSprite;
            else
                _barForeground.sprite = _filledSprite;
        }
    }
}
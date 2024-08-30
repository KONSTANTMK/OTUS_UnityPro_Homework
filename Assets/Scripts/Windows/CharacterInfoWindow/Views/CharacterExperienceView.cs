using Windows.CharacterInfoWindow.Presenters;
using Windows.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Serialization;

namespace Windows.CharacterInfoWindow.Views
{
    public class CharacterExperienceView : ReactiveView
    {
        [SerializeField] private Slider xpBar;
        [SerializeField] private Image barForeground;
        [SerializeField] private TMP_Text xpValue;

        
        [Header("Sprites")] 
        [SerializeField] private Sprite notFilledSprite;
        [SerializeField] private Sprite filledSprite;
        
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
            xpValue.text = newValue;
        }

        private void UpdateSliderValue(float newValue)
        {
            xpBar.value = newValue;

            if (newValue < 1)
                barForeground.sprite = notFilledSprite;
            else
                barForeground.sprite = filledSprite;
        }
    }
}
using Windows.CharacterInfoWindow.Presenters;
using Windows.Common;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;

namespace Windows.CharacterInfoWindow.Views
{
    public class CharacterStatView : ReactiveView
    {
        [SerializeField] private TMP_Text description;

        private ICharacterStatViewPresenter currentPresenter;

        public void Initialize(ICharacterStatViewPresenter presenter)
        {
            DisposeSubscriptions();
            currentPresenter = presenter;
            SubscribeToPresenter(currentPresenter);
        }

        private void SubscribeToPresenter(ICharacterStatViewPresenter presenter)
        {
            presenter.Level.Subscribe(OnLevelChanged).AddTo(Subscriptions);
        }

        private void OnLevelChanged(string newValue)
        {
            description.text = $"{currentPresenter.Name}: {currentPresenter.Level}";
        }
    }
}
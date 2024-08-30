using Windows.CharacterInfoWindow.Presenters;
using Windows.Common;
using TMPro;
using UnityEngine;
using UniRx;

namespace Windows.CharacterInfoWindow.Views
{
    public class CharacterStatView : ReactiveView
    {
        [SerializeField] private TMP_Text _description;

        private ICharacterStatViewPresenter _currentPresenter;

        public void Initialize(ICharacterStatViewPresenter presenter)
        {
            DisposeSubscriptions();
            _currentPresenter = presenter;
            SubscribeToPresenter(_currentPresenter);
        }

        private void SubscribeToPresenter(ICharacterStatViewPresenter presenter)
        {
            presenter.Level.Subscribe(OnLevelChanged).AddTo(Subscriptions);
        }

        private void OnLevelChanged(string newValue)
        {
            _description.text = $"{_currentPresenter.Name}: {_currentPresenter.Level}";
        }
    }
}
using Windows.CharacterInfoWindow.Presenters;
using Windows.CharacterInfoWindow.Views;
using Windows.Common;
using Zenject;

namespace Windows.CharacterInfoWindow
{
    public sealed class CharacterInfoWindowAdapter : IWindowAdapter
    {
        private readonly CharacterInfoWindowModel model;
        private readonly CharacterInfoWindowView view;

        [Inject]
        public CharacterInfoWindowAdapter(CharacterInfoWindowModel model, CharacterInfoWindowView view)
        {
            this.model = model;
            this.view = view;

            this.view.CloseButtonClicked += CloseWindow;
            this.view.Initialize(new CharacterInfoWindowPresenter(this.model.UserInfo, this.model.CharacterInfo, this.model.PlayerLevel));
        }

        ~CharacterInfoWindowAdapter()
        {
            view.CloseButtonClicked -= CloseWindow;
        }

        public void OpenWindow()
        {
            view.gameObject.SetActive(true);
        }

        public void CloseWindow()
        {
            view.gameObject.SetActive(false);
        }
    }
}
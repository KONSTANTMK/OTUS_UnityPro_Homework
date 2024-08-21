using Windows.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Windows
{
    public class WindowOpenManager : MonoBehaviour
    {
        private IWindowAdapter characterInfoWindowAdapter;
        
        [Inject]
        private void Construct(IWindowAdapter characterInfoWindowAdapter)
        {
            this.characterInfoWindowAdapter = characterInfoWindowAdapter;
        }

        [Button]
        private void OpenCharacterInfo()
        {
            characterInfoWindowAdapter.OpenPopup();
        }

        [Button] 
        private void CloseCharacterInfo()
        {
            characterInfoWindowAdapter.ClosePopup();
        }
    }
}
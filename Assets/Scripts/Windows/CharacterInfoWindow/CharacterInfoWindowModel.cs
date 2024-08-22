using PlayerData;
using UnityEngine;
using CharacterInfo = PlayerData.CharacterInfo;

namespace Windows.CharacterInfoWindow
{
    public sealed class CharacterInfoWindowModel : MonoBehaviour
    {
        [SerializeField] private CharacterInfo characterInfo;
        [SerializeField] private UserInfo userInfo;
        [SerializeField] private PlayerLevel playerLevel;

        public CharacterInfo CharacterInfo => characterInfo;
        public UserInfo UserInfo => userInfo;
        public PlayerLevel PlayerLevel => playerLevel;
    }
}
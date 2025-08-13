using UnityEngine;

namespace _02.Scripts.Player
{
    public class CharacterManager : MonoBehaviour
    {
        private static CharacterManager _instance;
        public static CharacterManager Instance{
            get
            {
                if (_instance == null) //null 방어코드
                {
                    _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
                }
                return _instance;
            }
        }

        private Player _player;
        public Player Player {
            get
            {
                return _player;
            }
            set //플레이어 값을 가져와서 '저장'하니깐 set도 필요함
            {
                _player = value;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (_instance == this)
                {
                    Destroy(gameObject);
                    
                }
            }
        }
    }
}

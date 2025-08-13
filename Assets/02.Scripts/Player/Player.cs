using UnityEngine;

namespace _02.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerController playerController;
        public PlayerCondition playerCondition;

        private void Awake()
        {
            CharacterManager.Instance.Player = this;
            playerController = GetComponent<PlayerController>();
            playerCondition = GetComponent<PlayerCondition>();
        }
    }
}

using UnityEngine;

namespace _02.Scripts.Item
{
    public class ItemUse : ItemObject
    {
        // Start is called before the first frame update
        public string Eat()
        {
            string effectInfo = $"{itemInfo.effect}";
            float heal = itemInfo.heal;
            float hunger = itemInfo.hunger;
            
            Debug.Log(itemInfo.heal);
            Debug.Log(itemInfo.hunger);
            Debug.Log(playerCondition);
            
            //아이템 효과 적용
            playerCondition.Eat(hunger);
            playerCondition.Heal(hunger);
            return effectInfo;
        }
    }
}

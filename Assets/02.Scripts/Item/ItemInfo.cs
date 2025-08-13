using UnityEngine;

namespace _02.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/ItemInfo", order = 1)]
    public class ItemInfo : ScriptableObject
    {
        public string name;
        public string description;
        public float damage;
        public float heal;
        public float hunger;
        public float stamina; //사용했을 때 닳는 스테미나
        public string effect; //효과 정보
    }
}

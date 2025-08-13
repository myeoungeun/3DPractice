using UnityEngine;

namespace _02.Scripts.Player
{
    public class PlayerCondition : MonoBehaviour
    {
        public UIConditions uiConditions;

        Condition Health { get { return uiConditions.health; }}
        Condition Hunger { get { return uiConditions.hunger; }}
        Condition Stamina { get { return uiConditions.stamina; }}

        void Update()
        {
            Hunger.Minus(Time.deltaTime * uiConditions.hunger.passiveValue);
            Stamina.Plus(Time.deltaTime * uiConditions.stamina.passiveValue);

            if (Hunger.curValue == 0f) //최소 0 이하로 떨어질 수 없어서 <= 말고 == 사용함
            {
                Health.Minus(Time.deltaTime * uiConditions.health.passiveValue);
            }
            if (Health.curValue == 0f) //중첩 if보다 따로 빼는게 더 깔끔하고 오류 방지할 수 있을듯.
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            Health.Plus(amount);
        }

        public void Eat(float amount)
        {
            Hunger.Plus(amount);
        }

        public void Die()
        {
            Debug.Log("죽었다!");
        }

        public void UseStamina(float amount)
        {
            Stamina.Minus(amount);
        }
    }
}

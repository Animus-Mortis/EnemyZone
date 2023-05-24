using UnityEngine;
using UnityEngine.Events;

namespace Game.Bot
{
    public class HealthBot : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHP;
        [SerializeField] private float nowHP;
        [SerializeField] private UnityEvent DieEvent;
        [SerializeField] private UnityEvent<float, float> ChangeHPOnUI;

        public bool died { get; private set; }

        private void OnEnable()
        {
            died = false;
            nowHP = maxHP;
            ChangeHPOnUI.Invoke(nowHP, maxHP);
        }

        public void Die()
        {
            died = true;
            DieEvent.Invoke();
        }

        public void TakeDamage(float damage)
        {
            nowHP -= damage;
            ChangeHPOnUI.Invoke(nowHP, maxHP);
            if (nowHP <= 0)
                Die();
        }
    }
}
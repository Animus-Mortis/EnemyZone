using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHP;
        [SerializeField] private float nowHP;
        [SerializeField] private UnityEvent DieEvent;
        [SerializeField] private UnityEvent<float, float> ChangeHPOnUI;

        private bool die;
        public bool IsDie { get { return die; } }

        private void Start()
        {
            nowHP = maxHP;
        }

        public void ResetPlayer()
        {
            die = false;
            nowHP = maxHP;
            ChangeHPOnUI.Invoke(nowHP, maxHP);
        }

        public void TakeDamage(float damage)
        {
            nowHP -= damage;
            ChangeHPOnUI.Invoke(nowHP, maxHP);
            if (nowHP <= 0 && !die)
                Die();
        }

        public void Die()
        {
            die = true;
            DieEvent.Invoke();
        }

        public void SetHP(int value)
        {
            nowHP += value;
            if (nowHP > maxHP)
                nowHP = maxHP;
            ChangeHPOnUI.Invoke(nowHP, maxHP);
        }

        public bool Damaged()
        {
            return nowHP != maxHP;
        }
    }
}
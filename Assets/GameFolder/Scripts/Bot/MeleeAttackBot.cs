using Game.Player;
using System.Collections;
using UnityEngine;

namespace Game.Bot
{
    public class MeleeAttackBot : MonoBehaviour
    {
        [SerializeField] private float gamage;
        [SerializeField] private float speedAttack;

        public float distanceToAttack;

        private Animator animator;
        private int randomTypeAttack;
        private bool attacked;
        private PlayerHealth player;
        private Coroutine attackCoroutine;

        private void Start()
        {
            animator = GetComponent<MovingBot>().animator;
        }

        private void OnEnable()
        {
            if(player != null)
                attackCoroutine = StartCoroutine(CheckCanAttack());
        }

        public void SetPlayer(PlayerHealth playerHealth)
        {
            player = playerHealth;
            attackCoroutine = StartCoroutine(CheckCanAttack());
        }

        public void StopAttack()
        {
            StopCoroutine(attackCoroutine);
        }

        public void SetDamage()
        {
            player.TakeDamage(gamage);
        }

        private IEnumerator CheckCanAttack()
        {
            while (true)
            {
                while (Vector3.Distance(transform.position, player.transform.position) < distanceToAttack)
                {
                    attacked = true;
                    randomTypeAttack = Random.Range(1, 3);
                    animator.SetBool($"Attack 0{randomTypeAttack}", true);
                    yield return new WaitForSeconds(speedAttack);
                }

                if (attacked)
                {
                    animator.SetBool($"Attack 0{randomTypeAttack}", false);
                    attacked = false;
                }

                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
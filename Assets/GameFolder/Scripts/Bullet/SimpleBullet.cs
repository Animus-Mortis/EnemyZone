using Game.Bot;
using UnityEngine;

namespace Game.Bullet
{
    public class SimpleBullet : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;

        private GameObject target;

        public void SetTagret(GameObject newTarget)
        {
            target = newTarget;
        }

        private void FixedUpdate()
        {
            if(Vector3.Distance(transform.position, target.transform.position) > 0.1f && !target.GetComponent<HealthBot>().died)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else
            {
                target.GetComponent<HealthBot>().TakeDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
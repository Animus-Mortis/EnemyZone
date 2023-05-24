using UnityEngine;
using UnityEngine.Events;

namespace Game.Player
{
    public class CheckCollider : MonoBehaviour
    {
        [SerializeField] private UnityEvent takeLootEvent;
        [SerializeField] private UnityEvent saleLootEvent;

        private RengeAttack rengeAttack;

        private void Awake()
        {
            rengeAttack = GetComponent<RengeAttack>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "EnemyZone")
            {
                rengeAttack.StartShuting();
            }
            else if(other.tag == "PlayerZone")
            {
                rengeAttack.StopShuting();
                saleLootEvent.Invoke();
            }
            else if(other.tag == "Loot")
            {
                Destroy(other.gameObject);
                takeLootEvent.Invoke();
            }
        }
    }
}
using Game.Bot;
using UnityEngine;

namespace Game.Player
{
    public class CheckEnemy : MonoBehaviour
    {
        [SerializeField] private BotManager botManager;
        [SerializeField] private float maxDistance;

        private GameObject firstEnemy;

        public GameObject GetTarget()
        {
            if (botManager.bots.Count > 0)
            {
                for (int i = 0; i < botManager.bots.Count; i++)
                {
                    if (!botManager.bots[i].GetComponent<HealthBot>().died)
                    {
                        if (firstEnemy == null)
                            firstEnemy = botManager.bots[i];

                        else if (CheckDistance(firstEnemy.transform.position) > CheckDistance(botManager.bots[i].transform.position))
                            firstEnemy = botManager.bots[i];
                    }
                }
            }
            else
            {
                firstEnemy = null;
            }

            if(firstEnemy != null)
            {
                if (CheckDistance(firstEnemy.transform.position) > maxDistance)
                    firstEnemy = null;
            }

            return firstEnemy;
        }

        private float CheckDistance(Vector3 target)
        {
            return Vector3.Distance(target, transform.position);
        }
    }
}
using Game.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Bot
{
    public class BotManager : MonoBehaviour
    {
        [SerializeField] private PlayerHealth player;
        [SerializeField] private int maxCountBotOnScene;
        [SerializeField] private SpawnerBots spawnerBots;
        [SerializeField] private Transform[] movingPoints;

        public List<GameObject> bots = new List<GameObject>();

        private void Awake()
        {
            spawnerBots.TakeMaxBot(maxCountBotOnScene);   
        }

        public PlayerHealth GetPlayer()
        {
            return player;
        }

        public Transform[] GetPoints()
        {
            return movingPoints;
        }

        public void ResetCheckPlayer()
        {
            for (int i = 0; i < bots.Count; i++)
            {
                bots[i].GetComponent<MovingBot>().SetPlayer(player.transform);
            }
        }
    }
}
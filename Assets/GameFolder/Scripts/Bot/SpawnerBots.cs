using System.Collections.Generic;
using UnityEngine;

namespace Game.Bot
{
    public class SpawnerBots : MonoBehaviour
    {
        [SerializeField] private GameObject prefBot;
        [SerializeField] private Transform SpawnArea;
        [SerializeField] private float yPositionSpawn;
        [SerializeField] private BotManager manager;

        private int maxBot;

        public void TakeMaxBot(int value)
        {
            maxBot = value;
        }

        private void Start()
        {
            FillingPool();
        }

        private void FillingPool()
        {
            for (int i = 0; i < maxBot; i++)
            {
                var newBot = Instantiate(prefBot);
                newBot.transform.position = GetRandomePosition();
                newBot.GetComponent<MeleeAttackBot>().SetPlayer(manager.GetPlayer());
                newBot.GetComponent<MovingBot>().SetPlayer(manager.GetPlayer().GetComponent<Transform>());
                newBot.GetComponent<MovingBot>().AddMovingPoints(manager.GetPoints());
                newBot.GetComponent<DieBot>().AddSpwnerManager(this);
                manager.bots.Add(newBot);
            }
        }

        public void SpawnNewBot()
        {
            for (int i = 0; i < manager.bots.Count; i++)
            {
                if (!manager.bots[i].activeSelf)
                {
                    manager.bots[i].transform.position = GetRandomePosition();
                    manager.bots[i].SetActive(true);
                    manager.bots[i].GetComponent<MovingBot>().Respawn();
                }
            }
        }

        private Vector3 GetRandomePosition()
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(SpawnArea.transform.position.x - SpawnArea.localScale.x / 2, SpawnArea.transform.position.x + SpawnArea.localScale.x / 2);
            pos.z = Random.Range(SpawnArea.transform.position.z - SpawnArea.localScale.z / 2, SpawnArea.transform.position.z + SpawnArea.localScale.z / 2);
            pos.y = yPositionSpawn;
            return pos;
        }
    }
}
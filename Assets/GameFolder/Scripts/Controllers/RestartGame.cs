using Game.Bot;
using Game.Player;
using UnityEngine;

namespace Game.Controller
{
    public class RestartGame : MonoBehaviour
    {
        [SerializeField] private GameObject joystick;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private BotManager botManager;

        public void Restart()
        {
            joystick.SetActive(true);
            playerHealth.transform.position = spawnPoint.position;
            playerHealth.ResetPlayer();
            playerHealth.gameObject.SetActive(true);
            botManager.ResetCheckPlayer();
            gameObject.SetActive(false);
        }

    }
}
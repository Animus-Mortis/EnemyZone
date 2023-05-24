using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Controller
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [SerializeField] private float InterpolationSpeed = 10.0f;
        [SerializeField] private float stepOfPlayer = 1;

        private Vector3 target;
        private PlayerMove playerMove;

        private void Start()
        {
            playerMove = Player.GetComponent<PlayerMove>();

            transform.position = new Vector3(Player.position.x, 0, Player.position.z);
        }

        private void FixedUpdate()
        {
            target = new Vector3(Player.position.x + playerMove.move.x * stepOfPlayer, 0, Player.position.z + playerMove.move.z * stepOfPlayer);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * InterpolationSpeed);
        }
    }
}
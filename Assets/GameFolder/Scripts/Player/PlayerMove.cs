using Game.Controller;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private Joystick joystick;
        [SerializeField] private float speedMove = 1;
        [SerializeField] private float speedRotation;

        private Coroutine MovingCoroutin;

        [HideInInspector] public Vector3 move;

        private void OnEnable()
        {
            MovingCoroutin = StartCoroutine(Moving());
        }

        public void StopMoving()
        {
            StopCoroutine(MovingCoroutin);
        }

        private IEnumerator Moving()
        {
            while (true)
            {
                move = new Vector3(joystick.HorizontalAxis, 0, joystick.VerticalAxis);
                if (Vector3.Angle(Vector3.forward, move) > 1f || Vector3.Angle(Vector3.forward, move) == 0)
                {
                    Vector3 direct = Vector3.RotateTowards(transform.forward, move, speedRotation, 0.0f);
                    controller.transform.rotation = Quaternion.LookRotation(direct);
                }

                controller.SimpleMove(move * speedMove);

                yield return new WaitForFixedUpdate();
            }
        }

    }
}
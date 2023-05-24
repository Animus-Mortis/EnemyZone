using Game.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Bot
{
    public class MovingBot : MonoBehaviour
    {
        public Animator animator;

        [SerializeField] private Transform[] movingPoints;
        [SerializeField] private float distanceToVisionPlayer;
        [SerializeField] private float speedMove;

        public Transform playerTransform;
        private NavMeshAgent agent;
        private Transform nowMovingPoints;
        private MeleeAttackBot MeleeAttack;
        private bool seePlayer;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            MeleeAttack = GetComponent<MeleeAttackBot>();
        }

        public void Respawn()
        {
            StartCoroutine(CheckPlayer());
            StartCoroutine(CheckPosition());
        }

        public void ChangePosition(Vector3 position)
        {
            agent.Warp(position);
        }

        public void SetPlayer(Transform player)
        {
            playerTransform = player;
            StartCoroutine(CheckPlayer());
        }

        public void AddMovingPoints(Transform[] points)
        {
            movingPoints = points;
            StartCoroutine(CheckPosition());
        }

        private Transform ChangeMovingPosition()
        {
            int randomPoint = Random.Range(0, movingPoints.Length);

            while (movingPoints[randomPoint] == nowMovingPoints)
            {
                if (movingPoints.Length == 0) break;

                randomPoint = Random.Range(0, movingPoints.Length);
            }
            return nowMovingPoints = movingPoints[randomPoint];
        }

        private void MoveAgent(Vector3 position)
        {
            agent.destination = position;
        }

        private IEnumerator CheckPosition()
        {
            while (true)
            {
                while (!seePlayer)
                {
                    StartWalkAnimation();

                    if (nowMovingPoints != null)
                    {
                        while (Vector3.Distance(transform.position, nowMovingPoints.position) > 0.5f)
                        {
                            yield return new WaitForSeconds(0.2f);
                        }
                    }

                    MoveAgent(ChangeMovingPosition().position);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return null;
            }
        }

        private void StartWalkAnimation()
        {
            animator.ResetTrigger("Run Forward");
            animator.SetTrigger("Walk Forward");
        }

        private IEnumerator CheckPlayer()
        {
            Player.PlayerHealth playerHealth = playerTransform.GetComponent<Player.PlayerHealth>();
            while (!playerHealth.IsDie)
            {
                while (Vector3.Distance(transform.position, playerTransform.position) < distanceToVisionPlayer && !playerHealth.IsDie)
                {
                    animator.ResetTrigger("Walk Forward");

                    if (Vector3.Distance(transform.position, playerTransform.position) < MeleeAttack.distanceToAttack)
                    {
                        agent.speed = 0;
                        animator.ResetTrigger("Run Forward");
                    }
                    else
                    {
                        animator.SetTrigger("Run Forward");
                        agent.speed = speedMove;
                    }

                    MoveAgent(playerTransform.position);
                    seePlayer = true;
                    yield return new WaitForSeconds(0.2f);
                }

                NotSeePlayer();
                yield return null;
            }
            NotSeePlayer();
        }

        private void NotSeePlayer()
        {
            if (nowMovingPoints != null)
                MoveAgent(nowMovingPoints.position);
            StartWalkAnimation();
            seePlayer = false;
            agent.speed = speedMove;
        }
    }
}
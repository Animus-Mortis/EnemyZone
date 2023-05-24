using Game.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class RengeAttack : MonoBehaviour
    {
        [SerializeField] private float delayAttack;
        [SerializeField] private SimpleBullet bullet;
        [SerializeField] private int bulletPoolCount;
        [SerializeField] private Transform shutPoint;

        private CheckEnemy checkEnemy;
        private List<SimpleBullet> bullets = new List<SimpleBullet>();
        private Coroutine shutingCoroutine;

        private void Awake()
        {
            checkEnemy = GetComponent<CheckEnemy>();
        }

        private void Start()
        {
            FillingBullPool();
        }

        private void FillingBullPool()
        {
            for (int i = 0; i < bulletPoolCount; i++)
            {
                var newBullet = Instantiate(bullet);
                newBullet.gameObject.SetActive(false);
                bullets.Add(newBullet);
            }
        }

        public void StartShuting()
        {
            if(shutingCoroutine == null)
            {
                shutingCoroutine = StartCoroutine(Shuting());
            }
        }

        public void StopShuting()
        {
            if(shutingCoroutine != null)
            {
                StopCoroutine(shutingCoroutine);
                shutingCoroutine = null;
            }
        }

        private IEnumerator Shuting()
        {
            while (true)
            {
                if (checkEnemy.GetTarget() != null)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (!bullets[i].gameObject.activeSelf)
                        {
                            bullets[i].transform.position = shutPoint.position;
                            bullets[i].SetTagret(checkEnemy.GetTarget());
                            bullets[i].gameObject.SetActive(true);
                            break;
                        }
                    }
                }
                yield return new WaitForSeconds(delayAttack);
            }
        }
    }
}
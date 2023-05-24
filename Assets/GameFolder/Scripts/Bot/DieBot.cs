using Game.Actions;
using System.Collections;
using UnityEngine;

namespace Game.Bot
{
    public class DieBot : Die, IDied
    {
        private SpawnerBots spawnerBots;
        private LootSpawn lootSpawn;

        private new void Awake()
        {
            base.Awake();
            lootSpawn = GetComponent<LootSpawn>();
        }

        private void OnEnable()
        {
            Respawn();
        }

        public void Respawn()
        {
            if (DissolveCoroutine != null)
            {
                StopCoroutine(DissolveCoroutine);
                DissolveCoroutine = null;
            }

            for (int i = 0; i < dissolvingMaterial.Count; i++)
            {
                StartCoroutine(Visibled(dissolvingMaterial[i]));
            }
        }

        public void DieEffect()
        {
            if (DissolveCoroutine == null)
            {
                for (int i = 0; i < dissolvingMaterial.Count; i++)
                {
                    DissolveCoroutine = StartCoroutine(Dissolve(dissolvingMaterial[i]));
                }
            }

            ActionWithDieEvent.Invoke();
        }

        public void AddSpwnerManager(SpawnerBots spawnManager)
        {
            if (spawnerBots == null)
                spawnerBots = spawnManager;
        }

        public IEnumerator Dissolve(DissolvingMaterial dissolvingMaterial)
        {
            while (dissolvingMaterial.material.GetFloat("_Alfa") < 0.6f)
            {
                dissolvingMaterial.alfa += Time.fixedDeltaTime * speedDissolve;
                dissolvingMaterial.material.SetFloat("_Alfa", dissolvingMaterial.alfa);
                yield return new WaitForFixedUpdate();
            }

            DissolveCoroutine = null;
            gameObject.SetActive(false);

            if (lootSpawn != null)
                lootSpawn.SpawnProbability(transform.position);

            if (spawnerBots != null)
                spawnerBots.SpawnNewBot();
        }

        public IEnumerator Visibled(DissolvingMaterial dissolvingMaterial)
        {
            while (dissolvingMaterial.material.GetFloat("_Alfa") > -1f)
            {
                dissolvingMaterial.alfa -= Time.fixedDeltaTime * speedDissolve;
                dissolvingMaterial.material.SetFloat("_Alfa", dissolvingMaterial.alfa);

                yield return new WaitForFixedUpdate();
            }

            dissolvingMaterial.material.SetFloat("_Alfa", -1f);
        }
    }
}
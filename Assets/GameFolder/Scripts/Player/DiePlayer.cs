using Game.Actions;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class DiePlayer : Die, IDied
    {
        private void OnEnable()
        {
            Respawn();
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
            ActionWithDieEvent.Invoke();
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Actions
{
    public abstract class Die : MonoBehaviour
    {
        [SerializeField] protected float speedDissolve = 1;
        [SerializeField] protected Renderer[] rendererBody;
        [SerializeField] protected UnityEvent ActionWithDieEvent;

        protected List<DissolvingMaterial> dissolvingMaterial = new List<DissolvingMaterial>();
        protected Coroutine DissolveCoroutine;

        protected void Awake()
        {
            for (int i = 0; i < rendererBody.Length; i++)
            {
                dissolvingMaterial.Add(new DissolvingMaterial(rendererBody[i].material, rendererBody[i].material.GetFloat("_Alfa")));
            }
        }
    }
}
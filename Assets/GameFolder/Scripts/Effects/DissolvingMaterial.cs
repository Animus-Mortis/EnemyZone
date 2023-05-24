using UnityEngine;

public class DissolvingMaterial : MonoBehaviour
{
    public Material material;
    public float alfa;

    public DissolvingMaterial(Material _material, float _alfa)
    {
        material = _material;
        alfa = _alfa;
    }
}

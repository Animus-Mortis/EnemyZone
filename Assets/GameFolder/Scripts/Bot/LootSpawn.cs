using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    [SerializeField] private GameObject lootPref;
    [Range(0,100)]
    [SerializeField] private int procentChance;

    public void SpawnProbability(Vector3 position)
    {
        if (Random.value <= (procentChance / 100f))
        {
            var newloot = Instantiate(lootPref);
            newloot.transform.position = position;
        }
    }
}

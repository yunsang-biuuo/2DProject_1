using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject prefab;
    public static int once = 1;
    private void Update()
    {
        if (!BasicMove3D.InputEnable) return;
        SpawnPrefabs();
    }

    public void SpawnPrefabs()
    {
        if (once == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                float x = Random.Range(-30f, 30f);
                float z = Random.Range(-30f, 30f);
                Vector3 randomPlace = new Vector3(x, 0f, z);
                Instantiate(prefab, randomPlace, Quaternion.identity);
            }
            once++;
        }
    }
}

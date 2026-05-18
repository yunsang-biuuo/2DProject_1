using UnityEngine;

public class Spawner :MonoBehaviour
{
    public GameObject prefab;

    private void OnEnable()
    {
        SpawnPrefabs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPrefabs();
        }
    }
    
    public void SpawnPrefabs()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}

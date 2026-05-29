using UnityEngine;

public enum DNSpawnSpotType
{
    None = 0,
    Harvest,
    DropItem,
    Dialogue,
    Monster
}

public enum DNStartSpawnType
{
    None = 0,
    OnAwake,
    OnEnable,
    OnRange,
    // UniTask나 코루틴으로 일정 시간마다 랜덤 생성도 구현해보자
}

public class DaniTech_SpawnSpot : MonoBehaviour
{
    [SerializeField] private DNSpawnSpotType _spawnSpotType;
    [SerializeField] private DNStartSpawnType _startSpawnType;

    [SerializeField] private string _spawnObjectDataId;
    [SerializeField] private Collider2D Collider_OnSpawnStart;

    private void Awake()
    {
        if (_startSpawnType == DNStartSpawnType.OnAwake)
        {
            StartSpawn();
        }
    }

    private void Start()
    {
        if (_startSpawnType == DNStartSpawnType.OnEnable)
        {
            StartSpawn();
        }


        if (Collider_OnSpawnStart != null)
        {
            Collider_OnSpawnStart.enabled = (_startSpawnType == DNStartSpawnType.OnRange);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            StartSpawn();
        }
    }

    private void StartSpawn()
    {
        switch (_spawnSpotType)
        {
            case DNSpawnSpotType.Harvest:
            case DNSpawnSpotType.DropItem:
                SpawnFieldObject();
                this.gameObject.SetActive(false);
                break;
            case DNSpawnSpotType.Dialogue:
                UIManager.Instance.OpenDialogueUI(_spawnObjectDataId);
                this.gameObject.SetActive(false);
                break;
        }
    }

    private void SpawnFieldObject()
    {
        var data = GameDataManager.Instance.GetFieldObjectData(_spawnObjectDataId);
        if (data == null)
        {
            Debug.LogError($"FieldObjectData 없음: {_spawnObjectDataId}");
            return;
        }

        var prefab = Resources.Load<GameObject>(data.PrefabPath);
        if (prefab == null)
        {
            Debug.LogError($"프리팹 없음: {data.PrefabPath}");
            return;
        }

        Instantiate(prefab, this.transform.position, Quaternion.identity);
    }

}
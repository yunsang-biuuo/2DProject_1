using UnityEngine;

public enum SpawnSpotType
{
    None = 0,
    Harvest,
    DropItem,
    Dialogue,
    Monster,
    Player
}

public enum StartSpawnType
{
    None = 0,
    OnAwake,
    OnEnable,
    OnRange,
    // UniTask나 코루틴으로 일정 시간마다 랜덤 생성도 구현해보자
}

public class SpawnSpot : MonoBehaviour
{
    [SerializeField] private SpawnSpotType _spawnSpotType;
    [SerializeField] private StartSpawnType _startSpawnType;
    [SerializeField] private string _spawnObjectDataId;
    [SerializeField] private Collider2D Collider_OnSpawnStart;

    public SpawnSpotType SpawnSpotType => _spawnSpotType;

    private void Awake()
    {
        if (_startSpawnType == StartSpawnType.OnAwake)
        {
            StartSpawn();
        }
    }

    private void Start()
    {
        if (_startSpawnType == StartSpawnType.OnEnable)
        {
            StartSpawn();
        }


        if (Collider_OnSpawnStart != null)
        {
            Collider_OnSpawnStart.enabled = (_startSpawnType == StartSpawnType.OnRange);
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
            case SpawnSpotType.Harvest:
            case SpawnSpotType.DropItem:
                SpawnFieldObject();
                this.gameObject.SetActive(false);
                break;
            case SpawnSpotType.Dialogue:
                UIManager.Instance.OpenDialogueUI(_spawnObjectDataId);
                this.gameObject.SetActive(false);
                break;
            case SpawnSpotType.Player:
                SpawnPlayer();
                this.gameObject.SetActive(false);
                break;
            case SpawnSpotType.Monster:
                SpawnMonster();
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

    private void SpawnPlayer()
    {
        if (GameManager.Instance != null && GameManager.Instance.PlayerCharacter != null)
        {
            GameManager.Instance.PlayerCharacter.transform.position = this.transform.position;
        }
    }

    private void SpawnMonster()
    {
        var monsterData = GameDataManager.Instance.GetMonsterData(_spawnObjectDataId);
        if (monsterData == null)
        {
            Debug.LogError($"MonsterData 없음: {_spawnObjectDataId}");
            return;
        }

        var prefab = Resources.Load<GameObject>(monsterData.PrefabPath);
        if (prefab == null)
        {
            Debug.LogError($"몬스터 프리팹 없음: {monsterData.PrefabPath}");
            return;
        }

        GameObject monsterObj = Instantiate(prefab, this.transform.position, Quaternion.identity);

    }
    public void ForceSpawnFromServer()
    {
        StartSpawn();
    }
}
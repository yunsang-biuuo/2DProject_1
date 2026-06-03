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

    // 타입 정의
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
                SpawnEnermy();
                this.gameObject.SetActive(false);
                break;
        }
    }

    // 필드 오브젝트
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

    // 플레이어 캐릭터 이동
    private void SpawnPlayer()
    {
        if (GameManager.Instance != null && GameManager.Instance.PlayerCharacter != null)
        {
            GameManager.Instance.PlayerCharacter.transform.position = this.transform.position;
        }
    }

    // 몬스터 생성, 활성화
    private void SpawnEnermy()
    {
        var enermyData = GameDataManager.Instance.GetMonsterData(_spawnObjectDataId);
        if (enermyData == null)
        {
            Debug.LogError($"MonsterData 없음: {_spawnObjectDataId}");
            return;
        }

        string enrmyPrefabPath = $"2DPrefab/Enermy/{enermyData.PrefabPath}";

        var prefab = Resources.Load<GameObject>(enrmyPrefabPath);
        if (prefab == null)
        {
            Debug.LogError($"몬스터 프리팹 없음: {enrmyPrefabPath} (Assets/Resources/Monsters/ 폴더에 파일이 있는지 확인하세요!)");
            return;
        }

        GameObject monsterObj = Instantiate(prefab, this.transform.position, Quaternion.identity);
        monsterObj.name = enermyData.Name;
    }

    public void ForceSpawnFromServer()
    {
        StartSpawn();
    }

}
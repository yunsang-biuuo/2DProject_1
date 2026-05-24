using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnermy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveTime = 2f;
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float detectRange = 5f;
    [Header("Combat")]
    [SerializeField] private int maxHp = 3;
    [SerializeField] private string dropItemDataId; // 드롭할 아이템 데이터 ID

    private int _currentHp;
    private bool _isDead = false;
    private bool _isHit = false;
    public int EntityInstancId { get; private set; }
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    private Vector3 _moveDirection;
    private float _stateTimer;
    private bool _isWaiting = false;


    private AudioContrEnermy _audioContrMonster;
    private AnimController_Monster _animController;

    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animController = GetComponent<AnimController_Monster>();
        _audioContrMonster = GetComponent<AudioContrEnermy>();

        _playerTransform = GameObject.FindWithTag("Player")?.transform;
        _moveDirection = new Vector3(-1f, 0, 0);
        _stateTimer = moveTime;
        SetMeshDirectionByMoveDirection(-1);
        _animController.SetState(StateMonster.Roam);

        _currentHp = maxHp;
    }

    void Update()
    {
        if (_playerTransform != null &&
            Vector3.Distance(transform.position, _playerTransform.position) <= detectRange)
        {
            ChasePlayer();
            return;
        }
        _stateTimer -= Time.deltaTime;
        if (_stateTimer <= 0f)
        {
            _isWaiting = !_isWaiting;
            if (_isWaiting)
            {
                _stateTimer = waitTime;
                _animController.SetState(StateMonster.Idle);
                _audioContrMonster.StopRoam();
            }
            else
            {
                FlipDirection();
                _stateTimer = moveTime;
                _animController.SetState(StateMonster.Roam);
                _audioContrMonster.PlayRoam();
            }
        }
        if (!_isWaiting)
            transform.position += _moveDirection * moveSpeed * Time.deltaTime;
    }

    void ChasePlayer()
    {
        float dirX = _playerTransform.position.x - transform.position.x;
        transform.position += new Vector3(Mathf.Sign(dirX), 0, 0) * moveSpeed * Time.deltaTime;
        SetMeshDirectionByMoveDirection((int)Mathf.Sign(dirX));
        _animController.SetState(StateMonster.Roam);
        _audioContrMonster.PlayRoam();
    }

    public void InitEnemyInfo(int instanceId)
    {
        EntityInstancId = instanceId;
    }

    void FlipDirection()
    {
        _moveDirection.x *= -1f;
        SetMeshDirectionByMoveDirection((int)_moveDirection.x);
    }

    void SetMeshDirectionByMoveDirection(int x)
    {
        _spriteRenderer.flipX = (x > 0);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    public void TakeDamage(int damage)
    {
        if (_isDead || _isHit) return;
        _currentHp -= damage;

        if (_currentHp <= 0)
            StartCoroutine(HitThenDieCo());
        else
            StartCoroutine(HitFlashCo());
    }

    private IEnumerator HitFlashCo()
    {
        _isHit = true;
        for (int i = 0; i < 3; i++)
        {
            _spriteRenderer.color = new Color(1, 0.3f, 0.3f, 0.3f);
            yield return new WaitForSeconds(0.08f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.08f);
        }
        _isHit = false;
    }

    private IEnumerator HitThenDieCo()
    {
        _isDead = true;
        yield return HitFlashCo(); // 마지막 피격 깜빡임 끝날 때까지 대기
        DropItem();
        Destroy(gameObject);
    }

    private void DropItem()
    {
        if (string.IsNullOrEmpty(dropItemDataId)) return;

        var data = GameDataManager.Instance.GetFieldObjectData(dropItemDataId);
        if (data == null) return;

        var prefab = Resources.Load<GameObject>(data.PrefabPath);
        if (prefab == null) return;

        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
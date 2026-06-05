using UnityEngine;
using System.Collections;

public class EnermyClose : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int _enermyDamage = 2;
    [SerializeField] private string dropItemDataId; // 드롭할 아이템 데이터 ID

    [Header("Setting")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2.5f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerLayer;

    private int _currentHp;
    private bool _isDead = false;
    private bool _isDamaged = false;
    public int EntityInstancId { get; private set; }
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    private float _attackTimer = 0f;

    private AudioContrEnermy _audioContrMonster;
    private AnimController_Enermy _animController;

    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animController = GetComponent<AnimController_Enermy>();
        _audioContrMonster = GetComponent<AudioContrEnermy>();

        _playerTransform = GameObject.FindWithTag("Player")?.transform;

        _animController.SetState(StateEnermy.Idle);

        _currentHp = maxHp;
    }

    void Update()
    {
        if (_isDead) return;

        if (_attackTimer > 0f) _attackTimer -= Time.deltaTime;

        // 플레이어 존재 여부와 거리를 파악
        if (_playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, _playerTransform.position);

            if (distance <= attackRange)
            {
                if (_attackTimer <= 0f)
                {
                    _animController.SetState(StateEnermy.Attack);
                    _attackTimer = attackCooldown;
                }
                else
                {
                    // 공격 후 쿨타임 중에는 제자리에 서서 가만히 대기
                    _animController.SetState(StateEnermy.Idle);
                }
                return; // 공격 중일 때, 로직 건너띄기
            }

            if (distance <= detectRange)
            {
                float dirX = _playerTransform.position.x - transform.position.x;
                transform.position += new Vector3(Mathf.Sign(dirX), 0, 0) * moveSpeed * Time.deltaTime;
                SetMeshDirectionByMoveDirection((int)Mathf.Sign(dirX));

                _animController.SetState(StateEnermy.Walk);

                return;
            }
        }

        _animController.SetState(StateEnermy.Idle);
    }

    // 피격 판정 함수
    public void OnEnemyAttackHit()
    {
        if (_isDead || _attackPoint == null) return;

        Collider2D hitPlayer = Physics2D.OverlapCircle(_attackPoint.position, attackRange, _playerLayer);

        if (hitPlayer != null)
        {
            if (hitPlayer.TryGetComponent<CyborgPlayer>(out var player))
            {
                player.TakeDamage(_enermyDamage); // 플레이어 피격 함수 호출
            }
        }
    }

    public void InitEnemyInfo(int instanceId)
    {
        EntityInstancId = instanceId;
    }

    void SetMeshDirectionByMoveDirection(int x)
    {
        _spriteRenderer.flipX = (x > 0);
    }

    void OnDrawGizmosSelected()
    {
        // 탐지 범위 (큰 원 - 빨간색)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        // 공격 범위 (작은 원 - 노란색)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            StartCoroutine(DeadCo());
        }
        else
        {
            StartCoroutine(HitFlashCo());
        }
    }

    private IEnumerator HitFlashCo()
    {
        _isDamaged = true;
        for (int i = 0; i < 3; i++)
        {
            _spriteRenderer.color = new Color(1, 0.3f, 0.3f, 0.5f);
            yield return new WaitForSeconds(0.08f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.08f);
        }
        _isDamaged = false;
    }

    private IEnumerator DeadCo()
    {
        _isDead = true;     // Update() 차단

        if (_animController != null)
        {
            _animController.SetState(StateEnermy.Dead);
        }

        yield return new WaitForSeconds(1f);

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

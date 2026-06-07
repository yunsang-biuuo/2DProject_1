using UnityEngine;

public class CyborgPlayer : MonoBehaviour
{
    

    [Header("기본 설정")]
    [SerializeField] private int _attackDamage = 1;
    [SerializeField] private int _maxHp = 60;
    public float moveSpeed = 5f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public static bool InputEnable = true;

    [Header("공격 설정")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private LayerMask _enermyLayer;

    [Header("애니메이션")]
    [SerializeField] private AnimController_Player _animationController;

    [Header("지면 체크 설정")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    float _horizontalInput;
    bool _isGrounded = false;
    bool _isRunning = false;
    bool _lookRight = true;

    private int _atkCount = 0;
    private bool _isDead = false;
    private int _currentHp;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Start()
    {
        ResetHP();
    }

    void Update()
    {
        // 사망했거나 입력이 비활성화 상태면 조작 차단
        if (!InputEnable || _isDead) return;

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _isRunning = Input.GetKey(KeyCode.LeftShift) && _horizontalInput != 0;

        // 방향 전환
        if (_horizontalInput > 0 && !_lookRight) Flip();
        else if (_horizontalInput < 0 && _lookRight) Flip();

        // 이동 애니메이션 설정
        _animator.SetBool("IsWalking", _horizontalInput != 0);
        //_animator.SetBool("IsRunning", _isRunning);

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
            _animationController.SetState(StatePlayer.Jump);
        }

        // 공격 처리
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (_isDead) return;

        // 지면 체크
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        if (_isGrounded) _animator.SetBool("IsJumping", false);

        float currentSpeed = _isRunning ? runSpeed : moveSpeed;
        _rigidbody.linearVelocity = new Vector2(_horizontalInput * currentSpeed, _rigidbody.linearVelocity.y);
    }

    public void ResetHP()
    {
        _currentHp = _maxHp;
        _isDead = false;
        InputEnable = true;

        if (_animator != null)
        {
            _animator.SetBool("IsDead", false);
        }
    }

    public void Attack()    // 공격 순서 계산 및 실행 함수
    {
        _animator.SetInteger("AtkCount", _atkCount);

        _animationController.SetState(StatePlayer.Attack);

        _atkCount = (_atkCount + 1) % 3;
    }

    public void TakeDamage(int enermydamage)
    {
        if (_isDead) return;

        _currentHp -= enermydamage;
        Debug.Log($"플레이어가 데미지를 입었습니다. 현재 체력: {_currentHp}/{_maxHp}");

        if (_currentHp <= 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger("IsDamaged");
        }
    }

    public void Die()
    {
        if (_isDead) return;

        _isDead = true;
        InputEnable = false; // 조작 불가능 설정
        _rigidbody.linearVelocity = Vector2.zero; 

        _animationController.SetState(StatePlayer.Dead);
        GameManager.Instance.OnPlayerDead();
    }


    void Flip()     // 스프라이트 방향전환
    {
        _lookRight = !_lookRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // 피격 판정 함수
    public void OnAttackHit()
    {
        var hits_1 = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enermyLayer);
        foreach (var hit in hits_1)
        {
            if (hit.TryGetComponent<EnermyClose>(out var enemy))
            {
                enemy.TakeDamage(_attackDamage);
            }
        }

        var hits_2 = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enermyLayer);
        foreach (var hit in hits_2)
        {
            if (hit.TryGetComponent<EnermyCloseR>(out var enemy))
            {
                enemy.TakeDamage(_attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;   // 지면 체크 빨강
            Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
        }

        if (_attackPoint != null)
        {
            Gizmos.color = Color.yellow;    // 어택 노랑
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }
}

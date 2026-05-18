using UnityEngine;

public class BasicMovePlayer : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 4f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public static bool InputEnable = true;

    [Header("지면 체크 설정")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    [Header("공격 설정")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.8f;
    [SerializeField] private LayerMask _monsterLayer;    // 몬스터 레이어
    [SerializeField] private int _attackDamage = 1;

    [Header("애니메이션")]
    [SerializeField] private AnimController_Player _animationController;

    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    float _horizontalInput;
    bool _isGrounded = false;
    bool _isRunning = false;
    bool _lookRight = true;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (!InputEnable) return;

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _isRunning = Input.GetKey(KeyCode.LeftShift) && _horizontalInput != 0;

        // 방향 전환
        if (_horizontalInput > 0 && !_lookRight) Flip();
        else if (_horizontalInput < 0 && _lookRight) Flip();

        _animator.SetBool("IsMoving", _horizontalInput != 0);
        _animator.SetBool("IsRunning", _isRunning);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
            _animator.SetBool("IsJumping", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("DoAttack");
        }
    }

    void FixedUpdate()
    {
        // 지면 체크
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        if (_isGrounded) _animator.SetBool("IsJumping", false);

        float currentSpeed = _isRunning ? runSpeed : moveSpeed;
        _rigidbody.linearVelocity = new Vector2(_horizontalInput * currentSpeed, _rigidbody.linearVelocity.y);
    }

    void Flip()
    {
        _lookRight = !_lookRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void OnAttackHit()
    {
        var hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _monsterLayer);
        foreach (var hit in hits)
        {
            hit.GetComponent<BasicMoveMonster>()?.TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
        }

        // 공격 범위 기즈모
        if (_attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }
}

using UnityEngine;

public class BasicMove3D : MonoBehaviour
{
    public GameObject Cube;
    public float _rayCastDistance = 10.0f;
    public static bool InputEnable = false;

    Vector3 moveDirection;
    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _rayCastDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

    void Update()
    {
        if (!InputEnable) return;

        float moveSpeed = 5f;

        moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(new Vector3(0, -0.3f, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0.3f, 0));
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float jumpForce = 5f;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        this.gameObject.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);


        // 레이캐스트
        if (Input.GetMouseButtonDown(0)) // 0 = 마우스 좌클릭
        {
            Ray ray = new Ray(transform.position + transform.forward * 0.5f, transform.forward);
            RaycastHit hit;

            Debug.DrawRay(transform.position + transform.forward * 0.5f, transform.forward * 15f, Color.red, 2f);

            if (Physics.Raycast(ray, out hit, 15f))
            {
                Monster monster = hit.collider.GetComponent<Monster>();
                if (monster != null)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("클릭됨!");
        }
    }
}

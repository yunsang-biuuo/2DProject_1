using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 따라갈 캐릭터의 Transform
    public Vector3 offset;    // 캐릭터와 카메라 사이의 거리 오프셋
    public float smoothSpeed = 0.125f; // 카메라가 따라가는 부드러운 속도 (0~1 사이)

    void LateUpdate()
    {
        if (target != null)
        {
            // 카메라가 가야 할 목표 위치 계산
            Vector3 desiredPosition = target.position + offset;

            // Lerp를 이용해 현재 위치에서 목표 위치로 부드럽게 이동
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 카메라 위치 업데이트
            transform.position = smoothedPosition;
        }
    }
}

using UnityEngine;

public class LobbyTriggerHandler : MonoBehaviour
{
    // 💡 3D의 OnTriggerEnter 대신 2D 전용 함수를 사용합니다.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tag 검사
        if (other.CompareTag("Player"))
        {
            // 게임 일시 정지
            Time.timeScale = 0f;

            // 로비 UI 다시 열기
            UIManager.Instance.OpenMainUI(UIType.RobbyUI);

            Debug.Log("2D 트리거 작동: 로비 UI 출력");
        }
    }
}
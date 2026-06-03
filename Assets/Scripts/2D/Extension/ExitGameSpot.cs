using UnityEngine;

public class ExitGameSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;

            // 로비 UI 다시 열기
            UIManager.Instance.OpenMainUI(UIType.RobbyUI);

            Debug.Log("2D 트리거 작동: 로비 UI 출력");
        }
    }
}
using UnityEngine;

public class ExitGameSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.OpenGameClearPopUI();
        }
    }
}
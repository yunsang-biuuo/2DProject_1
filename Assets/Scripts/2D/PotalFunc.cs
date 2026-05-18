using UnityEngine;
using System.Collections;

public class PotalFunc : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Exit());
        }
    }

    private IEnumerator Exit()
    {
        yield return null; // 다음 프레임에 실행

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

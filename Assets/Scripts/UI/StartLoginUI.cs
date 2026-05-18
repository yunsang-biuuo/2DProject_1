using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartLoginUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnClickStartButton);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnClickStartButton);
    }

    private void OnClickStartButton()
    {
        startButton.interactable = false;
        StartCoroutine(CoRestart());
    }

    private IEnumerator CoRestart()
    {
        UIManager.Instance.OpenLoadingUI();

        yield return new WaitForSeconds(3f);

        UIManager.Instance.CloseStartLoginUI();
        UIManager.Instance.CloseLoadingUI();

        UIManager.Instance.OpenLayout_Top();
    }
    //private IEnumerator CoRestart()
    //{
    //    bool isDone = false;

    //    var uiBase = UIManager.Instance.OpenLoadingUI(); // Extension 사용
    //    if (uiBase is LoadingUI loadingUI)
    //    {
    //        loadingUI.StartLoading(() => isDone = true);
    //    }

    //    yield return new WaitUntil(() => isDone);

    //    UIManager.Instance.CloseLoadingUI();
    //    UIManager.Instance.CloseStartLoginUI();
    //    UIManager.Instance.OpenLayout_Top();
    //}
}

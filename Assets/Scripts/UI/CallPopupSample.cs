using UnityEngine;
using UnityEngine.UI;

public class CallPopupSample : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button menuButton;

    private void OnEnable()
    {
        menuButton.onClick.AddListener(OnClickMenu);
    }
    private void OnDisable()
    {
        menuButton.onClick.RemoveListener(OnClickMenu);
    }
    private void OnClickMenu()
    {
        UIManager.Instance.OpenUI(UIRootType.PopupUI, UIType.PopupSample);
        Time.timeScale = 0f;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonUI : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button menuBtn;

    private void Awake()
    {
        if (menuBtn != null) menuBtn.onClick.AddListener(OnClickOpenMenuPopup);
    }

    private void OnClickOpenMenuPopup()
    {
        UIManager.Instance.OpenExitGamePopUI();
    }
}
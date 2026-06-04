using UnityEngine;
using UnityEngine.UI;

public class StartLoginUI : UIBase
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

        GameManager.Instance.OnLoginProcess();
    }
}

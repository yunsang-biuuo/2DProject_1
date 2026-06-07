using UnityEngine;
using UnityEngine.UI;


public class StoryScUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button closeBtn;

    private void Start()
    {
        closeBtn.onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()

    {
        UIManager.Instance.CloseStoryScUI();
    }
}

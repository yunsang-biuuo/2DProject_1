using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollBtnSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text text;

    public void Setup(Sprite iconSprite, string name)
    {
        if (iconSprite != null)
        {
            icon.sprite = iconSprite;
        }
        text.text = name;
    }

    void Start()
    {
        ChangeImage();
    }

    void ChangeImage()
    {
        Sprite newSprite = GameUtil.LoadSprite("wand1");

        if (newSprite != null)
        {
            icon.sprite = newSprite;
            Debug.Log("이미지 교체 완료!");
        }
    }
}

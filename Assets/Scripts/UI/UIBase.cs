using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    protected bool _isInitialized = false;

    public virtual void Init()
    {
        if (_isInitialized) return;
        _isInitialized = true;

        //  여기에 자식 컴포넌트(Button, Text 등)를 자동으로 찾는 바인딩 로직을 넣습니다.
    }

    public virtual void OnOpen()
    {
        Init();
        gameObject.SetActive(true);
    }

    public virtual void OnClose()
    {
        gameObject.SetActive(false);
    }
}

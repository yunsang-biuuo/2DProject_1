using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : UIBase
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    private Action _onLoadingComplete;

    public void StartLoading(Action onComplete)
    {
        _onLoadingComplete = onComplete;

        StopAllCoroutines();
        StartCoroutine(LoadingRoutine());
    }

    private IEnumerator LoadingRoutine()
    {
        float duration = 2f;
        float elapsed = 0f;
        _slider.value = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            _slider.value = progress;

            if (_text != null)
                _text.text = $"Loading... {(int)(progress * 100)}%";

            yield return null;
        }

        _slider.value = 1f;
        if (_text != null) _text.text = "Completed!";

        yield return new WaitForSecondsRealtime(0.5f);

        // 스스로 Close하지 않고, 매니저에게 끝났다고 알려줍니다.
        _onLoadingComplete?.Invoke();
    }
}

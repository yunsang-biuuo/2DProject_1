using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIBase
{
    [Header("Tab Buttons")]
    [SerializeField] private Button gameplayTabBtn;
    [SerializeField] private Button soundTabBtn;
    [SerializeField] private Button closeBtn;

    [Header("Layout Panels")]
    [SerializeField] private GameObject gameplayLayout;
    [SerializeField] private GameObject soundLayout;

    [Header("Sound Controls")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private AudioSource bgmAudioSource;

    private void Awake()
    {
        if (gameplayTabBtn != null) gameplayTabBtn.onClick.AddListener(OnClickGameplayTab);
        if (soundTabBtn != null) soundTabBtn.onClick.AddListener(OnClickSoundTab);
        if (closeBtn != null) closeBtn.onClick.AddListener(OnClickClose);

        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        }
    }

    private void OnClickGameplayTab()
    {
        if (gameplayLayout != null) gameplayLayout.SetActive(true);
        if (soundLayout != null) soundLayout.SetActive(false);
    }

    private void OnClickSoundTab()
    {
        if (gameplayLayout != null) gameplayLayout.SetActive(false);
        if (soundLayout != null) soundLayout.SetActive(true);
    }

    private void OnEnable()
    {
        if (gameplayLayout != null) gameplayLayout.SetActive(true);
        if (soundLayout != null) soundLayout.SetActive(false);

        // 사운드 매니저의 실제 현재 볼륨을 슬라이더 바에 동기화
        if (bgmSlider != null && SoundManager.Inst != null)
        {
            bgmSlider.value = SoundManager.Inst.GetBGMVolume();
        }
    }

    private void OnBgmVolumeChanged(float value)
    {
        if (SoundManager.Inst != null)
        {
            SoundManager.Inst.SetBGMVolume(value);
        }
    }

    private void OnClickClose()
    {
        if (UIManager.Instance != null) // UI 매니저가 씬에 없을 경우 대비
        {
            UIManager.Instance.CloseSettingUI();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
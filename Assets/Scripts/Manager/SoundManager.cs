using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSourcePlayer;
    [SerializeField] private AudioSource BGMSourcePlayer;

    public static SoundManager Inst { get; set; }

    private void Awake()
    {
        Inst = this;
    }

    public string GetSoundPath(string soundDataId)
    {
        string path = soundDataId;
        return path;
    }

    // 효과음 재생 (겹쳐서 재생 가능)
    public void PlaySFX(string soundDataId)
    {

        GameUtil.LoadAndPlayAudioClip(SFXSourcePlayer, soundDataId).Forget();
    }

    // 배경음 재생 (교체 재생)
    public void PlayBGM(string soundDataId)
    {
        GameUtil.LoadAndPlayAudioClip(BGMSourcePlayer, soundDataId, isLoop: true).Forget();
    }

    public float GetBGMVolume()
    {
        return BGMSourcePlayer != null ? BGMSourcePlayer.volume : 0.5f;
    }

    // [추가] 슬라이더 값을 받아 실제 볼륨 수정
    public void SetBGMVolume(float volume)
    {
        if (BGMSourcePlayer != null)
        {
            BGMSourcePlayer.volume = volume;
        }
    }

    public void StopBGM()
    {
        BGMSourcePlayer.Stop();
    }

    public void StopSFX()
    {
        SFXSourcePlayer.Stop();
    }

}

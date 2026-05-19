using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSourcePlayer;
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

        GameUtil.LoadAndPlayAudioClip(AudioSourcePlayer, soundDataId).Forget();
    }

    // 배경음 재생 (교체 재생)
    public void PlayBGM(string soundDataId)
    {
        GameUtil.LoadAndPlayAudioClip(BGMSourcePlayer, soundDataId, isLoop: true).Forget();
    }

    public void StopBGM()
    {
        BGMSourcePlayer.Stop();
    }

    public void StopSFX()
    {
        AudioSourcePlayer.Stop();
    }

}

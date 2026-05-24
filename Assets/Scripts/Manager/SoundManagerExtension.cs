using UnityEngine;

    public enum BGMType
    {
        None = 0,
        Lobby,
        Battle,
        Boss
    }

    public static class SoundManagerExtension
    {
        public static string GetBGMPath(this SoundManager soundManager, BGMType bgmType)
        {
            string path = string.Empty;
            path = $"Audio/BGM/{bgmType}";
            return path;
        }

        public static void PlayLobbyBGM(this SoundManager soundManager)
        {
            soundManager.PlayBGM(soundManager.GetBGMPath(BGMType.Lobby));
        }

        public static void PlayBattleBGM(this SoundManager soundManager)
        {
            soundManager.PlayBGM(soundManager.GetBGMPath(BGMType.Battle));
        }

        public static void PlayBossBGM(this SoundManager soundManager)
        {
            soundManager.PlayBGM(soundManager.GetBGMPath(BGMType.Boss));
        }

    }
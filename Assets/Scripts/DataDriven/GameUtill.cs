using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public static class GameUtil
{
    private static long _lastId = 0;

    public static void LoadFullData()
    {
        GameDataManager.Instance.LoadSkillData("Skill");
        GameDataManager.Instance.LoadCharacterData("Character");
        GameDataManager.Instance.LoadWeaponData("Weapon");
        GameDataManager.Instance.LoadCostumeData("Costume");
        GameDataManager.Instance.LoadItemData("Item");
        GameDataManager.Instance.LoadMonsterData("Monster");
        GameDataManager.Instance.LoadDialogueData("Dialogue");
        GameDataManager.Instance.LoadDialogueGroupData("DialogueGroup");
        GameDataManager.Instance.LoadFieldObjectData("FieldObject");
    }

    public static int CalcCharacterFinalDamage(int curCharacterLevel, int levelPerDamage, bool isCritical)
    {
        int damagePerLevel = (curCharacterLevel + levelPerDamage);
        int finalDamage = isCritical ? (damagePerLevel * 2) : damagePerLevel;
        return finalDamage;
    }

    // 리소스 로딩(동기)
    public static Sprite LoadSprite(string spriteName)
    {
        Sprite loadedSprite = Resources.Load<Sprite>($"Image/{spriteName}");

        if (loadedSprite != null)
        {
            return loadedSprite;
        }

        Debug.LogError($"에셋을 찾을 수 없습니다: {spriteName}");
        return null;
    }

    // 리소스 로딩(비동기)
    public static async UniTask<Sprite> LoadAndSetSpriteImage(Image targetImage, string spritePath)
    {
        Sprite sprite = await DaniTechResourceManager.Inst.LoadSprite(spritePath);
        if (sprite != null)
        {
            targetImage.sprite = sprite;
        }
        return sprite;
    }

    public static async UniTaskVoid LoadAndPlayAudioClip(AudioSource audioSource, string audioPath, bool isLoop = false)
    {
        AudioClip clip = await DaniTechResourceManager.Inst.LoadAsset<AudioClip>(audioPath);
        if (clip == null)
        {
            Debug.LogError($"{audioPath}를 찾을 수 없습니다! 어드레서블 설정이 되어 있는지 확인해주세요.");
            return;
        }

        if (isLoop == true)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public static async UniTaskVoid LoadAndSetTexture(RawImage targetRawImage, string texturePath)
    {
        targetRawImage.gameObject.SetActive(false);
        Texture texture = await DaniTechResourceManager.Inst.LoadAsset<Texture>(texturePath);
        if (texture != null)
        {
            targetRawImage.texture = texture;
        }
        targetRawImage.gameObject.SetActive(true);
    }

    // 다이얼로그
    public static List<string> GetDialogueIdList(string dialogueGroupId)
    {
        var list = new List<string>();

        var data = GameDataManager.Instance.GetDialogueGroupData(dialogueGroupId);
        if (data != null)
        {
            var idArr = data.DialogueIdList;
            foreach (var id in idArr)
            {
                list.Add(id);
            }
        }

        return list;
    }

    // 그냥 유니크 키가 발급되어야 할 때 사용하려고 만든 것 (의미가 있는 건 아니므로 사용만 하세요)
    public static long GenerateUniqueId()
    {
        long newId = DateTime.UtcNow.Ticks;

        // 원자적 연산으로 안전하게 ID 갱신
        while (true)
        {
            long lastId = Volatile.Read(ref _lastId);

            // 만약 현재 시간이 이전 ID보다 작거나 같다면 (루프가 너무 빠른 경우 포함)
            // 이전 ID + 1로 강제 설정하여 중복 방지
            long idToAssign = (newId <= lastId) ? lastId + 1 : newId;

            // _lastId가 내가 읽은 시점과 같다면 idToAssign으로 교체 (성공 시 루프 탈출)
            if (Interlocked.CompareExchange(ref _lastId, idToAssign, lastId) == lastId)
            {
                return idToAssign;
            }
            // 그 사이 다른 스레드가 값을 바꿨다면 다시 시도
        }
    }

}

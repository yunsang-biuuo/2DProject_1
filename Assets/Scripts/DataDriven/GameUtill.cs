using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

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
        GameDataManager_2.Instance.LoadData<EntityData>("Entity");
        GameDataManager_2.Instance.LoadData<TowerData>("Tower");
        GameDataManager_2.Instance.LoadData<AbilityData>("Ability");
        GameDataManager_2.Instance.LoadData<EnemyData>("Enemy");
        GameDataManager_2.Instance.LoadData<StageData>("Stage");
        GameDataManager_2.Instance.LoadData<WaveData>("Wave");
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
        Sprite sprite = await ResourceManager.Inst.LoadSprite(spritePath);
        if (sprite != null)
        {
            targetImage.sprite = sprite;
        }
        return sprite;
    }

    public static async UniTaskVoid LoadAndPlayAudioClip(AudioSource audioSource, string audioPath, bool isLoop = false)
    {
        AudioClip clip = await ResourceManager.Inst.LoadAsset<AudioClip>(audioPath);
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
        // 비동기로 로드하기 전까지는 해당 오브젝트를 잠깐 비활성화 해준다
        targetRawImage.gameObject.SetActive(false);
        Texture texture = await ResourceManager.Inst.LoadAsset<Texture>(texturePath);
        if (texture != null)
        {
            targetRawImage.texture = texture;
        }
        targetRawImage.gameObject.SetActive(true);
    }

    public static async UniTask<Animator> LoadAndMeshObjectAndBindAnimator(Transform parent, string meshObjectPath, string specificAnimContollerPath = "")
    {
        // 1) 3D Mesh 오브젝트를 먼저 로드해서 동적 생성한다
        var loadedMeshObject = await ResourceManager.Inst.InstantiateAsync(meshObjectPath, parent);
        if (loadedMeshObject == null)
        {
            Debug.LogError($"{meshObjectPath}를 찾을 수 없습니다! 어드레서블 설정이 되어 있는지 확인해주세요.");
            return null;
        }

        // 2) 3D Mesh 오브젝트가 하위에 있는 경우 실패할 수 있으니 꼭 최상위 부모 오브젝트에 Animator을 넣어두자
        var animator = loadedMeshObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError($"{loadedMeshObject.name}프리팹에 최상위 오브젝트가 Animator를 갖고 있지 않습니다!");
            return null;
        }

        // 3) 그 3D Mesh에 기본 애니메이션 컨트롤러가 아니라 지정한 걸로 바꾸고 싶다면 명시
        // ex.PlayerController 또는 NpcAnimController 같이 내가 만든 것을 써야할때
        if (string.IsNullOrEmpty(specificAnimContollerPath) == false)
        {
            var loadedAnimatorController = await ResourceManager.Inst.LoadAsset<RuntimeAnimatorController>(specificAnimContollerPath);
            animator.runtimeAnimatorController = loadedAnimatorController;
        }

        return animator;
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

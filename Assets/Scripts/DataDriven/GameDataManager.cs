using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        // +++ C# 콘솔때와 다르게 이제 Main()함수가 아닌
        // 모노의 메서드에서 호출될 수 있으므로, 데이터 매니저가 활성화되면 바로 모든 데이터를 한번 받아오자
        // 이처리는 원하는 시점이 있다면 이전해도 된다
        GameUtil.LoadFullData();
    }

    // --- JsonUtility의 한계를 극복하기 위한 Wrapper 클래스 ---
    [Serializable]
    private class SerializationWrapper<T>
    {
        public List<T> items; // JSON 파일의 루트 키 이름이 "items"여야 함
    }
    // ---------------------------------------------------

    public Dictionary<string, CharacterData> CharacterDataList { get; private set; } = new Dictionary<string, CharacterData>();
    public Dictionary<string, SkillData> SkillDataList { get; private set; } = new Dictionary<string, SkillData>();
    public Dictionary<string, WeaponData> WeaponDataList { get; private set; } = new Dictionary<string, WeaponData>();
    public Dictionary<string, CostumeData> CostumeDataList { get; private set; } = new Dictionary<string, CostumeData>();
    public Dictionary<string, ItemData> ItemDataList { get; private set; } = new Dictionary<string, ItemData>();
    public Dictionary<string, DialogueGroupData> DialogueGroupDataList { get; private set; } = new Dictionary<string, DialogueGroupData>();
    public Dictionary<string, DialogueData> DialogueDataList { get; private set; } = new Dictionary<string, DialogueData>();
    public Dictionary<string, FieldObjectData> FieldObjectDataList { get; private set; } = new Dictionary<string, FieldObjectData>();
    public Dictionary<string, MonsterData> MonsterDataList { get; private set; } = new Dictionary<string, MonsterData>();

    private Dictionary<string, T> LoadData<T>(string resourcePath) where T : GameDataBase
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>(resourcePath);
        if (jsonAsset == null)
        {
            Debug.LogError($"[Error] 리소스를 찾을 수 없습니다: {resourcePath}");
            return new Dictionary<string, T>();
        }
        string wrappedJson = "{\"items\":" + jsonAsset.text + "}";
        var wrapper = JsonUtility.FromJson<SerializationWrapper<T>>(wrappedJson);
        if (wrapper?.items != null)
        {
            Debug.Log($"{typeof(T).Name} 데이터를 {wrapper.items.Count}개 로드했습니다.");
            return wrapper.items.ToDictionary(item => item.Id);
        }
        return new Dictionary<string, T>();
    }

    public void LoadSkillData(string jsonPath)
    {
        SkillDataList = LoadData<SkillData>(jsonPath);
    }

    public void LoadCharacterData(string jsonPath)
    {
        CharacterDataList = LoadData<CharacterData>(jsonPath);
    }

    public void LoadWeaponData(string jsonPath)
    {
        WeaponDataList = LoadData<WeaponData>(jsonPath);
    }

    public void LoadCostumeData(string jsonPath)
    {
        CostumeDataList = LoadData<CostumeData>(jsonPath);
    }

    public void LoadItemData(string jsonPath)
    {
        ItemDataList = LoadData<ItemData>(jsonPath);
    }

    public void LoadMonsterData(string jsonPath)
    {
        MonsterDataList = LoadData<MonsterData>(jsonPath);
    }
    public void LoadDialogueData(string jsonPath)
    {
        DialogueDataList = LoadData<DialogueData>(jsonPath);
    }
    public void LoadDialogueGroupData(string jsonPath)
    {
        DialogueGroupDataList = LoadData<DialogueGroupData>(jsonPath);
    }
    public void LoadFieldObjectData(string jsonPath)
    {
        FieldObjectDataList = LoadData<FieldObjectData>(jsonPath);
    }


    //==========================    아래는 사용을 위한 부분들을 메서드 정의  ====================================

    public CharacterData GetCharacterData(string id)
    {
        if (CharacterDataList == null || string.IsNullOrEmpty(id)) return null;

        return CharacterDataList.TryGetValue(id, out var item) ? item : null;
    }

    public SkillData GetSkill(string id)
    {
        if (SkillDataList == null || string.IsNullOrEmpty(id)) return null;

        return SkillDataList.TryGetValue(id, out var item) ? item : null;
    }

    public WeaponData GetWeaponData(string id)
    {
        if (WeaponDataList == null || string.IsNullOrEmpty(id)) return null;

        return WeaponDataList.TryGetValue(id, out var data) ? data : null;
    }

    public CostumeData GetCostumeData(string id)
    {
        if (CostumeDataList == null || string.IsNullOrEmpty(id)) return null;

        return CostumeDataList.TryGetValue(id, out var data) ? data : null;
    }

    public ItemData GetItemData(string id)
    {
        if (ItemDataList == null || string.IsNullOrEmpty(id)) return null;

        return ItemDataList.TryGetValue(id, out var data) ? data : null;
    }

    public DialogueData GetDialogueData(string dataId)
    {
        if (DialogueDataList == null || string.IsNullOrEmpty(dataId)) return null;

        return DialogueDataList.TryGetValue(dataId, out var data) ? data : null;
    }

    public DialogueGroupData GetDialogueGroupData(string dataId)
    {
        if (DialogueGroupDataList == null || string.IsNullOrEmpty(dataId)) return null;

        return DialogueGroupDataList.TryGetValue(dataId, out var data) ? data : null;
    }

    public MonsterData GetMonsterData(string dataId)
    {
        if (MonsterDataList == null || string.IsNullOrEmpty(dataId)) return null;

        return MonsterDataList.TryGetValue(dataId, out var data) ? data : null;
    }

    public FieldObjectData GetFieldObjectData(string dataId)
    {
        if (FieldObjectDataList == null || string.IsNullOrEmpty(dataId)) return null;

        return FieldObjectDataList.TryGetValue(dataId, out var data) ? data : null;
    }

}
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameUtil
{
    public static void LoadFullData()
    {
        GameDataManager.Instance.LoadSkillData(GetFullDataPath("Skill"));
        GameDataManager.Instance.LoadCharacterData(GetFullDataPath("Character"));
        GameDataManager.Instance.LoadWeaponData(GetFullDataPath("Weapon"));
        GameDataManager.Instance.LoadCostumeData(GetFullDataPath("Costume"));
        GameDataManager.Instance.LoadItemData(GetFullDataPath("Item"));
        GameDataManager.Instance.LoadMonsterData(GetFullDataPath("Monster"));
        GameDataManager.Instance.LoadDialogueData(GetFullDataPath("Dialogue"));
        GameDataManager.Instance.LoadDialogueGroupData(GetFullDataPath("DialogueGroup"));
        GameDataManager.Instance.LoadFieldObjectData(GetFullDataPath("FieldObject"));
    }

    public static string GetFullDataPath(string dataTableName)
    {
        if (string.IsNullOrEmpty(dataTableName))
        {
            Debug.Log("테이블 이름이 올바르지 않습니다!");
            return string.Empty;
        }

        //string relativePath = $"Assets/Resources/JsonData/{dataTableName}.json";
        //string fullPath = Path.GetFullPath(relativePath);
        //return fullPath;
        return $"JsonOutput/{dataTableName}";
    }


    public static int CalcCharacterFinalDamage(int curCharacterLevel, int levelPerDamage, bool isCritical)
    {
        int damagePerLevel = (curCharacterLevel + levelPerDamage);
        int finalDamage = isCritical ? (damagePerLevel * 2) : damagePerLevel;
        return finalDamage;
    }

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
}
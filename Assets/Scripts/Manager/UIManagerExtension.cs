using UnityEngine;

public enum UIRootType
{
    None = 0,
    BackGroundUI,
    MainUI,
    ContentUI,
    PopupUI,
    VeryFrontUI
}

public enum UIType
{
    PopupSample,
    LoadingUI,
    ScrollSample,
    InGameMenu,
    DialogueUI,
    StartLoginUI,
    SettingUI,
    QuitPopupUI,
    RobbyUI,
}

public static class UIManagerExtension
{
    public static string GetUIPath(this UIManager uiManager, UIRootType uiRootType, UIType uiType)
    {
        string path = string.Empty;

        path = $"UIPrefabs/{uiRootType}/{uiType}";
        return path;
    }

    public static void ShowStartupUIOnGameStart(this UIManager uiManager)
    {
        uiManager.OpenStartLoginUI();
    }

    // ------------------------------------------------------------------
    // veryfrontUI
    public static void OpenStartLoginUI(this UIManager uiManager)
    {
        var uiBase = uiManager.OpenVeryFrontUI(UIType.StartLoginUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseStartLoginUI(this UIManager uiManager)
    {
        uiManager.CloseVeryFrontUI( UIType.StartLoginUI);
    }

    public static UIBase OpenLoadingUI(this UIManager uiManager)
    {
        var uiBase = uiManager.OpenVeryFrontUI(UIType.LoadingUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return null;
        }
        return uiBase;
    }
    public static void CloseLoadingUI(this UIManager uiManager)
    {
        uiManager.CloseVeryFrontUI(UIType.LoadingUI);
    }

    // ------------------------------------------------------------------
    // popupUI
    public static void OpenQuitPopup(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenPopupUI(UIType.QuitPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseQuitPopup(this UIManager uiManager)
    {
        uiManager.ClosePopupUI(UIType.QuitPopupUI);
    }

    public static void OpenSettingUI(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenPopupUI(UIType.SettingUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void OpenPopupSample(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenPopupUI(UIType.PopupSample);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void ClosePopupSampleUI(this UIManager uiManager)
    {
        uiManager.ClosePopupUI(UIType.PopupSample);
    }

    // ------------------------------------------------------------------
    // mainUI
    public static void OpenRobbyUI(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenMainUI(UIType.RobbyUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    // ------------------------------------------------------------------
    // contentUI
    public static void OpenInGameMenu(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenContentUI(UIType.InGameMenu);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void OpenDialogueUI(this UIManager uiManager, string startDialogueId)
    {
        var uiBase = uiManager.OpenContentUI(UIType.DialogueUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

        if (uiBase is DaniTech_DialogueUI dialogueUi)
        {
            dialogueUi.StartDialogue(startDialogueId);
        }
    }

    // ------------------------------------------------------------------
    // BackgroundUI

}
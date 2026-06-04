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
    ScrollSample,

    StartLoginUI,
    LoadingUI,

    MenuUI,
    DialogueUI,
    
    SettingUI,
    QuitPopUI,
    
    EnterGamePopUI,
    ClearGamePopUI,
    RobbyUI,
    ChapterScUI,
    StoryScUI,
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
    public static void OpenQuitPopUI(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenPopupUI(UIType.QuitPopUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseQuitPopUI(this UIManager uiManager)
    {
        uiManager.ClosePopupUI(UIType.QuitPopUI);
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
    public static void CloseSettingUI(this UIManager uiManager)
    {
        uiManager.ClosePopupUI(UIType.SettingUI);
    }


    public static void OpenEnterGamePopUI(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenPopupUI(UIType.EnterGamePopUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseEnterGamePopUI(this UIManager uiManager)
    {
        uiManager.ClosePopupUI(UIType.EnterGamePopUI);
    }


    public static void OpenClearGamePopUI(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenPopupUI(UIType.ClearGamePopUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseClearGamePopUI(this UIManager uiManager)
    {
        uiManager.ClosePopupUI(UIType.ClearGamePopUI);
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
    public static void CloseRobbyUI(this UIManager uiManager)
    {
        uiManager.CloseMainUI(UIType.RobbyUI);
    }


    public static void OpenChapterScUI(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenMainUI(UIType.ChapterScUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseChapterScUI(this UIManager uiManager)
    {
        uiManager.CloseMainUI(UIType.ChapterScUI);
    }


    public static void OpenStoryScUI(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenMainUI(UIType.StoryScUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseStoryScUI(this UIManager uiManager)
    {
        uiManager.CloseMainUI(UIType.StoryScUI);
    }

    // ------------------------------------------------------------------
    // contentUI
    public static void OpenInGameMenu(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenContentUI(UIType.MenuUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseMenuUI(this UIManager uiManager)
    {
        uiManager.CloseContentUI(UIType.MenuUI);
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
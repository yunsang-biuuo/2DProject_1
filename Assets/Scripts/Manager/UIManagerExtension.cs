using UnityEngine;

public enum UIRootType
{
    None = 0,
    BackgroundUI,
    MainUI,
    ContentUI,
    PopupUI,
    VeryFrontUI
}

public enum UIType
{
    PopupSample,
    LoadingSample,
    ScrollSample,
    Layout_Top,
    DialogueUI,
    StartLoginUI
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

    // ==============================================================================

    public static void OpenStartLoginUI(this UIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(UIRootType.VeryFrontUI, UIType.StartLoginUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseStartLoginUI(this UIManager uiManager)
    {
        uiManager.CloseUI(UIRootType.VeryFrontUI, UIType.StartLoginUI);
    }

    public static void OpenLayout_Top(this UIManager uiManger)
    {
        var uiBase = uiManger.OpenMainUI(UIType.Layout_Top);
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
        uiManager.CloseUI(UIRootType.PopupUI, UIType.PopupSample);
    }

    public static void OpenLoadingUI(this UIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(UIRootType.VeryFrontUI, UIType.LoadingSample);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseLoadingUI(this UIManager uiManager)
    {
        uiManager.CloseUI(UIRootType.VeryFrontUI, UIType.LoadingSample);
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

}
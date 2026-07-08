using UnityEngine;
using UnityEngine.UI;

public class DaniTech_MVVMTestUI : UIBase
{
    [SerializeField] private UIButton Button_GiveExp;
    [SerializeField] private InputField InputField_ChangeName;

    private void Awake()
    {
        Button_GiveExp.BindOnClickButtonEvent(OnClick_GiveExp);
        InputField_ChangeName.onSubmit.AddListener(OnSubmit_ChangeName);
    }

    private void OnClick_GiveExp()
    {
        NetworkManager.Inst.LocalPlayerService.RequestGiveExpToLocalPlayer(30);
    }

    private void OnSubmit_ChangeName(string newName)
    {
        NetworkManager.Inst.LocalPlayerService.RequestChangePlayerName(newName);
    }
}
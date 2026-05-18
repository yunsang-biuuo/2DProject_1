using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSample : MonoBehaviour
{
    [SerializeField] private Button btn_CreateSlot;
    [SerializeField] private Button btn_Delete;

    [SerializeField] private Transform content;
    [SerializeField] private GameObject slotBoxPrefab;

    [SerializeField] private GameObject scrollSample;

    private int _generatedKey = 0;
    private Dictionary<int, ScrollBtnSlot> _slotList = new Dictionary<int, ScrollBtnSlot>();

    void Start()
    {
        btn_CreateSlot.onClick.AddListener(CreateSlot);
        btn_Delete.onClick.AddListener(() => scrollSample.SetActive(false));
    }

    void CreateSlot()
    {
        GameObject obj = Instantiate(slotBoxPrefab, content, false);
        if (obj == null) return;

        ScrollBtnSlot slot = obj.GetComponentInChildren<ScrollBtnSlot>();
        if (slot == null) return;

        _generatedKey++;

        obj.name = $"Slot_{_generatedKey}";

        slot.Setup(null, $"Weapon {_generatedKey}");

        _slotList.Add(_generatedKey, slot);
    }

}

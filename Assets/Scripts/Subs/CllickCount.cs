using UnityEngine;

public class CllickCount : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            GameManager.Instance.AddAttackCount();
        }

        if (Input.GetKeyDown(KeyCode.S))
            GameManager.Instance.SaveData();
    }
}

using UnityEngine;

public class Monster : MonoBehaviour
{
    Transform Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(Player);
    }
}

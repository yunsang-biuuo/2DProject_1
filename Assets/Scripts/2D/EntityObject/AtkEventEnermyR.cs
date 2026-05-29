using UnityEngine;

public class AtkEventEnermyR : MonoBehaviour
{
    private EnermyCloseR _monster;
    void Start() => _monster = GetComponentInParent<EnermyCloseR>();

    public void OnEnemyAttackHit() => _monster.OnEnemyAttackHit();
}
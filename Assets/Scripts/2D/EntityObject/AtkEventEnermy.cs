using UnityEngine;

public class AtkEventEnermy : MonoBehaviour
{
    private EnermyClose _monster;
    void Start() => _monster = GetComponentInParent<EnermyClose>();

    public void OnEnemyAttackHit() => _monster.OnEnemyAttackHit();
}

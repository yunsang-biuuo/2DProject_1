using UnityEngine;

public class AtkEventPlayer : MonoBehaviour
{
    private CyborgPlayer _player;
    void Start() => _player = GetComponentInParent<CyborgPlayer>();
    public void OnAttackHit() => _player.OnAttackHit();
}

using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    private BasicMovePlayer _player;
    void Start() => _player = GetComponentInParent<BasicMovePlayer>();
    public void OnAttackHit() => _player.OnAttackHit();
}

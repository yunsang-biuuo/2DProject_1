using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    private BasicPlayer _player;
    void Start() => _player = GetComponentInParent<BasicPlayer>();
    public void OnAttackHit() => _player.OnAttackHit();
}

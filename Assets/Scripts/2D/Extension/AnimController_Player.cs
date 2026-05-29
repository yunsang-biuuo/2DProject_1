using UnityEngine;

public enum StatePlayer
{
    Idle,
    Walk,
    Run,
    Jump,
    Attack,
    Damaged,
    Dead
}

public class AnimController_Player : MonoBehaviour
{
    [SerializeField] private Animator Animator_Player;

    private StatePlayer _currentState;

    public void SetState(StatePlayer newState)
    {
        if (newState == StatePlayer.Idle && _currentState == StatePlayer.Idle)
        {
            return;
        }

        _currentState = newState;
        switch (_currentState)
        {
            case StatePlayer.Walk:
                Animator_Player.SetBool("IsWalking", true);
                break;
            case StatePlayer.Jump:
                Animator_Player.SetBool("IsJumping", true);
                break;
            case StatePlayer.Run:
                Animator_Player.SetBool("IsRuning", true);
                break;
            case StatePlayer.Attack:
                Animator_Player.SetTrigger("Attack");
                break;
            case StatePlayer.Damaged:
                Animator_Player.SetTrigger("IsDamaged");
                break;
            case StatePlayer.Dead:
                Animator_Player.SetBool("IsDead", true);
                break;
            case StatePlayer.Idle:
                ResetAllParameters();
                break;
        }
    }

    private void ResetAllParameters()
    {
        Animator_Player.SetBool("IsMoving", false);
        Animator_Player.SetBool("IsRuning", false);
        Animator_Player.SetBool("IsDead", false);
    }
}

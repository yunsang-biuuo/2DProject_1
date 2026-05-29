using UnityEngine;

public enum StateEnermy
{
    Idle,
    Walk,
    Attack,
    Damaged,
    Dead
}

public class AnimController_Enermy : MonoBehaviour
{
    [SerializeField] private Animator Animator_Enermy;

    private StateEnermy _currentState;

    public void SetState(StateEnermy newState)
    {
        if (newState == StateEnermy.Idle && _currentState == StateEnermy.Idle)
        {
            return;
        }

        _currentState = newState;
        switch (_currentState)
        {
            case StateEnermy.Walk:
                Animator_Enermy.SetBool("IsWalking", true);
                break;
            case StateEnermy.Attack:
                Animator_Enermy.SetTrigger("Attack");
                break;
            case StateEnermy.Dead:
                Animator_Enermy.SetBool("IsDead", true);
                break;
            case StateEnermy.Damaged:
                Animator_Enermy.SetBool("IsDamaged", true);
                break;
            case StateEnermy.Idle:
                ResetAllParameters();
                break;
        }
    }

    private void ResetAllParameters()
    {
        Animator_Enermy.SetBool("IsWalking", false);
        Animator_Enermy.SetBool("IsDead", false);
        Animator_Enermy.SetBool("IsDamaged", false);
    }
}
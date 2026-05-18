using UnityEngine;

public enum StateMonster
{
    Idle,
    Roam,
    Attack,
    Damaged,
    Dead
}

public class AnimController_Monster : MonoBehaviour
{
    [SerializeField] private Animator Animator_Monster;

    private StateMonster _currentState;

    public void SetState(StateMonster newState)
    {
        if (newState == StateMonster.Idle && _currentState == StateMonster.Idle)
        {
            return;
        }

        _currentState = newState;
        switch (_currentState)
        {
            case StateMonster.Roam:
                Animator_Monster.SetBool("IsRoaming", true);
                break;
            case StateMonster.Attack:
                Animator_Monster.SetTrigger("DoAttack");
                break;
            case StateMonster.Dead:
                Animator_Monster.SetBool("IsDead", true);
                break;
            case StateMonster.Damaged:
                Animator_Monster.SetBool("IsDamaged", true);
                break;
            case StateMonster.Idle:
                ResetAllParameters();
                break;
        }
    }

    private void ResetAllParameters()
    {
        Animator_Monster.SetBool("IsRoaming", false);
        Animator_Monster.SetBool("IsDead", false);
        Animator_Monster.SetBool("IsDamaged", false);
    }
}
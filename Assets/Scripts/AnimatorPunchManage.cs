using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPunchManage : StateMachineBehaviour
{
    enum PunchTypes {
        LEFT = 1,
        RIGHT = 2,
        MUTANT = 3,
        ZOMBIE = 4,
        MMA_KICK01 = 5,
        MMA_KICK02 = 6,
        NUMBER_OF_TYPES = 7
    };

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PunchTypes nextState = (PunchTypes) Random.Range(1, (int)PunchTypes.NUMBER_OF_TYPES);

        animator.SetInteger("PunchType", (int)nextState);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // 
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

}

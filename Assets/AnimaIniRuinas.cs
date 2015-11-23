using UnityEngine;
using System.Collections;

public class AnimaIniRuinas : StateMachineBehaviour
{

    public Animator Eran;
    float speed = 1.5f;
    public RuinasLevelScript Script;
    CanvasGroup Cav; //Canvas
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Eran = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Eran2Sprites").GetComponent<Animator>();
        Script = GameObject.Find("DungeonScript").GetComponent<RuinasLevelScript>();
        Cav = GameObject.Find("HUD(Canvas)").GetComponent<CanvasGroup>();
        Cav.alpha = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Eran.SetTrigger("Up");
        Eran.SetFloat("Speed", speed);
        if (speed > 0)
            speed -= 0.01f;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Cav.alpha = 1;
        Script.wait = false;

        Debug.Log("Saiu");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}

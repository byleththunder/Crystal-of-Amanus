using UnityEngine;
using System.Collections;

public class EfeitoAtaque : StateMachineBehaviour {

    public ParticleSystem CrescentEffect;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CrescentEffect = GameObject.Find("CrescentMoon").GetComponent<ParticleSystem>();
        switch (GameObject.FindObjectOfType<Character>().visao)
        {
            case TargetVision.Back:
                CrescentEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
                CrescentEffect.startRotation = 0;
                break;
            case TargetVision.Front:
                CrescentEffect.transform.localRotation = Quaternion.Euler(0, 180, 0);
                CrescentEffect.startRotation = Mathf.Deg2Rad * 180f;
                break;
            case TargetVision.Left:
                CrescentEffect.transform.localRotation = Quaternion.Euler(0, -90, 0);
                CrescentEffect.startRotation = Mathf.Deg2Rad * -90f;
                break;
            case TargetVision.Right:
                CrescentEffect.transform.localRotation = Quaternion.Euler(0, 90, 0);
                CrescentEffect.startRotation = Mathf.Deg2Rad * 90f;
                break;
        }
        CrescentEffect.Emit(1);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

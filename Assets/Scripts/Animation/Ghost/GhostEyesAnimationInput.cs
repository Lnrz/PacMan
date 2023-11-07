using UnityEngine;

public class GhostEyesAnimationInput : StateMachineBehaviour
{
    private AbstractMovingEntity ame;
    private Animator anim;
    private bool firstTime = true;
    private int[] hashes = new int[4];

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (firstTime)
        {
            firstTime = false;
            anim = animator;
            ame = animator.GetComponent<AbstractMovingEntity>();
            hashes[0] = Animator.StringToHash("LookUp");
            hashes[1] = Animator.StringToHash("LookRight");
            hashes[2] = Animator.StringToHash("LookDown");
            hashes[3] = Animator.StringToHash("LookLeft");
        }
        ame.AddDirectionListener(OnDirectionChange);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ame.RemoveDirectionListener(OnDirectionChange);
    }

    private void OnDirectionChange(int newDirectionIndex)
    {
        if (newDirectionIndex != -1)
        {
            anim.SetTrigger(hashes[newDirectionIndex]);
        }
    }
}
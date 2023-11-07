using UnityEngine;

public class RestoreEyesAnimation : StateMachineBehaviour
{
    private AbstractMovingEntity ame;
    private bool firstTime = true;
    private int[] hashes = new int[4];

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (firstTime)
        {
            firstTime = false;
            ame = animator.GetComponent<AbstractMovingEntity>();
            hashes[0] = Animator.StringToHash("LookUp");
            hashes[1] = Animator.StringToHash("LookRight");
            hashes[2] = Animator.StringToHash("LookDown");
            hashes[3] = Animator.StringToHash("LookLeft");
        }
        int dirIndex;

        dirIndex = ame.GetDirectionIndex();
        if (dirIndex == -1)
        {
            dirIndex = 1;
        }
        animator.SetTrigger(hashes[dirIndex]);
    }
}
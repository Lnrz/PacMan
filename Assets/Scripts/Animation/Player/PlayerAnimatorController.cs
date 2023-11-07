using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator anim;
    private int[] goingHashes = new int[4];
    private int movingHash;

    private void Awake()
    {
        AbstractMovingEntity ame;

        anim = GetComponent<Animator>();
        goingHashes[0] = Animator.StringToHash("GoingUp");
        goingHashes[1] = Animator.StringToHash("GoingRight");
        goingHashes[2] = Animator.StringToHash("GoingDown");
        goingHashes[3] = Animator.StringToHash("GoingLeft");
        movingHash = Animator.StringToHash("IsMoving");
        ame = GetComponent<AbstractMovingEntity>();
        ame.AddDirectionListener(OnDirectionChange);
    }

    private void OnDirectionChange(int dirIndex)
    {
        if (dirIndex == -1)
        {
            anim.SetBool(movingHash, false);
        }
        else
        {
            anim.SetBool(movingHash, true);
            anim.SetTrigger(goingHashes[dirIndex]);
        }
    }
}
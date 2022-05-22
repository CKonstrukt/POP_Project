using UnityEngine;

public class AnimForkButter : MonoBehaviour
{
    public Animator animatorFork;
    public Animator animatorButter;


    private void OnEnable() {
        animatorButter.SetBool("spawn", true);
        animatorFork.SetBool("spawn", true);
    }
}

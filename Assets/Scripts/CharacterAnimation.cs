using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Anim;
    void awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void Walk(bool walk)
    {
        Anim.SetBool(AnimationTags.WALK_PAR, walk);
        // print("walking");
    }
    public void Defend(bool defend)
    {
        Anim.SetBool(AnimationTags.DEFEND_PAR, defend);
    }
    public void Attack_1()
    {
        Anim.SetTrigger(AnimationTags.ATTACK1_PAR);
    }
    public void Attack_2()
    {
        Anim.SetTrigger(AnimationTags.ATTACK2_PAR);
    }

    public void FreezeAnimation(){
        Anim.speed = 0f;
    }
    public void UnFreezeAnimation(){
        Anim.speed = 1f;
    }

}

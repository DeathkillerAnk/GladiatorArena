using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
{
    public GameObject attackPoint;

    private CharacterAnimation playerAnimation;
    public PlayerShield shield;
    private CharacterSoundFX soundFX;
    // Start is called before the first frame update
    void Awake()
    {
        playerAnimation = GetComponent<CharacterAnimation>();
        shield = GetComponent<PlayerShield>();
        soundFX = GetComponentInChildren<CharacterSoundFX>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            playerAnimation.Defend(true);
            shield.ActivateShield(true);
        }

        if(Input.GetKeyUp(KeyCode.J)){
            playerAnimation.UnFreezeAnimation();
            playerAnimation.Defend(false);
            shield.ActivateShield(false);
        }

        if(Input.GetKeyDown(KeyCode.K)){
            if(Random.Range(0,2) > 0 ){
                playerAnimation.Attack_1();
                soundFX.Attack1();
            } else {
                playerAnimation.Attack_2();
                soundFX.Attack2();
            }
            
        }    
    }
    
    void ActivateAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void DeactivateAttackPoint()
    {
        if(attackPoint.activeInHierarchy){
            attackPoint.SetActive(false);
        }
    }

}

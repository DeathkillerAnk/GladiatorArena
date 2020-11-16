using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFX : MonoBehaviour
{
    private AudioSource soundFX;

    [SerializeField]
    private AudioClip attackSound1, attackSound2, dieSound;
    // Start is called before the first frame update
    void Awake()
    {
        soundFX = GetComponent<AudioSource>();
        soundFX.Play();
    }

    public void Attack1()
    {
        soundFX.clip = attackSound1;
        soundFX.Play();
    }

    public void Attack2()
    {
        soundFX.clip = attackSound2;
        soundFX.Play();
    }

    public void Die()
    {
        soundFX.clip = dieSound;
        soundFX.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

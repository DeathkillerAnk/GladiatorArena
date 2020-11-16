using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;
    public bool isPlayer;

    [HideInInspector]
    public bool shieldActivated;

    private float xDeath = -90f;
    private float deathSmooth = 0.9f;
    private float rotateTime = 0.23f;
    private bool playerDied;
    
    private CharacterSoundFX soundFX;

    [SerializeField]
    private Image health_UI;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    void Awake()
    {
        soundFX = GetComponentInChildren<CharacterSoundFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerDied){
            RotateAfterDeath();
        }
    }

    public void ApplyDamage(float damage)
    {
        if(shieldActivated){
            return;
        }

        health -= damage; 
        if(health_UI != null){
            health_UI.fillAmount = health/100f;
        }
        // print(health);
        if(health <= 0){

            soundFX.Die();
            print("dead");

            GetComponent<Animator>().enabled = false;

            StartCoroutine(AllowRotate());

            if(isPlayer){
                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerAttackInput>().enabled = false;

                Camera.main.transform.SetParent(null);

                GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG).GetComponent<EnemyController>().enabled = false;
            } else {
                GetComponent<EnemyController>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }

    void RotateAfterDeath()
    {
        transform.eulerAngles = new Vector3(
            Mathf.Lerp(transform.eulerAngles.x, xDeath, Time.deltaTime * deathSmooth), transform.eulerAngles.y, transform.eulerAngles.z);
    }

    IEnumerator AllowRotate()
    {
        playerDied = true;
        yield return new WaitForSeconds(rotateTime);
        playerDied = false;
    }
}

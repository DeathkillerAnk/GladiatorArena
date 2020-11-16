using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState{
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{

    private CharacterAnimation enemyAnim;
    private NavMeshAgent navAgent;

    private Transform playerTarget;
    public GameObject attackPoint;
    public float movementSpeed = 3.5f;
    
    public float attackDistance = 1.5f;
    public float chasePlayerAfterAttackDistance = 1f;

    private float waitBeforeAttackTime = 3f;
    private float attackTimer;

    private EnemyState enemyState;
     private CharacterSoundFX soundFX;
    // Start is called before the first frame update
    void Awake()
    {
        enemyAnim = GetComponent<CharacterAnimation>();
        navAgent = GetComponent<NavMeshAgent>();

        soundFX = GetComponentInChildren<CharacterSoundFX>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Start()
    { 
        enemyState = EnemyState.CHASE;
        attackTimer = waitBeforeAttackTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyState.CHASE){
            ChasePlayer();
        }
        if(enemyState == EnemyState.ATTACK){
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(playerTarget.position);
        navAgent.speed = movementSpeed;

        if(navAgent.velocity.sqrMagnitude == 0){
            enemyAnim.Walk(false);
        } else {
            enemyAnim.Walk(true);
        }

        if(Vector3.Distance(transform.position, playerTarget.position) <= attackDistance){
            enemyState = EnemyState.ATTACK;
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        enemyAnim.Walk(false);

        attackTimer += Time.deltaTime;

        if(attackTimer > waitBeforeAttackTime){
            if(Random.Range(0,2) > 0 ){
                enemyAnim.Attack_1();
                soundFX.Attack1();
            } else {
                enemyAnim.Attack_2();
                soundFX.Attack2();
            }
            attackTimer = 0f;
        }

        if(Vector3.Distance(transform.position, playerTarget.position) > attackDistance + chasePlayerAfterAttackDistance){
            navAgent.isStopped = false;
            enemyState = EnemyState.CHASE;
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

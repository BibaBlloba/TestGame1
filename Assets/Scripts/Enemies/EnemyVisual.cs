using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyEntity))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyVisual : MonoBehaviour
{
    private const string ATTACK_TRIGGER = "Attack";
    
    [SerializeField] private EnemyEntity _enemyEntity;
    [SerializeField] private EnemyAI _enemyAI;
    
    private Animator animator;
    private NavMeshAgent agent;
    private Vector2 lastDirection;
    
    private const string IS_ROAMING_ANIM_PARAM = "IsRoaming";
    private const string XInput_ANIM_PARAM = "XInput";
    private const string YInput_ANIM_PARAM = "YInput";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 velocity = agent.velocity;
        Vector2 normalizedVelocity = velocity.normalized;

        if (normalizedVelocity != Vector2.zero)
        {
            lastDirection = normalizedVelocity;
            
            animator.SetBool(IS_ROAMING_ANIM_PARAM, true);
            animator.SetFloat(XInput_ANIM_PARAM, velocity.x);
            animator.SetFloat(YInput_ANIM_PARAM, velocity.y);
        }
        else
        {
            animator.SetBool(IS_ROAMING_ANIM_PARAM, false);
            animator.SetFloat(XInput_ANIM_PARAM, lastDirection.x);
            animator.SetFloat(YInput_ANIM_PARAM, lastDirection.y);
        }
    }
    
    private void Start()
    {
        _enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
    }

    private void OnDestroy()
    {
        _enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
    }
    
    private void _enemyAI_OnEnemyAttack(object sender, EventArgs e)
    {
        Debug.Log("ataka");
        animator.SetTrigger(ATTACK_TRIGGER);
    }
    
    public void AttackColliderTurnOff()
    {
        _enemyEntity.AttackColliderTurnOff();
    }
    
    public void AttackColliderTurnOn()
    {
        _enemyEntity.AttackColliderTurnOn();
    }
    
}

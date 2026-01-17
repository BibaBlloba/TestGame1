using UnityEngine;
using UnityEngine.AI;

public class EnemyVisual : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    
    private const string IS_ROAMING_ANIM_PARAM = "IsRoaming";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool(IS_ROAMING_ANIM_PARAM, isMoving);
    }
    
}

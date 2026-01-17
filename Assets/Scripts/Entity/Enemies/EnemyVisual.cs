using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyVisual : MonoBehaviour
{
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
    
}

using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;
using Utils = Game.Utils.Utils;

[RequireComponent(typeof(EnemyAI))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 2f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private float chasingDistanceStart = 2f;
    [SerializeField] private float chasingDistanceMax = 10f;
    [SerializeField] private bool isChasingEnemy = true;
    [SerializeField] private bool isAttackingEnemy = true;
    SerializeField] private float speedMultiplier = 1f;

    private NavMeshAgent agent;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startPosition;
    private State _currentState;
    private float _attackingDistance = 2f;
    private float _attackRate = 2f;
    private float _nextAttackTime = 0f;

    public event EventHandler OnEnemyAttack;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = agent.speed * speedMultiplier;
        _currentState = startingState;
    }

    private void Update()
    {
        CheckCurrentState();
        switch (_currentState)
        {
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime <= 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
            case State.Chasing:
                ChasingTarget();
                break;
            case State.Attacking:
                AttackTarget();
                break;
            default:
            case State.Idle:
                break;
        }
    }

    private void AttackTarget()
    {
        if (Time.time > _nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            _nextAttackTime = Time.time + _attackRate;
        }
    }

    private void ChasingTarget()
    {
        agent.stoppingDistance = 1.5f;
        agent.SetDestination(Player.Instance.transform.position);
    }

    private void CheckCurrentState()
    {
        if (!Player.Instance)
            _currentState = State.Roaming;
        
        State newState = startingState;
        
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (isChasingEnemy)
        {
            if (distanceToPlayer <= chasingDistanceStart)
                newState = State.Chasing;
        }

        if (isAttackingEnemy)
        {
            if (distanceToPlayer <= _attackingDistance)
            {
                newState = State.Attacking;
            }
        }

        _currentState = newState;
    }

    private void Roaming()
    {
        startPosition = transform.position;
        roamPosition = GetRoamingPosition();
        ChangeFacingDirection(startPosition, roamPosition);
        agent.SetDestination(roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + Utils.GetRandomPosition() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 startingPoint, Vector3 targetPoint)
    {
        if (startingPoint.x > targetPoint.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    private enum State 
    {
        Roaming,
        Chasing,
        Attacking,
        Death,
        Idle
    }

    private void OnDrawGizmos()
    {
        switch (_currentState) 
        {
            case State.Chasing:
                if (Player.Instance)
                    Gizmos.DrawLine(transform.position, Player.Instance.transform.position);
                Gizmos.color = new Color(1, 0, 0, 0.2f);
                break;
            default:
                Gizmos.color = new Color(0, 1, 0, 0.2f);
                break;
        }
        
        Gizmos.DrawWireSphere(transform.position, chasingDistanceStart);
    }

}

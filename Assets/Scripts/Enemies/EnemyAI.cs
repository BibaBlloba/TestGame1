using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;
using Utils = Game.Utils.Utils;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 2f;
    [SerializeField] private float roamingTimerMax = 2f;

    private NavMeshAgent agent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        state = startingState;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime <= 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
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
        Roaming
    }

}

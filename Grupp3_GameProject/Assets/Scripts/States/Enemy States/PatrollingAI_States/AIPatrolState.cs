
using UnityEngine;

//Author: Molly R?le

[CreateAssetMenu()]
public class AIPatrolState : AI_BaseState
{
    [SerializeField]
    private float sightDistance;

    [SerializeField]
    private float speed;

    private Transform currentPatrol;

    public override void Enter()
    {
        currentPatrol = playerPos;
        navAgent.SetDestination(currentPatrol.position);
        navAgent.speed = speed;
    }
    public override void RunUpdate()
    {
        float dist = Vector3.Distance(enemy.transform.position, playerPos.position);
        if (!Physics.Linecast(enemy.transform.position, playerPos.position, collisionLayer) && dist <= sightDistance)//kolla ?ven n?gon sight-distance
        {
            stateMachine.ChangeState<AIChasePlayerState>();
        }
        if (navAgent.remainingDistance < 2.0f)
        {
            currentPatrol = enemy.GetPatrolPoint();
            navAgent.SetDestination(currentPatrol.position);
        }
    }
}
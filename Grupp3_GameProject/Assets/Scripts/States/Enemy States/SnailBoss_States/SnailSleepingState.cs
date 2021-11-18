using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Molly Röle

[CreateAssetMenu()]
public class SnailSleepingState : State
{
    private SnailBoss snailBoss;

    protected override void Initialize()
    {
        snailBoss = (SnailBoss)owner;
        Debug.Assert(snailBoss);
    }
    public override void Enter()
    {
        snailBoss.GetBody().SetActive(false);
        snailBoss.SetVulnerable(false);
        Debug.Log("Sleeping state entered!");
    }
    public override void RunUpdate()
    {
        if (snailBoss.IsAwake())
        {
            stateMachine.ChangeState<SnailHeadbuttAtkState>(); //when not testing stuff, the target state should be SproutAtkState
        }
    }
}

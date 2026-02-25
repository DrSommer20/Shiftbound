using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        player.RB.linearVelocity = new Vector2(0f, player.RB.linearVelocity.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (player.InputX != 0f)
        {
            stateMachine.ChangeState(player.RunState); 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
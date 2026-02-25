using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Später hier: player.Anim.Play("Run");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.InputX == 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.RB.linearVelocity = new Vector2(player.InputX * player.moveSpeed, player.RB.linearVelocity.y);

        player.CheckIfShouldFlip(player.InputX);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
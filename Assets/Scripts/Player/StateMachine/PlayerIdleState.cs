using UnityEngine;

namespace Assets.Scripts.Player.StateMachine
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.RB.linearVelocity = new Vector2(0f, player.RB.linearVelocity.y);
            player.Anim.Play("idle");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
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
}
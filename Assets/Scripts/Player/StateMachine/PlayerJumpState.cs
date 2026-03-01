using UnityEngine;

namespace Assets.Scripts.Player.StateMachine
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.RB.linearVelocity = new Vector2(player.RB.linearVelocity.x, player.jumpForce);

            player.ConsumeJump();

            player.Anim.Play("jump");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            player.RB.linearVelocity = new Vector2(player.InputX * player.moveSpeed, player.RB.linearVelocity.y);
            player.CheckIfShouldFlip(player.InputX);
        }
    }
}
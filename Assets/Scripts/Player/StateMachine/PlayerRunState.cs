using UnityEngine;

namespace Assets.Scripts.Player.StateMachine
{
    public class PlayerRunState : PlayerState
    {
        public PlayerRunState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.Anim.Play("running");
        }

        public override void LogicUpdate() //TODO: Implement Wall Check
        {
            base.LogicUpdate();
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
}
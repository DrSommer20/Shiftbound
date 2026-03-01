using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player.StateMachine
{
	public class PlayerFallState: PlayerState
	{
        public PlayerFallState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.Anim.Play("fall");
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
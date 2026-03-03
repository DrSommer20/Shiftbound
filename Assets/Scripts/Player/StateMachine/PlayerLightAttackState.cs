using UnityEngine;

namespace Assets.Scripts.Player.StateMachine
{
	public class PlayerLightAttackState: PlayerState
	{
        public PlayerLightAttackState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.Anim.Play("lightStrike");
            player.RB.linearVelocity = Vector2.zero;
            IsFinished = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !player.Anim.IsInTransition(0))
            {
                IsFinished = true;
            }
        }

        public override bool CanBeInterrupted()
        {
            return false;
        }
    }
}

using UnityEngine;

namespace Assets.Scripts.Player.StateMachine
{
    public class PlayerDashState : PlayerState
    {
        private float dashStartTime;

        public PlayerDashState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.ConsumeJump();  // um einen Jump bei leichtem abheben zu verhindern

            player.RB.gravityScale = 0f;

            dashStartTime = Time.time;

            float dashDirection = player.FacingRight ? 1f : -1f;
            player.RB.linearVelocity = new Vector2(dashDirection * player.dashSpeed, 0f);

            player.Anim.Play("dash");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            if (Time.time >= dashStartTime + player.dashDuration)
            {
                IsFinished = true;
                Exit();
            }
        }

        public override bool CanBeInterrupted()
        {
            return false;
        }

        public override void Exit()
        {
            base.Exit();

            player.RB.gravityScale = player.DefaultGravity;
            player.ResetDashCooldown();
            player.RB.linearVelocity = new Vector2(player.RB.linearVelocity.x * 0.5f, player.RB.linearVelocity.y);
        }
    }
}
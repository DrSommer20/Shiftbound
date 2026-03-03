using UnityEngine;

namespace Assets.Scripts.Player.StateMachine
{
    public abstract class PlayerState
    {
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        public bool IsFinished { get; protected set; }

        // Konstruktor
        public PlayerState(PlayerController player, PlayerStateMachine stateMachine)
        {
            this.player = player;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            IsFinished = false;
        }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
        public virtual bool CanBeInterrupted()
        {
            return true;
        }

        public virtual bool IsAllowedToChangeFromState(PlayerState state)
        {
            return true; 
        }
    }
}
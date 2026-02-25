public abstract class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;

    // Konstruktor
    public PlayerState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    // Wird 1x beim Betreten des States aufgerufen
    public virtual void Enter() { }

    // Wird jeden Frame aufgerufen (für Input & Timer)
    public virtual void LogicUpdate() { }

    // Wird jeden FixedUpdate aufgerufen (für Rigidbody-Physik)
    public virtual void PhysicsUpdate() { }

    // Wird 1x beim Verlassen des States aufgerufen
    public virtual void Exit() { }
}
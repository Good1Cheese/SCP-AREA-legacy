using Zenject;

public class RunController : MoveController
{
    [Inject] readonly PlayerStamina m_playerStamina;

    public override float GetMove()
    {
        if (!m_playerStamina.HasPlayerStamina) { return 0; }

        return base.GetMove();
    }
}

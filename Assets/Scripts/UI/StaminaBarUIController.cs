public class StaminaBarUIController : StatisticsBarUIController
{
    public override float GetBarValue()
    {
        return MainLinks.Instance.PlayerStamina.StaminaValue;
    }
}

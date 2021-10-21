using Zenject;

public class HealableCellSaving : DataSaving
{
    [Inject] readonly PlayerHealth m_playerHealth;

    AutoHealableHealthCell m_autoHealableHealthCell;

    public float healProgress;
    public bool ishealContinueable;

    void Start()
    {
        m_autoHealableHealthCell = m_playerHealth.HealthCells.GetFirstFilledCell() as AutoHealableHealthCell;
    }

    public override void Save()
    {
        ishealContinueable = m_autoHealableHealthCell.HealthCellHealEffect.IsHealContinueable;
        healProgress = m_autoHealableHealthCell.Slider.value;
    }

    public override void LoadData()
    {
        if (!m_playerHealth.HealthCells.IsCurrentCellLast() || !ishealContinueable) { return; }

        m_autoHealableHealthCell.Slider.value = healProgress;
        m_autoHealableHealthCell.HealthCellHealEffect.PlayHeal();
    }



}

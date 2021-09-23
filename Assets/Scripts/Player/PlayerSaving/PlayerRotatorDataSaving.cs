
using Zenject;

public class PlayerRotatorDataSaving : DataSaving
{
    [Inject] readonly PlayerRotator m_playerRotator;

    public float m_yRotation;
    public float m_xRotation;

    public override void Save()
    {
        m_yRotation = m_playerRotator.YRotation;
        m_xRotation = m_playerRotator.XRotation;
    }

    public override void Load()
    {
        m_playerRotator.XRotation = m_xRotation;
        m_playerRotator.YRotation = m_yRotation;
    }

}
using UnityEngine;
using Zenject;

public class HealthCellsGetter : MonoBehaviour
{
    [Inject] readonly PlayerHealth m_playerHealth;

    void Awake()
    {
        m_playerHealth.Cells.AddRange(GetComponentsInChildren<HealthCell>());    
    }
}

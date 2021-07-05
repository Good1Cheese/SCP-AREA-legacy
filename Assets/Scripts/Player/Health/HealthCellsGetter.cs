using UnityEngine;
using Zenject;

public class HealthCellsGetter : MonoBehaviour
{
    [Inject] PlayerHealth m_playerHealth;

    void Awake()
    {
        m_playerHealth.HealthCells.AddRange(GetComponentsInChildren<HealthCell>());    
    }
}

using UnityEngine;
using Zenject;

public class MoveDetector : MonoBehaviour
{
    [Inject] readonly GameObject m_playerGameObject;

    protected MoveController m_moveController;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == m_playerGameObject && m_moveController.IsMoving)
        {
            print("das " + m_moveController.name);
        }
    }
}
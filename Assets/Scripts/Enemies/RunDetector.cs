using UnityEngine;
using Zenject;

public class RunDetector : MoveDetector
{
    [Inject]
    void Construct(RunController runController)
    {
        m_moveController = runController;
    }
}

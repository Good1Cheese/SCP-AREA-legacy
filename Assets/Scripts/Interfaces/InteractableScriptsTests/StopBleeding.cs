using UnityEngine;
using Zenject;

public class StopBleeding : IInteractable
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public override void Interact()
    {
        m_characterBleeding.StopBleeding();
    }
}

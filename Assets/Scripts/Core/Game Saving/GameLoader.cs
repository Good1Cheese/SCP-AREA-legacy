using System.Collections;
using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
    [Inject] readonly GameLoading m_gameLoading;

    IEnumerator Start()
    {
        if (m_gameLoading.WasGameLoadedFromMenu) 
        {
            yield return new WaitForSeconds(1);
            m_gameLoading.Load();
        }
    }
}

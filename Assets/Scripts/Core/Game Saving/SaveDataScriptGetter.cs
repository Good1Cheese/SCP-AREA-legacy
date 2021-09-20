using UnityEngine;
using Zenject;

namespace Assets.Scripts.ScriptableObjects
{
    public class SaveDataScriptGetter : MonoBehaviour
    {
        [Inject] readonly GameSaving m_gameSaver;

        void Awake()
        {
            DataSaving[] collection = GetComponentsInChildren<DataSaving>();
            m_gameSaver.SaveData.AddRange(collection);
        }
    }
}

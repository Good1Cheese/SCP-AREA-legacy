using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsPreset", menuName = "Installers/SettingsPresetInstaller")]
public class SettingsPresetInstaller : ScriptableObjectInstaller<SettingsPresetInstaller>
{
    public float generalSensivity;

    public override void InstallBindings()
    {
        Container.BindInstance(this);
    }

}
using UnityEngine;

[AddComponentMenu("Scripts/Dungeon Generator/Module")]
public class Module : MonoBehaviour
{
    public ModulesTypes[] Tipo = new ModulesTypes[1];
    void Start()
    {

    }
	public ModuleConnector[] GetExits()
	{
		return GetComponentsInChildren<ModuleConnector>();
	}
}

using UnityEngine;

public class Module : MonoBehaviour
{
    public string[] Tags = new string[1];
    void Start()
    {

        Tags[0] = tag.ToString();
            
        
    }
	public ModuleConnector[] GetExits()
	{
		return GetComponentsInChildren<ModuleConnector>();
	}
}

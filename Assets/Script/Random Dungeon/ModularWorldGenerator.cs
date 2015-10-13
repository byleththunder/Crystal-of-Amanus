using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum ModulesTypes { Quarto, Corredor, CorredorFechado, RespawnRoom, Curva };
[AddComponentMenu("Scripts/Dungeon Generator/Module World Generator")]
public class ModularWorldGenerator : MonoBehaviour
{
    public Module[] Modules;
    public Module StartModule;
    public Module EndLineModule;
    public Module FinalModule;
    public int Iterations = 5;
    public int countdown = 3;
    int indice = 0;
    void Start()
    {
        Generate();
    }

    void Generate()
    {
        var startModule = (Module)Instantiate(StartModule, transform.position, transform.rotation);
        startModule.transform.SetParent(transform);
        var pendingExits = new List<ModuleConnector>(startModule.GetExits());

        for (int iteration = 0; iteration < Iterations; iteration++)
        {
            var newExits = new List<ModuleConnector>();

            foreach (var pendingExit in pendingExits)
            {
                indice++;
                var newTag = SafeCourse(pendingExit.Tipos, pendingExit);
                var newModulePrefab = CheckWay(pendingExit, newTag, iteration);
                var newModule = (Module)Instantiate(newModulePrefab);
                newModule.transform.SetParent(transform);
                newModule.name = newModule.name + " - " + indice;
                var newModuleExits = newModule.GetExits();
                var exitToMatch = newModuleExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleExits);
                MatchExits(pendingExit, exitToMatch);
                newExits.AddRange(newModuleExits.Where(e => e != exitToMatch));
            }

            pendingExits = newExits;
        }
    }

    private Module CheckWay(ModuleConnector lastconnector, ModulesTypes _newTag, int _iteration)
    {
        RaycastHit hit;

        if (Physics.Raycast(lastconnector.transform.position + new Vector3(0, 1, 0), lastconnector.transform.forward, out hit, 5f))
        {
            
            Debug.DrawLine(lastconnector.transform.position, hit.point, Color.green);
            return EndLineModule;
        }

        if (Physics.Raycast(lastconnector.transform.position + new Vector3(0, 1, 0), lastconnector.transform.forward, out hit, 20f))
        {
            //print("20f ---- " + hit.collider.transform.parent + " ----- " + hit.collider.name + " | " + lastconnector.name + " --- " + lastconnector.transform.parent.name);
           Debug.DrawLine(lastconnector.transform.position, hit.point, Color.blue);
            return FinalModule;
        }
        if (_iteration + 1 == Iterations)
        {
            return FinalModule;
        }
        return GetRandomWithTag(Modules, _newTag);
    }
    private ModulesTypes SafeCourse(ModulesTypes[] array, ModuleConnector lastconnector)
    {

        ModulesTypes m = array[Random.Range(0, array.Length)];
        
        return m;
    }
    private void MatchExits(ModuleConnector oldExit, ModuleConnector newExit)
    {
        var newModule = newExit.transform.parent;
        var forwardVectorToMatch = -oldExit.transform.forward;
        var correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newExit.transform.forward);
        newModule.RotateAround(newExit.transform.position, Vector3.up, correctiveRotation);
        var correctiveTranslation = oldExit.transform.position - newExit.transform.position;
        newModule.transform.position += correctiveTranslation;
    }


    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[Random.Range(0, array.Length)];
    }



    private static Module GetRandomWithTag(IEnumerable<Module> modules, ModulesTypes tagToMatch)
    {
        var matchingModules = modules.Where(m => m.Tipo.Contains(tagToMatch)).ToArray();
        return GetRandom(matchingModules);
    }

    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}

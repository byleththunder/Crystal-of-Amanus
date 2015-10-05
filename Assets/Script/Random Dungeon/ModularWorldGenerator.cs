using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ModulesTypes { Quarto, Corredor, CorredorFechado, RespawnRoom, Curva };
public class ModularWorldGenerator : MonoBehaviour
{
    public Module[] Modules;
    public Module StartModule;
    public Module EndModule;
    public int Iterations = 5;
    public int countdown = 3;
    int indice = 0;
    void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        var startModule = (Module)Instantiate(StartModule, transform.position, transform.rotation);
        var pendingExits = new List<ModuleConnector>(startModule.GetExits());

        for (int iteration = 0; iteration < Iterations; iteration++)
        {
            var newExits = new List<ModuleConnector>();

            foreach (var pendingExit in pendingExits)
            {
                indice++;
                var newTag = SafeCourse(pendingExit.Tipos, pendingExit);
                var newModulePrefab = (CheckWay(pendingExit) ? EndModule : (iteration + 1 == Iterations ? EndModule : GetRandomWithTag(Modules, newTag)));
                var newModule = (Module)Instantiate(newModulePrefab);
                newModule.name = newModule.name + " - " + indice;
                var newModuleExits = newModule.GetExits();
                var exitToMatch = newModuleExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleExits);
                MatchExits(pendingExit, exitToMatch);
                newExits.AddRange(newModuleExits.Where(e => e != exitToMatch));
                yield return new WaitForSeconds(0.2f);
            }

            pendingExits = newExits;
        }
    }

    private bool CheckWay(ModuleConnector lastconnector)
    {
        Ray raio = new Ray(lastconnector.transform.position, lastconnector.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(lastconnector.transform.position, lastconnector.transform.forward, out hit, 1000f))
        {
            Debug.Log("fIM DA LINHA = "+ indice + "Hit what? R: "+hit.collider.name);
            Debug.DrawLine(lastconnector.transform.position, hit.point, Color.blue);
            return true;

        }
        if (Physics.Raycast(lastconnector.transform.position, lastconnector.transform.forward+Vector3.right, out hit, 1000f))
        {
            Debug.Log("fIM DA LINHA = " + indice + "Hit what? R: " + hit.collider.name);
            Debug.DrawLine(lastconnector.transform.position, hit.point, Color.red);
            return true;
        }
        if (Physics.Raycast(lastconnector.transform.position, lastconnector.transform.forward + Vector3.left, out hit, 1000f))
        {
            Debug.Log("fIM DA LINHA = " + indice + "Hit what? R: " + hit.collider.name);
            Debug.DrawLine(lastconnector.transform.position, hit.point, Color.green);
            return true;
        }
        return false;
    }
    private ModulesTypes SafeCourse(ModulesTypes[] array, ModuleConnector lastconnector)
    {

        ModulesTypes m = ModulesTypes.Corredor;
        if(countdown==0)
        {
            Debug.Log("RAMDOM");
            countdown = 1;
            m = array[Random.Range(0, array.Length)];
            

            
        }else
        {
            countdown--;
        }
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

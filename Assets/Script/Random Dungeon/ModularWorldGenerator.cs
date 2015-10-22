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
                var newTag = GetRandom(pendingExit.Tipos);
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

    //Essa classe eu que criei.
    private Module CheckWay(ModuleConnector lastconnector, ModulesTypes _newTag, int _iteration)
    {
        RaycastHit hit;
        //Para saber se eu devo colocar um objeto vázio entre caminhos ou fechar a passagem, eu disparo um raio 1m acima do ModuleConnector para que o raio detecte as paredes
        //Se eu colocar o raio na mesma altura do ModuleConnector, o raio ultrapassa o Quad(Chão), fazendo com que não se detecte nada. Por que as distancias 5f e 20f?
        //5f é a distancia minima para detectar se existe uma sala na frente do corredor. 20f é uma distancia segura, que impede que salas e corredores se colidam.
        //obs.: Quando se atinge o limite de iterações, eu fecho as passagens
        /*6|
         *5|
         *4|
         *3|                         
         *2|                        _____
         *1|---------------->(Raio) |___| (Parede)
         *o-------------------------------(Quad)
         * */
        if (Physics.Raycast(lastconnector.transform.position + new Vector3(0, 1, 0), lastconnector.transform.forward - new Vector3(0,1,0), out hit, 5f))
        {
            
            Debug.DrawLine(lastconnector.transform.position, hit.point, Color.green);
            return EndLineModule;
        }

        if (Physics.Raycast(lastconnector.transform.position + new Vector3(0, 1, 0), lastconnector.transform.forward, out hit, 20f))
        {
            Debug.DrawLine(lastconnector.transform.position, hit.point, Color.blue);
            return FinalModule;
        }
        if (_iteration + 1 == Iterations)
        {
            return FinalModule;
        }
        return GetRandomWithTag(Modules, _newTag);
    }
   
    /*Isso pertence ao script original, é a parte que eu necessitava. Nesse método
     *se pega a referencia da posição do modulo antigo para que o novo se conecte a ele na posição e rotação certa. Além de juntar o module, se junta todo o prefab da sala 
     *ou corredor.
    */
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

using UnityEngine;
using System.Collections;

public class ExplodirModelos : MonoBehaviour {

    IEnumerator SplitMesh()
    {
        
        MeshFilter MF = GetComponent<MeshFilter>();
        MeshRenderer MR = GetComponent<MeshRenderer>();
        Mesh M = MF.mesh;
        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;
        print(M.subMeshCount);
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {
            int[] indices = M.GetTriangles(submesh);
            
            for (int i = 0; i < indices.Length; i += (indices.Length/20))
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }
                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                GO.transform.position = transform.position-transform.lossyScale;
                GO.transform.rotation = transform.rotation;
                GO.layer = LayerMask.NameToLayer("Fragmentos");
                GO.AddComponent<MeshRenderer>().material = MR.materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                GO.AddComponent<BoxCollider>();
                GO.AddComponent<Rigidbody>().AddExplosionForce(-200f, transform.position, 20);

                Destroy(GO, Random.Range(0.0f, 0.2f));
            }
        }
        MR.enabled = false;

        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    public void Destruir()
    {
        StartCoroutine(SplitMesh());
    }
}

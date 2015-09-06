using UnityEngine;
using System.Collections;

public class Morcego : Monster
{

    public Rigidbody rdb;
    float ramdom;
    // Use this for initialization
    void Start()
    {
        rdb = GetComponent<Rigidbody>();
        ramdom = Random.Range(0, 91);
    }

    public override void AI()
    {
        // print((Vector3.up * Mathf.Sin(Time.time * 3 + ramdom)));
        float sin = Mathf.Sin((Time.time+ramdom));
        rdb.AddRelativeForce(Vector3.up * sin);
    }
}

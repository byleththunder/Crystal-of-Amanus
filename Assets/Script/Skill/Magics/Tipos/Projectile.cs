using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public enum ProjectType { None, Fire };
    public ProjectType Type;
    public bool Reflect = false;
    public string Tag = string.Empty;
    public string WhoShoot = string.Empty;
}

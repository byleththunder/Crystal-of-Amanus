using UnityEngine;
using System.Collections;

public class Monster : Target {

	public virtual Item Loot()
    {
        return null;
    }
    public virtual void AI()
    {

    }
}

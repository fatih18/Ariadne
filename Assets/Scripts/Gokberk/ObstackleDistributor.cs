using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstackleDistributor : MonoBehaviour
{

    public void distribute()
    {
        //TODO implement a obstacle distribution algorithm.
    }
    abstract public void setMesh(Mesh mesh);
    abstract public void setMaterial(Material material);
    //abstract public void setBehavior(Behavior behavior);

}

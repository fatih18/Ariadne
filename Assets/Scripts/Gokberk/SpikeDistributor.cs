using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDistributor : ObstackleDistributor
{
    public Mesh mesh;
    public Material material;
    //public Behavior behavior;
    
    public override void setMesh(Mesh mesh)
    {
        mesh = this.mesh;
    }
    public override void setMaterial(Material material)
    {
        material = this.material;
    }
    //public void setBehavior(Behavior behavior)
   //// }
    
}   


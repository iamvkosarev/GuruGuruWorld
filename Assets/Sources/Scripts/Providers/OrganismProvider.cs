using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class OrganismProvider : MonoProvider<OrganismComponent>
{
    private void Awake()
    {
        value.Transform = transform;
    }
}

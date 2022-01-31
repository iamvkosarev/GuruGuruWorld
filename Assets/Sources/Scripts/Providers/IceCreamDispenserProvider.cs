using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class IceCreamDispenserProvider : MonoProvider<IceCreamDispenserComponent>
{
    private void Awake()
    {
        value.IceCreamTime = UnityEngine.Random.Range(4f, 8f);
    }
}

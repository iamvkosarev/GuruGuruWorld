using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Voody.UniLeo;


public class HungryProvider : MonoProvider<HungryComponent>
{
    public Color mouthColor;

    private void OnDrawGizmos()
    {
        Gizmos.color = mouthColor;
        Gizmos.DrawWireCube(value.MouthPoint.position, value.MouthSize);
    }
}


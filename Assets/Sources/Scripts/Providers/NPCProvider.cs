using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class NPCProvider : MonoProvider<NPCComponent>
{
    private void Awake()
    {
        value.TargetPos = transform.position;
    }
}

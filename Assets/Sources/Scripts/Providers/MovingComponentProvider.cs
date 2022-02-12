using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public sealed class MovingComponentProvider : MonoProvider<MovingComponent>
{
    private void Awake()
    {
        value.IncreasingSpeedValue = 1f;
        //value.JumpSpandedTime = UnityEngine.Random.Range(0.4f, 1f);
    }
}

using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class SetColorProvider : MonoProvider<SetColorComponent>
{
    public Color[] ChoosingColours;
    private void Awake()
    {
        value.NeedColor = ChoosingColours[UnityEngine.Random.Range(0, ChoosingColours.Length)];
    }
}

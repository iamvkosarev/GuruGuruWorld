using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class SetFaceProvider : MonoProvider<SetFaceComponent>
{
    public void SetData(Transform transform, PelmenFaceType pelmenFaceType)
    {
        value.PelmenTransform = transform;
        value.PelmenFaceType= pelmenFaceType;
    }
}

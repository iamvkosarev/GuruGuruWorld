using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class DuplicateMovingProvider : MonoProvider<DuplicateMovingComponent>
{
    private void OnDrawGizmos()
    {
        if(value.DuplicateTransform == null) { return; }
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(value.WhenTelportPoint + value.DuplicateTransform.parent.position, 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(value.WhereTelportLocalPoint + value.DuplicateTransform.parent.position, 1f);
    }
}

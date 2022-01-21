using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Voody.UniLeo;

public class PositionRendererSorterProvider : MonoProvider<PositionRendererSorterComponent>
{
    [SerializeField] private Color color = Color.white;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        if(value.Transform != null)
        {
            Gizmos.DrawSphere(value.Transform.position + new Vector3(0, value.Offset), 1f);

        }
    }
}


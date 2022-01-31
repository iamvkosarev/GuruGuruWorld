using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class ShowHelpImageProvider : MonoProvider<ShowHelpImageComponent>
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(value.CheckPoint != null)
        {
            Gizmos.DrawWireCube(value.CheckPoint.position, value.CheckPlayerSize);
        }
    }
}

using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class DamageAdderProvider : MonoProvider<DamageAdderComponent>
{
    public int ellipseLines = 100;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var damageCheckerBox in value.DamageCheckerEllipses)
        {
            float a = damageCheckerBox.A;
            float b = damageCheckerBox.B;
            for (int i = 0; i < ellipseLines; i++)
            {
                float angle = Mathf.PI * 2 * (float)i / (float)ellipseLines;
                float sin = Mathf.Sin(angle);
                float cos = Mathf.Cos(angle);
                var r = a * b / Mathf.Sqrt(a * a * sin * sin + b * b * cos * cos);
                Gizmos.DrawLine(value.HightTransform.position + damageCheckerBox.LocalPos, value.HightTransform.position + damageCheckerBox.LocalPos + r * new Vector3(cos, sin));
            }
        }

    }
}

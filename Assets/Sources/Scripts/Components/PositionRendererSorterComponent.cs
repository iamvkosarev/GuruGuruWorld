using UnityEngine;
using System;

[Serializable]
public struct PositionRendererSorterComponent
{
    public int Offset;
    public bool RunOnlyOnce;
    public Renderer Renderer;
    public Transform Transform;
}
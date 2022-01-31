using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportView : UsingView
{
    public bool isNotFree;
    public MovingStaticData MovingStaticData;
    public Vector3 Direction;
    public Transform UsingPoint;

    public bool IsNotFree
    {
        set
        {
            isNotFree = value;
            IsUsing = value;
        }
        get
        {
            return isNotFree;
        }
    }

}

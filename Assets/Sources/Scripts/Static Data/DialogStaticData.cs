using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

[CreateAssetMenu(menuName = "MyProject/Game/DialogStaticData")]
public class DialogStaticData : ScriptableObject
{
    public DialogProvider DialogProvider;
    public SetTextProvider SetTextProvider;
}

using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

[CreateAssetMenu(menuName = "MyProject/Game/TextStaticData")]
public class TextStaticData : ScriptableObject
{
    public DialogProvider DialogProvider;
    public SetTextProvider SetTextProvider;
    public TextMassageProvider TextMassageProvider;
}

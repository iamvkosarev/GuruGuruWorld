using Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class SetTextProvider : MonoProvider<SetTextComponent>
{
    public void SetStruct(string text, SpeakerStruct speakingTextStructure, float switchOffTime)
    {
        value.Text = text;
        value.Offcet = speakingTextStructure.Offcet;
        value.Taraget = speakingTextStructure.Transform;
        value.ButtomTagOffset = speakingTextStructure.ButtomTagOffset;
        value.SwitchOffTime = switchOffTime;
    }
}

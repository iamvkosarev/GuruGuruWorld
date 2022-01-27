using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct HungryComponent
{
    public ParticleSystem EatingVFX;
    public Slider Slider;
    public Transform SpawnEatenPointsText;
    public Transform MouthPoint;
    public Vector3 MouthSize;
    public int EatenPoints;
    public int MaxEatenPoints;
    public float ShowingSliderTimer;
    public float ShowingSliderTimerMax;
}
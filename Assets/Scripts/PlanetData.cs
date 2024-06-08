using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Data", menuName = "Planetary System/New Planet Data", order = 0)]
public class PlanetData : ScriptableObject
{
    public MassClassEnum MassClassEnum;
    public double MinMass;
    public double MaxMass;
    public float MinRadius;
    public float MaxRadius;

    [Range(10.0f, 100.0f)]
    public float MaxSpeed;
}
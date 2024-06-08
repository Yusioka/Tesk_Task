using UnityEngine;

public interface IPlanetaryObject
{
    MassClassEnum MassClassEnum { get; }
    double Mass { get; }
    float Radius { get; }
    float Speed { get; }
    float DistanceToSystemCenter { get; }

    void Initialize(PlanetData planetData, double mass);
}
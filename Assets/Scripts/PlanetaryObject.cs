using UnityEngine;

public class PlanetaryObject : MonoBehaviour, IPlanetaryObject
{
    public MassClassEnum MassClassEnum { get; set; }
    public double Mass { get; set; }
    public float Radius { get; set; }
    public float Speed { get; set; }
    public float DistanceToSystemCenter { get; set; }

    public void Initialize(PlanetData planetData, double mass)
    {
        MassClassEnum = planetData.MassClassEnum;
        Mass = mass;
        Radius = InterpolateRadius(Mass, planetData.MinMass, planetData.MaxMass, planetData.MinRadius, planetData.MaxRadius);
        Speed = Random.Range(10, planetData.MaxSpeed);

        InitializeVisual();
    }

    private float InterpolateRadius(double mass, double minMass, double maxMass, float minRadius, float maxRadius)
    {
        return minRadius + (float)((mass - minMass) / (maxMass - minMass) * (maxRadius - minRadius));
    }

    private void InitializeVisual()
    {
        transform.localScale = new Vector3(Radius, Radius, Radius);

        var planetColor = Random.ColorHSV(0, 1, 0.5f, 1, 0.2f, 1, 1, 1);
        GetComponent<Renderer>().material.color = planetColor;

        var planetTrail = GetComponent<TrailRenderer>();
        planetTrail.startWidth = Radius / 2;
        planetTrail.endWidth = 0;
        planetTrail.material.color = planetColor;
    }
}
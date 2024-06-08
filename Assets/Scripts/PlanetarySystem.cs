using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetarySystem : MonoBehaviour, IPlanetarySystem
{
    [SerializeField] private List<PlanetData> _planetsData;
    [SerializeField] private PlanetaryObject _planetPrefab;

    private List<PlanetaryObject> _planetaryObjects = new List<PlanetaryObject>();

    public IEnumerable<IPlanetaryObject> PlanetaryObjects { get => _planetaryObjects; }
    public List<PlanetData> PlanetsData { get => _planetsData; }
    public double CurrentSystemMass { get; set; }

    public void UpdateSystem(float deltaTime)
    {
        foreach (var planetaryObject in _planetaryObjects)
        {
            planetaryObject.transform.RotateAround(transform.position, Vector3.up, planetaryObject.Speed * deltaTime);
        }
    }

    public void CreatePlanet(PlanetData planetData, double mass)
    {
        var planet = Instantiate(_planetPrefab, transform);
        planet.Initialize(planetData, mass);

        _planetaryObjects.Add(planet);

        planet.DistanceToSystemCenter = GetDistanceToCenter(planet.Radius, _planetaryObjects);
        planet.transform.position = new Vector3(planet.DistanceToSystemCenter, 0, planet.DistanceToSystemCenter);
    }

    public void DestroySystem()
    {
        foreach (var planetaryObject in _planetaryObjects)
        {
            Destroy(planetaryObject.gameObject);
        }

        Destroy(gameObject);
    }

    private float GetDistanceToCenter(float radius, IEnumerable<PlanetaryObject> planetaryObjects)
    {
        float minDistance = 0;

        if (planetaryObjects.Any())
        {
            minDistance = planetaryObjects.Max(existingPlanet => existingPlanet.DistanceToSystemCenter + Mathf.Max(radius, existingPlanet.Radius));
        }

        return minDistance + radius;
    }
}
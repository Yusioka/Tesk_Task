using System.Linq;
using UnityEngine;

public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
{
    [SerializeField] private PlanetarySystem _planetarySystemPrefab;

    private System.Random _random = new System.Random();

    public IPlanetarySystem Create(double mass)
    {
        var planetarySystem = Instantiate(_planetarySystemPrefab);
        planetarySystem = InitializeSystem(mass, planetarySystem);

        Debug.Log($"Total mass: {planetarySystem.CurrentSystemMass}");

        return planetarySystem;
    }

    private PlanetarySystem InitializeSystem(double mass, PlanetarySystem planetarySystem)
    {
        var sortedPlanetsData = planetarySystem.PlanetsData.OrderByDescending(planetData => planetData.MaxMass);

        while (planetarySystem.CurrentSystemMass < mass)
        {
            var availablePlanetaryObjectsData = sortedPlanetsData.Where(planetData => planetData.MinMass + planetarySystem.CurrentSystemMass < mass);

            var maxIndex = availablePlanetaryObjectsData.Count() / 2;
            var planetaryObjectData = availablePlanetaryObjectsData.ElementAt(_random.Next(maxIndex));
            var planetaryObjectMass = planetaryObjectData.MinMass + _random.NextDouble() * (planetaryObjectData.MaxMass - planetaryObjectData.MinMass);

            var remainingMass = mass - planetarySystem.CurrentSystemMass;

            if (planetarySystem.CurrentSystemMass == 0 && planetaryObjectMass >= mass)
            {
                continue;
            }

            if (planetaryObjectMass > remainingMass)
            {
                planetaryObjectMass = remainingMass;
            }

            planetarySystem.CreatePlanet(planetaryObjectData, planetaryObjectMass);

            Debug.Log($"{planetaryObjectData.name} Mass: {planetaryObjectMass}");

            planetarySystem.CurrentSystemMass += planetaryObjectMass;
        }

        return planetarySystem;
    }
}
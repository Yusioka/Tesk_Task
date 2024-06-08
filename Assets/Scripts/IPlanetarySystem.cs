using System.Collections.Generic;

public interface IPlanetarySystem
{
    IEnumerable<IPlanetaryObject> PlanetaryObjects { get; }
    void UpdateSystem(float deltaTime);
    void DestroySystem();
}
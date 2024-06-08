using System.Globalization;
using TMPro;
using UnityEngine;

public class PlanetarySystemCreator : MonoBehaviour
{
    [SerializeField] private double _maxPlanetarySystemMass;
    [SerializeField] private PlanetarySystemFactory _systemFactory;
    [SerializeField] private TMP_InputField _massText;

    private IPlanetarySystem _planetarySystem;

    private void Awake()
    {
        _massText.onEndEdit.AddListener((_systemFactory) => CreateSystemByButton());
    }

    private void Update()
    {
        if (_planetarySystem != null)
        {
            _planetarySystem.UpdateSystem(Time.deltaTime);
        }
    }

    public void CreateSystemByButton()
    {
        var input = _massText.text.Replace(',', '.');

        if (_planetarySystem != null)
        {
            _planetarySystem.DestroySystem();
            _planetarySystem = null;
        }

        if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double mass) && mass <= _maxPlanetarySystemMass)
        {
            _planetarySystem = _systemFactory.Create(mass);
        }
        else
        {
            Debug.LogError("Invalid mass input or system mass is too high");
        }
    }
}
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _minZoom = 0f;
    [SerializeField] private float _maxZoom = 1000.0f;

    private void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            var newZoom = Mathf.Clamp(gameObject.transform.position.y - scroll * _cameraSpeed * Time.deltaTime, _minZoom, _maxZoom);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, newZoom, gameObject.transform.position.z);
        }
    }
}
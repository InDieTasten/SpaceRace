using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Spaceship;

    // The maximum velocity - when reached the camera is at max zoom level (and stays there even if velocity increases)
    public float maxVelocity;

    public float maxZoom;
    public float minZoom;

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Binds camera position to spaceship position - so it is always centered
        _camera.transform.position = new Vector3(Spaceship.transform.position.x, Spaceship.transform.position.y, _camera.transform.position.z);

        var zoom = (maxZoom * Spaceship.GetComponent<Rigidbody2D>().velocity.magnitude) / maxVelocity;

        // make sure the zoom is between max and min values
        _camera.orthographicSize = Mathf.Min(Mathf.Max(minZoom, zoom), maxZoom);
    }
}

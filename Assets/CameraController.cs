using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Spaceship;

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
        _camera.transform.position = new Vector3(Spaceship.transform.position.x, Spaceship.transform.position.y, _camera.transform.position.z);

        var zoom = (maxZoom * Spaceship.GetComponent<Rigidbody2D>().velocity.magnitude) / maxVelocity;
        _camera.orthographicSize = Mathf.Min(Mathf.Max(minZoom, zoom), maxZoom);
    }
}

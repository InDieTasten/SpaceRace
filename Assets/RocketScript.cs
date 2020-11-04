using UnityEngine;

public class RocketScript : MonoBehaviour
{
    private Vector2 _initalPosition;

    public float LateralThrust;
    public float TorgueThrust;
    public float Weight;

    // Start is called before the first frame update
    void Start()
    {
        _initalPosition = this.gameObject.GetComponent<Rigidbody2D>().position;
        this.gameObject.GetComponent<Rigidbody2D>().mass = Weight;
    }

    // Update is called once per frame
    void Update()
    {
        var r2d = this.gameObject.GetComponent<Rigidbody2D>();

        // Move the spaceship when an arrow key is pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            r2d.AddRelativeForce(new Vector2(0, LateralThrust));
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            r2d.AddTorque(TorgueThrust);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            r2d.AddTorque(-1 * TorgueThrust);
        }
    }

    public void Reset()
    {
        var r2d = this.gameObject.GetComponent<Rigidbody2D>();

        r2d.velocity = Vector2.zero;
        r2d.angularVelocity = 0;
        r2d.SetRotation(-90);
        r2d.position = _initalPosition;
    }
}

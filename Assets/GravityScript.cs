using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public GameObject Spaceship;
    public GameObject Earth;
    public GameObject Moon;

    public GameObject GravityText;

    public float GravityEarth;
    public float GravityMoon;

    // Start is called before the first frame update
    void Start()
    {
        if (Spaceship == null || Spaceship.tag != "Spaceship")
        {
            throw new System.Exception("Spaceship missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var directionFromShipToEarth = (Earth.transform.position - Spaceship.transform.position).normalized;
        var directionFromShipToMoon = (Moon.transform.position - Spaceship.transform.position).normalized;

        Physics2D.gravity = (directionFromShipToEarth * CalcGravityEarth()) + (directionFromShipToMoon * CalcGravityMoon());

        GravityText.GetComponent<UnityEngine.UI.Text>().text =
            "x: " + System.Math.Round(Physics2D.gravity.x, 2) +
            " y: " + System.Math.Round(Physics2D.gravity.y, 2) +
            " speed: " + System.Math.Round(Spaceship.GetComponent<Rigidbody2D>().velocity.magnitude, 2);
    }

    //ToDo: Improve Calculation
    private float CalcGravityEarth()
    {
        return GravityEarth / ((Earth.transform.position - Spaceship.transform.position).magnitude);
    }

    private float CalcGravityMoon()
    {
        return GravityMoon / ((Moon.transform.position - Spaceship.transform.position).magnitude);
    }
}

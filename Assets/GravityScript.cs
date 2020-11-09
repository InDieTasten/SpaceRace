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

        // The gravity that affects the ship is the combined gravity from earth and the moon
        Physics2D.gravity = (directionFromShipToEarth * CalcGravityEarth()) + (directionFromShipToMoon * CalcGravityMoon());

        // Text for debugging purposes - tells you the direction and strenght of the current gravity
        GravityText.GetComponent<UnityEngine.UI.Text>().text =
            "x: " + System.Math.Round(Physics2D.gravity.x, 2) +
            " y: " + System.Math.Round(Physics2D.gravity.y, 2) +
            " speed: " + System.Math.Round(Spaceship.GetComponent<Rigidbody2D>().velocity.magnitude, 2);
    }

    //ToDo: Improve Calculation
    private float CalcGravityEarth()
    {
        // Should be gravity = (constant * massPlanet * massShip) / distance² - for real earth and spaceship (1 ton) this is around 9819 Newton on the surface
        // Since the game is not to scale we set our own constant and degradation
        return GravityEarth / ((Earth.transform.position - Spaceship.transform.position).magnitude);
    }

    private float CalcGravityMoon()
    {
        return GravityMoon / ((Moon.transform.position - Spaceship.transform.position).magnitude);
    }
}

using UnityEngine;

public class BorderCollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Spaceship")
        {
            coll.gameObject.SendMessage("Reset");
        }
    }
}

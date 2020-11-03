using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject crystal;
    public GameObject plate;
    public Vector3 mousePos;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    
    public RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        PlaceCrystal();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
        mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
        
        hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Renderer>().enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                print("Booom");
                PlaceCrystal();
            }
        }
        else
        {
            crystal.GetComponent<Renderer>().enabled = false;
        }
    }

    private void PlaceCrystal()
    {
        var platePos = plate.transform.position;
        var plateSize = plate.GetComponent<Renderer>().bounds.size;
        float minX = platePos.x - plateSize.x/2;
        float maxX = platePos.x + plateSize.x/2;
        float minY = platePos.y - plateSize.y/2;
        float maxY = platePos.y + plateSize.y/2;

        var targetPoint = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

        crystal.transform.position = targetPoint;
    }
}

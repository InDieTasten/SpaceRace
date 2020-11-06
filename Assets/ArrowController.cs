using System;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    public GameObject ArrowSpriteRenderer;
    public GameObject ArrowText;
    public GameObject Spaceship;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var direction = Target.transform.position - Spaceship.transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        if (angle > -225 && angle <= -135)
        {
            angle = -180;
        }
        else if (angle > -135 && angle <= -45)
        {
            angle = -90;
        }
        else if (angle > -45 && angle <= 45)
        {
            angle = 0;
        }
        else if (angle > 45 || angle <= -225)
        {
            angle = 90;
        }
        ArrowSpriteRenderer.GetComponent<RectTransform>().transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        ArrowText.GetComponent<Text>().text = Math.Round(Mathf.Abs(Vector2.Distance(Spaceship.transform.position, Target.transform.position)), 2).ToString();
    }
}

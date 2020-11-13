using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the arrow on the edge of the screen pointing to the "Target"
/// </summary>
public class ArrowController : MonoBehaviour
{
    // The actual arrow
    public GameObject ArrowSpriteRenderer;

    // The text next to the arrow - indicates distance
    public GameObject ArrowText;

    public GameObject Spaceship;
    public GameObject Target;

    private Rect _canvas;
    private RectTransform _arrow;
    private RectTransform _arrowText;
    private float _arrowHeight;
    private float _arrowTextHeight;
    private float _arrowTextWidth;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = this.GetComponent<RectTransform>().rect;
        _arrow = ArrowSpriteRenderer.GetComponent<RectTransform>();
        _arrowText = ArrowText.GetComponent<RectTransform>();
        _arrowHeight = _arrow.rect.height;
        _arrowTextHeight = _arrowText.rect.height;
        _arrowTextWidth = _arrowText.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        float angleInDegrees = CalculateAngleFromSpaceshipToTarget();

        if (angleInDegrees > 315 || angleInDegrees <= 45)
        {
            RenderUpArrow(angleInDegrees);
            // Set the angle so that it points straight to the edge of the screen
            angleInDegrees = 0;
        }
        else if (angleInDegrees > 45 && angleInDegrees <= 135)
        {
            RenderRightArrow(angleInDegrees);
            angleInDegrees = 90;
        }
        else if (angleInDegrees > 135 && angleInDegrees <= 225)
        {
            RenderDownArrow(angleInDegrees);
            angleInDegrees = 180;
        }

        else if (angleInDegrees > 225 && angleInDegrees <= 315)
        {
            RenderLeftArrow(angleInDegrees);
            angleInDegrees = 270;
        }

        var arrowAngle = angleInDegrees;
        if (angleInDegrees == 90)
        {
            arrowAngle = -90;
        }
        else if (angleInDegrees == 270)
        {
            arrowAngle = 90;
        }
        ArrowSpriteRenderer.GetComponent<RectTransform>().transform.rotation = Quaternion.AngleAxis(arrowAngle, Vector3.forward);


        ArrowText.GetComponent<Text>().text = Math.Round(Mathf.Abs(Vector2.Distance(Spaceship.transform.position, Target.transform.position)), 2).ToString();

    }

    private void RenderDownArrow(float angleInDegrees)
    {
        // The arrow is supposed to be on the edge of the screen in relation to the angle. 
        // e.g: That target is 45 degrees from the ship - the arrow should be on the top right edge. For 135 degrees on the bottom right edge.
        // So we need to devide the height (or width - depending on the direction) of the canvas (or screen) by 90, so 1 degree represents X pixel of width or height
        // To set the lowest possible value to 0 and the highest to 90 we subtract the minimum size of the angle for that direction and store it in a temp variable.
        var tempAngleDegrees = angleInDegrees - 135;

        // We need to subtract the total available with / 2 because the anchored position is relative to the center of the canvas
        // Note: "anchored position" - relative to screen center - "position" relative to global coordinates
        var xPositionOfTheArrow = -1 * (((_canvas.width / 90) * tempAngleDegrees) - (_canvas.width / 2));

        _arrow.anchoredPosition = new Vector2(xPositionOfTheArrow, (-1 * _canvas.height / 2) + _arrowHeight);
        _arrowText.anchoredPosition = new Vector2(_arrow.anchoredPosition.x, _arrow.anchoredPosition.y + _arrowTextHeight);
    }

    private void RenderRightArrow(float angleInDegrees)
    {
        var tempAngleDegrees = angleInDegrees - 45;
        var yPositionOfTheArrow = -1 * (((_canvas.height / 90) * tempAngleDegrees) - (_canvas.height / 2));

        // Note: We offset the arrow by its height, not its width, because it is rotated
        _arrow.anchoredPosition = new Vector2((_canvas.width / 2) - _arrowHeight, yPositionOfTheArrow);
        _arrowText.anchoredPosition = new Vector2(_arrow.anchoredPosition.x - _arrowTextWidth, _arrow.anchoredPosition.y);
    }

    private void RenderLeftArrow(float angleInDegrees)
    {
        var tempAngleDegrees = angleInDegrees - 225;
        var yPositionOfTheArrow = ((_canvas.height / 90) * tempAngleDegrees) - (_canvas.height / 2);

        _arrow.anchoredPosition = new Vector2((-1 * _canvas.width / 2) + _arrowHeight, yPositionOfTheArrow);
        _arrowText.anchoredPosition = new Vector2(_arrow.anchoredPosition.x + _arrowTextWidth, _arrow.anchoredPosition.y);
    }

    private void RenderUpArrow(float angleInDegrees)
    {
        var tempAngleDegrees = angleInDegrees;

        // we need to shift the values around a bit so that we get a nice 0 to 90 set of values
        if (angleInDegrees > 315)
        {
            tempAngleDegrees -= 360;
        }
        tempAngleDegrees += 45;

        var xPositionOfTheArrow = ((_canvas.width / 90) * tempAngleDegrees) - (_canvas.width / 2);

        _arrow.anchoredPosition = new Vector2(xPositionOfTheArrow, (_canvas.height / 2) - _arrowHeight);
        _arrowText.anchoredPosition = new Vector2(_arrow.anchoredPosition.x, _arrow.anchoredPosition.y - _arrowTextHeight);
    }

    private float CalculateAngleFromSpaceshipToTarget()
    {
        // A vector from spaceship to target
        var direction = Target.transform.position - Spaceship.transform.position;

        // The angle from an up-vector to the direction of from the spaceship to the target
        // Note: On a compass the values would be: North - 0 , East - 90 , South - 180, West - 270; - So when the angle is 270 the target is west of the spaceship
        float angle = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);

        // The angle is returned as -180 to 180 - we convert it so it is like a compass and thus easier to think about
        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}

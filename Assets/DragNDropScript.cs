using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDropScript : MonoBehaviour
{
    public Vector2 SmallestPosition;
    public Vector2 BiggestPosition;

    private Renderer _initialRenderer;
    private Transform _initialTransform;

    private Ray? _lastMousePosition;

    private bool _firstMouseDownWasInObject;

    void Start()
    {
        _initialRenderer = this.gameObject.GetComponent<Renderer>();
        _initialTransform = this.gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(DragNDropingObject()){
            if(_lastMousePosition != null){
                float moveXDistance = mousePosition.origin.x - _lastMousePosition.Value.origin.x;
                float moveYDistance = mousePosition.origin.y - _lastMousePosition.Value.origin.y;
                float newXPosition = _initialTransform.position.x + moveXDistance;
                float newYPosition = _initialTransform.position.y + moveYDistance;

                Vector3 newPosition = new Vector3();
                newPosition.z = _initialTransform.position.z;

                if(SmallestPosition.x < newXPosition && BiggestPosition.x > newXPosition){
                    newPosition.x = newXPosition;
                }else{
                    newPosition.x = _initialTransform.position.x;
                }

                if(SmallestPosition.y < newYPosition && BiggestPosition.y > newYPosition){
                    newPosition.y = newYPosition;
                }else{
                    newPosition.y = _initialTransform.position.y;
                }

                _initialTransform.position = newPosition;
                
            }
            _lastMousePosition = (Ray?)mousePosition;
        }else{
            _lastMousePosition = null;
        }        
    }

    bool DragNDropingObject()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButton(0)){
            if(!_firstMouseDownWasInObject && mousePosition.origin.x > _initialRenderer.bounds.min.x && mousePosition.origin.y > _initialRenderer.bounds.min.y && mousePosition.origin.x < _initialRenderer.bounds.max.x && mousePosition.origin.y < _initialRenderer.bounds.max.y){
                _firstMouseDownWasInObject = true;
            }
        }else{
            _firstMouseDownWasInObject = false;
        }
        return _firstMouseDownWasInObject;
    }
}

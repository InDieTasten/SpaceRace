using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PipeController : MonoBehaviour
{
    private Renderer _initialRenderer;
    private Transform _transformPipe;

    private Ray? _lastMousePosition;

    private float _initialXPosition;

    void Start()
    {
        _initialRenderer = this.gameObject.GetComponent<Renderer>();
        _transformPipe = this.gameObject.GetComponent<Transform>();
        _initialXPosition = _transformPipe.position.x;
    }

    void Update()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(MousePrimaryHeltDownInPipe()){
            if(_lastMousePosition != null){
                float moveDistance = mousePosition.origin.x - _lastMousePosition.Value.origin.x;
                var newPipePosition = new Vector3(_transformPipe.position.x + moveDistance, _transformPipe.position.y, _transformPipe.position.z);
                if(newPipePosition.x > _initialXPosition){
                   _transformPipe.position = newPipePosition; 
                }
                
            }
            _lastMousePosition = (Ray?)mousePosition;
        }else{
            _lastMousePosition = null;
        }        
    }

    bool MousePrimaryHeltDownInPipe()
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Input.GetMouseButton(0) && mousePosition.origin.x > _initialRenderer.bounds.min.x && mousePosition.origin.y > _initialRenderer.bounds.min.y && mousePosition.origin.x < _initialRenderer.bounds.max.x && mousePosition.origin.y < _initialRenderer.bounds.max.y;
    }

}

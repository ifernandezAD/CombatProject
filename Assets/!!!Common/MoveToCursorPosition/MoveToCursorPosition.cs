using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveToCursorPosition : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    Camera main;

    private void Awake()
    {
        main = Camera.main;
    }
    
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.value;
        Ray ray = main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            transform.position = hit.point;
        }
    }
}

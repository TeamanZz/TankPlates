using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private bool canMoveAutomaticaly;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;

    private int directionSign = 1;

    void Update()
    {
        if (!canMoveAutomaticaly)
            return;

        MoveAutomaticaly();
    }

    private void OnMouseDown()
    {
        canMoveAutomaticaly = false;
    }

    private void OnMouseUp()
    {
        canMoveAutomaticaly = true;
    }

    private void OnMouseDrag()
    {
        MoveManually();
    }

    private void MoveManually()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newCubePosition = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(newCubePosition.x, leftEdge, rightEdge), transform.position.y, transform.position.z);
    }

    private void MoveAutomaticaly()
    {
        Vector3 newPos = transform.position + new Vector3(movementSpeed * directionSign * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(newPos.x, leftEdge, rightEdge), newPos.y, newPos.z);
        if (transform.position.x >= rightEdge || transform.position.x <= leftEdge)
            directionSign *= -1;
    }
}
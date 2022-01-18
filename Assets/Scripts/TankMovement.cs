using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public bool needMoveForward;
    [SerializeField] private bool canMoveAutomaticaly;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;

    private int directionSign = 1;

    void Update()
    {
        if (needMoveForward)
        {
            MoveTankForward();
            MoveCameraForward();
        }

        if (canMoveAutomaticaly)
            MoveHorizontalAutomaticaly();
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
        MoveHorizontalManually();
    }

    private void MoveHorizontalManually()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newCubePosition = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(newCubePosition.x, leftEdge, rightEdge), transform.position.y, transform.position.z);
    }

    private void MoveHorizontalAutomaticaly()
    {
        Vector3 newPos = transform.position + new Vector3(movementSpeed * directionSign * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(newPos.x, leftEdge, rightEdge), newPos.y, newPos.z);
        if (transform.position.x >= rightEdge || transform.position.x <= leftEdge)
            directionSign *= -1;
    }

    private void MoveTankForward()
    {
        Vector3 newPos = transform.position + new Vector3(0, 0, movementSpeed / 5 * Time.deltaTime);
        transform.position = newPos;
    }

    private void MoveCameraForward()
    {
        Vector3 currentCameraPos = Camera.main.transform.position;
        Vector3 cameraNewPos = new Vector3(0, currentCameraPos.y, currentCameraPos.z + movementSpeed / 5 * Time.deltaTime);
        Camera.main.transform.position = cameraNewPos;
    }
}
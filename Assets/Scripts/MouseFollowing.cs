using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowing : MonoBehaviour
{
    private Vector3 lastMousePos = default;

    public Transform tankTransform;
    public TankMovement tankMovement;

    private bool canMoveTank = true;

    private void OnEnable()
    {
        lastMousePos = default;
    }

    public void DenyTankMovement()
    {
        canMoveTank = false;
    }

    public void AllowTankMovement()
    {
        canMoveTank = true;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            lastMousePos = default;
            tankMovement.canMoveAutomaticaly = true;
        }

        if (!Input.GetKey(KeyCode.Mouse0))
            return;

        if (canMoveTank == false)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (tankMovement.canMoveAutomaticaly == true)
                tankMovement.canMoveAutomaticaly = false;

            var currentMousePos = new Vector3(hit.point.x, 0, 0);
            if (lastMousePos == default)
                lastMousePos = currentMousePos;
            var deltaX = currentMousePos.x - lastMousePos.x;

            Vector3 newTankPos = new Vector3(tankTransform.position.x + deltaX, tankTransform.position.y, tankTransform.position.z);
            tankTransform.position = new Vector3(Mathf.Clamp(newTankPos.x, -5, 5), tankTransform.position.y, tankTransform.position.z);
            lastMousePos = currentMousePos;
        }
    }
}
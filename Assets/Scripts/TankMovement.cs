using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public bool needMoveForward;

    [SerializeField] private GameObject reflectWalls;
    [SerializeField] private bool canMoveAutomaticaly;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;

    private Vector3 lastNearestLinePos;
    private int directionSign = 1;

    void Update()
    {
        if (needMoveForward)
        {
            MoveTankForward();
            MoveCameraForward();
            MoveReflectionWallsForward();

            CheckOnDisableMoveForward();
        }

        if (canMoveAutomaticaly)
            MoveHorizontalAutomaticaly();
    }

    public float distanceToTank;
    private void OnMouseDown()
    {
        canMoveAutomaticaly = false;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        distanceToTank = mousePos.x - transform.position.x; 
        Debug.Log("Down");
    }

    private void OnMouseUp()
    {
        distanceToTank = 0;
        canMoveAutomaticaly = true;
    }

    private void OnMouseDrag()
    {
        MoveHorizontalManually();
        //Debug.Log("Up");
    }

    public void CheckOnMoveForward(Vector3 nearestLinePos)
    {
        if ((Mathf.Abs(nearestLinePos.z - transform.position.z) > 10) && (BossesSpawner.Instance.lastSpawnedBoss == null || BossesSpawner.Instance.lastSpawnedBoss.activeSelf == false))
        {
            lastNearestLinePos = nearestLinePos;
            needMoveForward = true;
        }
    }

    private void CheckOnDisableMoveForward()
    {
        if (Mathf.Abs(lastNearestLinePos.z - transform.position.z) <= 10)
        {
            needMoveForward = false;
        }

        if (BossesSpawner.Instance.lastSpawnedBoss != null && Mathf.Abs(BossesSpawner.Instance.lastSpawnedBoss.transform.position.z - transform.position.z) <= 10)
        {
            BossesSpawner.Instance.lastSpawnedBoss.SetActive(true);
            needMoveForward = false;
        }
    }

    private void MoveHorizontalManually()
    {
        //var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //var newCubePosition = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        //transform.position = new Vector3(Mathf.Clamp(newCubePosition.x, leftEdge, rightEdge), transform.position.y, transform.position.z);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.DrawLine(transform.position, mousePos, Color.red); 
        Vector3 newCubePosition = new Vector3(mousePos.x - distanceToTank, transform.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(newCubePosition.x, leftEdge, rightEdge), transform.position.y, transform.position.z);

    }

    private void MoveHorizontalAutomaticaly()
    {
        Vector3 newPos = transform.position + new Vector3(movementSpeed / 2 * directionSign * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(newPos.x, leftEdge, rightEdge), newPos.y, newPos.z);
        if (transform.position.x >= rightEdge || transform.position.x <= leftEdge)
            directionSign *= -1;
    }

    private void MoveTankForward()
    {
        Vector3 newPos = transform.position + new Vector3(0, 0, movementSpeed / 2 * Time.deltaTime);
        transform.position = newPos;
    }

    private void MoveCameraForward()
    {
        Vector3 currentCameraPos = Camera.main.transform.position;
        Vector3 cameraNewPos = new Vector3(0, currentCameraPos.y, currentCameraPos.z + movementSpeed / 2 * Time.deltaTime);
        Camera.main.transform.position = cameraNewPos;
    }

    private void MoveReflectionWallsForward()
    {
        Vector3 wallsPosition = reflectWalls.transform.position;
        Vector3 wallsNewPos = new Vector3(0, wallsPosition.y, wallsPosition.z + movementSpeed / 2 * Time.deltaTime);
        reflectWalls.transform.position = wallsNewPos;
    }
}
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly float minX = -2.75f;
    private readonly float maxX = 2.75f;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private float yMoveSpeed = 2f; //전진 이동 속도

    private float startPointX;
    private float finalPointX;

    private void Awake()
    {
        startPointX = 0;
        finalPointX = 0;
    }

    private void Update()
    {
        MoveToX();
        MoveToY();
    }
    public void MoveToX()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPointX = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
            startPointX -= transform.position.x;
        }

        if (Input.GetMouseButton(0))
        {
            finalPointX = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
            finalPointX -= startPointX;
            finalPointX = Mathf.Clamp(finalPointX, minX, maxX);

            float velocityY = 0f;
            float x = Mathf.SmoothDamp(transform.position.x, finalPointX, ref velocityY, 0.02f);
            transform.position = new Vector3(x, transform.position.y, 0f);
        }
    }

    public void MoveToY()
    {
        transform.position += Vector3.up * yMoveSpeed * Time.deltaTime;
    }


}
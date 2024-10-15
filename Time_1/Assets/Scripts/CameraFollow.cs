using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform coreTarget;
    public Transform playerTarget;
    public Vector3 coreOffset;
    public Vector3 playerOffset;
    [SerializeField] private Camera cam;

    [Header("Values")]
    public float playerZoomSize;
    public float coreZoomSize;
    public float ZoomSpeed = 0.25f;
    public float ZoomTime = 0.2f;
    public float smoothSpeed = 1.0f;


    private bool FLAG;


    private void Awake()
    {
        FLAG = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwapPosition();
        }
    }


    private void FixedUpdate()
    {
        if(FLAG)
        {
            Vector3 desiredPosition = playerTarget.position + playerOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, playerZoomSize,ref ZoomSpeed, ZoomTime);
        }
        else
        {
            Vector3 desiredPosition = coreTarget.position + coreOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, coreZoomSize, ref ZoomSpeed, ZoomTime);
        }
    }

    public void SwapPosition()
    {
        FLAG = !FLAG;
    }
}

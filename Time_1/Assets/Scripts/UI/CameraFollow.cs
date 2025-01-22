using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public Transform coreTarget;
    public Transform playerTarget;
    public Vector3 coreOffset;
    public Vector3 playerOffset;
    [SerializeField] private Camera cam;

    [Header("Values")]
    public float playerZoomSize;
    public float coreZoomSize;
    public float ZoomSpeed = 0f;
    public float ZoomTime = .1f;
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
            player.SetActive(true);
            Vector3 desiredPosition = playerTarget.position + playerOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, playerZoomSize,ref ZoomSpeed, ZoomTime);

            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                playerZoomSize -= scrollInput * 10f;
                playerZoomSize = Mathf.Clamp(playerZoomSize, 1f, 15f);
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, playerZoomSize, ref ZoomSpeed, ZoomTime);
            }
        }
        else
        {
            Vector3 desiredPosition = coreTarget.position + coreOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, coreZoomSize, ref ZoomSpeed, ZoomTime);
            player.SetActive(false);
        }
    }

    public void SwapPosition()
    {
        FLAG = !FLAG;
    }
}

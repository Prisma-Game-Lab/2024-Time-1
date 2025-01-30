using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public Transform coreTarget;
    public Transform playerTarget;
    public Vector3 coreOffset;
    public Vector3 playerOffset;
    public SpriteRenderer playerSprite;
    public Canvas HealthBarSprite;
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
            playerSprite.enabled = true;
            HealthBarSprite.enabled = true;
            Vector3 desiredPosition = playerTarget.position + playerOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, playerZoomSize,ref ZoomSpeed, ZoomTime);

            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                playerZoomSize -= scrollInput * 10f;
                playerZoomSize = Mathf.Clamp(playerZoomSize, 3f, 15f);
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, playerZoomSize, ref ZoomSpeed, ZoomTime);
            }
        }
        else
        {
            Vector3 desiredPosition = coreTarget.position + coreOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, coreZoomSize, ref ZoomSpeed, ZoomTime);
            playerSprite.enabled = false;
            HealthBarSprite.enabled = false;
        }
    }

    public void SwapPosition()
    {
        FLAG = !FLAG;
    }
}

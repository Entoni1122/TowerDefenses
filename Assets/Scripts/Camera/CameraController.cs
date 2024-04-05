using System.Collections;
using UnityEngine;


namespace TowerDefense
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Vars")]
        [SerializeField] float cameraSpeed;
        [SerializeField] float cameraSpeedRotation;
        [SerializeField] float cameraZoomSpeed;
        [SerializeField] float borderTreshold;
        float targetFOV = 80;
        bool bShouldCameraMove;

        [Space]

        [Header("Camera Targets")]
        private Camera CameraRef;
        private Vector3 Direction;

        private void Start()
        {
            CameraRef = GetComponentInChildren<Camera>();
        }
        void Update()
        {
            CalculateCameraMovement();
            if (Input.GetKeyDown(KeyCode.P))
            {
                bShouldCameraMove = !bShouldCameraMove;
            }
            CameraZoom();
            CalculateRotation();
        }
        void CalculateCameraMovement()
        {
            if (bShouldCameraMove)
            {
                Direction = Vector3.zero;
                if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - borderTreshold)
                {
                    Direction.z = +1;
                }
                if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= borderTreshold)
                {
                    Direction.z = -1;
                }
                if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - borderTreshold)
                {
                    Direction.x = +1;
                }
                if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= borderTreshold)
                {
                    Direction.x = -1;
                }

                Vector3 moveCamera = transform.forward * Direction.z + transform.right * Direction.x;
                transform.position += moveCamera * cameraSpeed * Time.deltaTime;
            }
        }
        void CalculateRotation()
        {
            float rotatedir = 0;
            if (Input.GetKey(KeyCode.Q))
            {
                rotatedir += 1;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotatedir -= 1;
            }
            transform.eulerAngles += new Vector3(0, rotatedir * cameraSpeedRotation * Time.deltaTime, 0);
        }
        void CameraZoom()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                targetFOV -= 5f;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                targetFOV += 5f;
            }
            targetFOV = Mathf.Clamp(targetFOV, 15, 60);

            CameraRef.fieldOfView = Mathf.Lerp(CameraRef.fieldOfView, targetFOV, cameraZoomSpeed * Time.deltaTime);
        }
    }
}

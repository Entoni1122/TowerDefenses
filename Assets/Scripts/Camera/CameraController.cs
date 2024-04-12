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
        float targetFOV = 60;
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
            //CalculateCameraMovement();
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    bShouldCameraMove = !bShouldCameraMove;
            //}
            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    CameraZoom();
            //}

            //CalculateRotation();
        }
        //void CalculateCameraMovement()
        //{
        //    if (bShouldCameraMove)
        //    {
        //        Direction = Vector3.zero;
        //        if (Input.GetKey(KeyCode.W))
        //        {
        //            Direction.z = +1;
        //        }
        //        if (Input.GetKey(KeyCode.S))
        //        {
        //            Direction.z = -1;
        //        }
        //        if (Input.GetKey(KeyCode.D))
        //        {
        //            Direction.x = +1;
        //        }
        //        if (Input.GetKey(KeyCode.A))
        //        {
        //            Direction.x = -1;
        //        }

        //        Vector3 moveCamera = transform.forward * Direction.z + transform.right * Direction.x;
        //        transform.position += moveCamera * cameraSpeed * Time.deltaTime;
        //    }
        //}
        //void CalculateRotation()
        //{
        //    float rotatedir = 0;
        //    if (Input.GetKey(KeyCode.Q))
        //    {
        //        rotatedir += 1;
        //    }
        //    if (Input.GetKey(KeyCode.E))
        //    {
        //        rotatedir -= 1;
        //    }
        //    transform.eulerAngles += new Vector3(0, rotatedir * cameraSpeedRotation * Time.deltaTime, 0);
        //}
        bool shouldZoom;
        public void CameraZoom()
        {
            targetFOV = shouldZoom ? targetFOV = 60 : 90;

            CameraRef.fieldOfView = targetFOV;
            shouldZoom = !shouldZoom;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class RotateEarth : MonoBehaviour
    {
        public float PCRotationSpeed = 10f;
        public float MobileRotationSpeed = 0.4f;
        //Drag the camera object here
        public Camera cam;
        public Transform objToRotate;

        void OnMouseDrag()
        {
            float rotX = Input.GetAxis("Mouse X") * PCRotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * PCRotationSpeed;

            Vector3 right = Vector3.Cross(cam.transform.up, objToRotate.position - cam.transform.position);
            Vector3 up = Vector3.Cross(objToRotate.position - cam.transform.position, right);
            objToRotate.rotation = Quaternion.AngleAxis(-rotX, up) * objToRotate.rotation;
            objToRotate.rotation = Quaternion.AngleAxis(rotY, right) * objToRotate.rotation;
        }

        void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                Ray camRay = cam.ScreenPointToRay(touch.position);
                RaycastHit raycastHit;
                if (Physics.Raycast(camRay, out raycastHit, 100,LayerMask.NameToLayer("Earth")))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Debug.Log("Touch phase began at: " + touch.position);
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        Debug.Log("Touch phase Moved");
                        objToRotate.Rotate(0,-touch.deltaPosition.x * MobileRotationSpeed, 0, Space.World);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        Debug.Log("Touch phase Ended");
                    }
                }
            }
        }
    }
}

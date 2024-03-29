using System.Collections;
using UnityEngine;


namespace TowerDefense
{
    public class CameraController : MonoBehaviour
    {
        public float cameraSpeed;

        public float borderTreshold;

        private bool bShouldCameraMove = true;

        void Update()
        {
            if (bShouldCameraMove)
            {
                if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - borderTreshold)
                {
                    transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= borderTreshold)
                {
                    transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - borderTreshold)
                {
                    transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime, Space.World);
                }
                if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= borderTreshold)
                {
                    transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime, Space.World);
                }
            }
            if (Input.GetKey(KeyCode.P))
            {
                bShouldCameraMove = !bShouldCameraMove;
            }
        }
    }
}

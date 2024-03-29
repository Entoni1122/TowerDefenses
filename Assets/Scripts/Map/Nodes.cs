using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Collider))]
    public class Nodes : MonoBehaviour
    {
        [Header("On Mouse Hover Color")]
        public Color HoverColor;
        private Color startingColor;
        private Renderer objRend;

        [Header("Build Child")]
        [SerializeField] GameObject TurretBuildOn;

        public float costToBuild;

        public Vector3 postoTo;
        public Vector3 startingPos;

        private void Start()
        {
            objRend = GetComponent<Renderer>();
            startingColor = objRend.material.color;
            startingPos = transform.position;
            TurretBuildOn = null;
        }

        public void Init(Vector3 pos,float yScale)
        {
            postoTo = pos;
            print(yScale);
            transform.localScale = new Vector3(transform.localScale.x,yScale,transform.localScale.z);
        }
        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position,postoTo,Time.deltaTime * 5);
            float dist = Vector3.Distance(transform.position, postoTo);   
            if (dist <= 0.4f)
            {
                transform.position = postoTo;
            }
        }

        private void OnMouseEnter()
        {
            objRend.material.color = HoverColor;
        }
        private void OnMouseExit()
        {
            GetComponent<MeshRenderer>().material.color = startingColor;
        }

        private void OnMouseDown()
        {
            if (TurretBuildOn != null)
            {
                print("Already an object on top");
                return;
            }
            else if (PlayerStats.Instance.Coin >= costToBuild)
            {
                PlayerStats.Instance.RemoveMoney(costToBuild);
                GameObject turret = BuildingManager.Instance.GetTurretToBuild();
                TurretBuildOn = Instantiate(turret, transform.position + new Vector3(0, 1, 0), transform.rotation);
                return;
            }
            print("Not enough money");

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Spawner"))
            {
                print("SomeOneEntered");
                Destroy(gameObject);
            }
        }
    }
}

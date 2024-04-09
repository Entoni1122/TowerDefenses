using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Collider))]
    public class Nodes : MonoBehaviour, I_interactable
    {
        [Header("On Mouse Hover Color")]
        public Color HoverColor;
        private Color startingColor;
        private Renderer objRend;

        [Header("Build Child")]
        [SerializeField] GameObject TurretBuildOn;
        public float costToBuild;

        [Header("Utilities")]
        public Vector3 PosToGo;
        public Vector3 startingPos;
        Color colors;

        public PathNode pathNode;

        private void Awake()
        {
            pathNode = new PathNode();
        }
        private void Start()
        {
            objRend = GetComponent<Renderer>();
            startingColor = objRend.material.color;
            startingPos = transform.position;
            TurretBuildOn = null;
            GetComponent<MeshRenderer>().sharedMaterial.color = colors;
        }

        public void Init(Vector3 pos, float yScale, Color color)
        {
            //PosToGo = pos;
            //colors = color;
            //transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
        }
        //private void Update()
        //{
        //    //transform.position = Vector3.Lerp(transform.position, PosToGo, Time.deltaTime * 5);
        //    //float dist = Vector3.Distance(transform.position, PosToGo);
        //    //if (dist <= 0.4f)
        //    //{
        //    //    transform.position = PosToGo;
        //    //}
        //}

        #region MouseBehaviour
        public void OnMouseHover()
        {
            objRend.material.color = HoverColor;
        }
        private void OnMouseExit()
        {
            GetComponent<MeshRenderer>().material.color = colors;
            
        }
        public void OnMouseLeftClick()
        {
            if (TurretBuildOn != null)
            {
                print("Already an object on top");
                return;
            }
            else if (PlayerStats.Instance.Coin >= costToBuild)
            {
                GameObject turret = BuildingManager.Instance.GetTurretToBuild();
                TurretBuildOn = Instantiate(turret, transform.position + new Vector3(0, 20f, 0), transform.rotation,transform);
                float cost = turret.GetComponent<Buildable>().costToBuild;
                PlayerStats.Instance.RemoveMoney(cost);
                return;
            }
            print("Not enough money");

        }

        public void OnMouseRightClick()
        {
            print("Right clicking doesnt do shit......................for now");
        }

        public void OnMouseUnhover()
        {
            
        }
        #endregion
    
    }
}

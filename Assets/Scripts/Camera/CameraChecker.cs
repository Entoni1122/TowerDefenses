using TMPro;
using UnityEngine;


namespace TowerDefense
{
    public class CameraChecker : MonoBehaviour
    {
        [SerializeField] GameObject upgradeMenu;
        [SerializeField] TextMeshProUGUI upgradeButton;
        [SerializeField] TextMeshProUGUI sellButton;

        GameObject Target;

        void Update()
        {
            CheckForInteractable();
        }
        void CheckForInteractable()
        {
            RaycastHit hit;

            Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 200f, Color.red);

            if (Physics.Raycast(transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit, 200f))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Turret"))
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        Target = hit.transform.gameObject;
                        upgradeMenu.SetActive(true);
                        upgradeMenu.transform.position = Input.mousePosition;
                        UpdateTurretData(hit.transform.gameObject);
                        return;
                    }
                }
            }
        }
        public void ButtonClicked()
        {
            if (Target != null)
            {
                Turretbehaviour turet = Target.GetComponent<Turretbehaviour>();
                turet.UpgradeTurret();
                UpdateTurretData(turet.gameObject);
                return;
            }

        }
        public void ButtonSell()
        {
            if (Target != null)
            {
                Turretbehaviour turet = Target.GetComponent<Turretbehaviour>();
                upgradeMenu.SetActive(false);
                PlayerStats.Instance.AddMoney(turet.SellCost);
                Destroy(turet.gameObject);
                return;
            }

        }
        void UpdateTurretData(GameObject hit)
        {
            Turretbehaviour turretData = hit.transform.GetComponent<Turretbehaviour>();
            upgradeButton.text = "Upgrade: " + turretData.UpgradeCost.ToString();
            sellButton.text = "Sell: " + turretData.SellCost.ToString();
        }
    }
}

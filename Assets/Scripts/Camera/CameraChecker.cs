using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    public class CameraChecker : MonoBehaviour
    {
        [SerializeField] GameObject upgradeMenu;
        [SerializeField] TextMeshProUGUI upgradeButton;
        [SerializeField] TextMeshProUGUI sellButton;
        [SerializeField] GameObject CheckRadiusPrefab;
        GameObject Target;
        private void Start()
        {
            CheckRadiusPrefab = Instantiate(CheckRadiusPrefab);
            CheckRadiusPrefab.SetActive(false);
        }
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
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("HexagonNode"))
                    {
                        if (hit.transform.gameObject != null)
                        {
                            hit.transform.GetComponent<Nodes>().OnMouseHover();
                            if (Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                hit.transform.GetComponent<Nodes>().OnMouseLeftClick();
                                ActivateUpgradeMenu(false);
                            }
                        }
                    }

                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Turret"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                        {
                            Target = hit.transform.gameObject;
                            ActivateUpgradeMenu(true);
                            UpdateTurretData(hit.transform.gameObject);
                            return;
                        }
                    }
                }
            }
        }
        void ActivateUpgradeMenu(bool show)
        {
            if (!show)
            {
                upgradeMenu.SetActive(false);
                CheckRadiusPrefab.SetActive(false);
                return;
            }
            upgradeMenu.SetActive(true);
            upgradeMenu.transform.position = Input.mousePosition;
        }
        void UpdateTurretData(GameObject hit)
        {
            Turretbehaviour turretData = hit.transform.GetComponent<Turretbehaviour>();
            CheckRadiusPrefab.SetActive(true);
            CheckRadiusPrefab.transform.position = hit.transform.position;
            CheckRadiusPrefab.transform.localScale = new Vector3(turretData.CheckForEnemiesRadius, turretData.CheckForEnemiesRadius, turretData.CheckForEnemiesRadius) * 2;
            upgradeButton.text = "Upgrade: " + turretData.UpgradeCost.ToString();
            sellButton.text = "Sell: " + turretData.SellCost.ToString();
        }

        #region ButtonFunctionality
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
        public void ExitMenuButton()
        {
            ActivateUpgradeMenu(false);
        }
        #endregion


    }
}

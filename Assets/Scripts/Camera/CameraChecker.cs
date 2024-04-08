using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace TowerDefense
{
    public class CameraChecker : MonoBehaviour
    {
        [Header("Turrets UI")]
        [SerializeField] TextMeshProUGUI upgradeButton;
        [SerializeField] TextMeshProUGUI sellButton;
        [SerializeField] GameObject upgradeMenu;
        [SerializeField] GameObject CheckRadiusPrefab;

        [Space]

        [Header("Single Turrets PowerUp")]
        [SerializeField] TextMeshProUGUI turretLevelTXT;
        [SerializeField] TextMeshProUGUI buildingFirstRowTXT;
        [SerializeField] TextMeshProUGUI buildingFirstSecondTXT;
        [SerializeField] TextMeshProUGUI buildingFirstThirdTXT;


        GameObject CameraTargetFound;

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
                            CameraTargetFound = hit.transform.gameObject;
                            ActivateUpgradeMenu(true);
                            UpdateTurretData(hit.transform.gameObject);
                            return;
                        }
                    }

                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Building"))
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse1))
                        {
                            CameraTargetFound = hit.transform.gameObject;
                            ActivateUpgradeMenu(true);
                            UpdateStructure(hit.transform.gameObject);
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
                return;
            }
            upgradeMenu.SetActive(true);
        }
        void UpdateTurretData(GameObject hit)
        {
            Turretbehaviour turretData = hit.transform.GetComponent<Turretbehaviour>();
            CheckRadiusPrefab.SetActive(true);
            CheckRadiusPrefab.transform.position = hit.transform.position;
            CheckRadiusPrefab.transform.localScale = new Vector3(turretData.CheckForEnemiesRadius, turretData.CheckForEnemiesRadius, turretData.CheckForEnemiesRadius) * 2;
            turretLevelTXT.text = "Level " + turretData.Level.ToString();
            upgradeButton.text = "Upgrade:\n" + turretData.UpgradeCost.ToString();
            sellButton.text = "Sell:\n" + turretData.SellCost.ToString();
            buildingFirstRowTXT.text = "Damage: " + turretData.DMG.ToString();
            buildingFirstSecondTXT.text = "Range: " + turretData.CheckForEnemiesRadius.ToString("00");
            buildingFirstThirdTXT.text = "PawRate: " + turretData.FireRate.ToString("00");
        }
        void UpdateStructure(GameObject hit)
        {
            Structure turretData = hit.transform.GetComponent<Structure>();
            turretLevelTXT.text = "Level " + turretData.Level.ToString();
            upgradeButton.text = "Upgrade:\n" + turretData.UpgradeCost.ToString();
            sellButton.text = "Sell:\n" + turretData.SellCost.ToString();
            buildingFirstRowTXT.text = "GoldRate: " + turretData.CoinGainRate.ToString();
            buildingFirstSecondTXT.text = "GoldGain: " + turretData.CoinGainded.ToString();
            buildingFirstThirdTXT.text = " ";
        }

        #region ButtonFunctionality
        public void ButtonClicked()
        {
            if (CameraTargetFound != null)
            {
                Buildable turet = CameraTargetFound.GetComponent<Buildable>();
                if (turet.isTurret)
                {
                    turet.UpgradeBuilding();
                    UpdateTurretData(turet.gameObject);
                    return;
                }
                turet.UpgradeBuilding();
                UpdateStructure(turet.gameObject);
            }
        }
        public void ButtonSell()
        {
            if (CameraTargetFound != null)
            {
                Buildable turet = CameraTargetFound.GetComponent<Buildable>();
                upgradeMenu.SetActive(false);
                CheckRadiusPrefab.SetActive(false);
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

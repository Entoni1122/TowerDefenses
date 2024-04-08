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
        [SerializeField] TextMeshProUGUI turretDmgTXT;
        [SerializeField] TextMeshProUGUI turretRangeTXT;
        [SerializeField] TextMeshProUGUI turretFireRateTXT;

        [Header("Tendine REF")]
        [SerializeField] Button TendinaTurretsRef;
        [SerializeField] Button TendinaStructuresRef;
        Animation AnimationTurretsRef;
        Animation AnimationStructuresRef;

        GameObject CameraTargetFound;

        private void Start()
        {
            CheckRadiusPrefab = Instantiate(CheckRadiusPrefab);
            CheckRadiusPrefab.SetActive(false);

            AnimationTurretsRef = TendinaTurretsRef.gameObject.GetComponent<Animation>();
            AnimationStructuresRef = TendinaStructuresRef.gameObject.GetComponent<Animation>();

            TendinaTurretsRef.onClick.AddListener(delegate { ButtonTendinaClicked(AnimationTurretsRef); });
            TendinaStructuresRef.onClick.AddListener(delegate { ButtonTendinaClicked(AnimationStructuresRef); });
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
                            CameraTargetFound = hit.transform.gameObject;
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
        }
        void UpdateTurretData(GameObject hit)
        {
            Turretbehaviour turretData = hit.transform.GetComponent<Turretbehaviour>();
            CheckRadiusPrefab.SetActive(true);
            CheckRadiusPrefab.transform.position = hit.transform.position;
            CheckRadiusPrefab.transform.localScale = new Vector3(turretData.CheckForEnemiesRadius, turretData.CheckForEnemiesRadius, turretData.CheckForEnemiesRadius) * 2;
            upgradeButton.text = "Upgrade:\n" + turretData.UpgradeCost.ToString();
            sellButton.text = "Sell:\n" + turretData.SellCost.ToString();
            turretDmgTXT.text = "Damage: " + turretData.DMG.ToString();
            turretRangeTXT.text = "Range: " + turretData.CheckForEnemiesRadius.ToString("00");
            turretFireRateTXT.text = "PawRate: " + turretData.FireRate.ToString("00");
            turretLevelTXT.text = "Level " + turretData.turretLevel.ToString();
        }

        #region ButtonFunctionality

        int animIndex;
        public void ButtonTendinaClicked(Animation buttonRef)
        {
            if (!buttonRef.isPlaying)
            {
                buttonRef.Play();
            }
        }



        public void ButtonClicked()
        {
            if (CameraTargetFound != null)
            {
                Turretbehaviour turet = CameraTargetFound.GetComponent<Turretbehaviour>();
                turet.UpgradeTurret();
                UpdateTurretData(turet.gameObject);
                return;
            }

        }
        public void ButtonSell()
        {
            if (CameraTargetFound != null)
            {
                Turretbehaviour turet = CameraTargetFound.GetComponent<Turretbehaviour>();
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

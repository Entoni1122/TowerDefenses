using UnityEngine;
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    public GameObject stdTurretToBuild;
    private GameObject currentTurret;

    void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        currentTurret = stdTurretToBuild;
    }
    public GameObject GetTurretToBuild()
    {
        return currentTurret;
    }
}

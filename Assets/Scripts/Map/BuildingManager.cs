using System.Collections.Generic;
using UnityEngine;
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    public GameObject stdTurretToBuild;

    public List<GameObject> turrets = new List<GameObject>();

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

    public void SetCurrentToMouseClickedOption(int b)
    {
        currentTurret = turrets[b];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

namespace TowerDefense
{
    public class Structure : Buildable
    {
        float time = 0;
        public float CoinGainRate = 10;
        public float CoinGainded;

        public override void Start()
        {
            base.Start();
            isTurret = false;
        }

        public override void Update()
        {
            base.Update();
            time -= Time.deltaTime;
            if (time <=  0)
            {
                GetGold();
                time = CoinGainRate;
            }
        }

        public void GetGold()
        {
            PlayerStats.Instance.AddMoney(CoinGainded);
        }
        public override void UpgradeBuilding()
        {
            base.UpgradeBuilding();
            if (PlayerStats.Instance.Coin >= UpgradeCost)
            {
                PlayerStats.Instance.Coin -= UpgradeCost;
                UpgradeCost += 50;
                CoinGainded += 50;
                CoinGainRate -= 0.5f;
                SellCost += UpgradeCost * 0.5f;
                PlayerStats.Instance.UdpateStats();
            }
        }
    }
}

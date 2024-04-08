using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace TowerDefense
{

    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats Instance;

        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI CoinTXT;
        [SerializeField] TextMeshProUGUI HpTXT;

        public float Coin;
        public int HP;

        public int WaveCount;
        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
        }
        private void Start()
        {
            UdpateStats();
        }
        public void AddMoney(float b)
        {
            Coin += b;
            UdpateStats();
        }
        public void RemoveMoney(float b)
        {
            Coin -= b;
            UdpateStats();
        }

        public void UdpateStats()
        {
            CoinTXT.text = "$ " + Coin.ToString();
            HpTXT.text = "<3 " + HP.ToString();
        }
    }
}

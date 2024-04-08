using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TendinaAnimationManager : MonoBehaviour
    {
        Animator _animator;

        [SerializeField] private Button turrets;
        [SerializeField] private Button structures;

        [SerializeField] private Button generalTendina;

        [SerializeField] GameObject turretsScelta;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            turrets.onClick.AddListener(ButtonTurret);
            //structures.onClick.AddListener(delegate { OnClick("TurretsTrigger"); });
            generalTendina.onClick.AddListener(delegate { OnClick("TurretsTrigger"); });
        }
        public void OnClick(string name)
        {
            _animator.SetTrigger(name);
        }

        bool showTurrets = true;
        public void ButtonTurret()
        {
            if (showTurrets)
            {
                turretsScelta.SetActive(false);
            }
            else
            {
                turretsScelta.SetActive(true);
            }
            showTurrets = !showTurrets;
        }
    }
}

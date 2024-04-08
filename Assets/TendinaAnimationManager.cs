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
        [SerializeField] GameObject structureScelta;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            turrets.onClick.AddListener(delegate { ButtonTurret(turretsScelta, structureScelta); });
            structures.onClick.AddListener(delegate { ButtonTurret(structureScelta, turretsScelta); });
            generalTendina.onClick.AddListener(delegate { OnClick("TurretsTrigger"); });
        }
        public void OnClick(string name)
        {
            _animator.SetTrigger(name);
        }

        bool showTurrets = true;
        public void ButtonTurret(GameObject toActivate,GameObject toDeactivate)
        {
            toActivate.SetActive(true);
            toDeactivate.SetActive(false);
        }
    }
}

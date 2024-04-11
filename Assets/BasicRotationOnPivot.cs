using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace TowerDefense
{
    public class BasicRotationOnPivot : MonoBehaviour
    {
        [SerializeField] float Speed;

        void Update()
        {
            transform.eulerAngles += Vector3.up * Speed * Time.deltaTime;
        }
    }
}

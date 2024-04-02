using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    interface I_interactable
    {
        void OnMouseHover();
        void OnMouseUnhover();
        void OnMouseLeftClick();
        void OnMouseRightClick();

    }
}

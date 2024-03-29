using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static Transform[] nodes;

    private void Awake()
    {
        nodes = new Transform[transform.childCount];

        for(int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = transform.GetChild(i);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] Material material;

    public void Update()
    {
        material.SetVector("_offset",new Vector2 (0,-transform.position.z));
    }
}

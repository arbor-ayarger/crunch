using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLight : MonoBehaviour
{
    public static List<IsLight> lights = new List<IsLight>();

    void Awake()
    {
        lights.Add(this);
    }

    private void OnDestroy()
    {
        lights.Remove(this);
    }
}

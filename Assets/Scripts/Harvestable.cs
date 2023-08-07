using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public void Harvest()
    {
        Destroy(gameObject);
    }
}

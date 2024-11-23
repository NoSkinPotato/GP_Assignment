using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public float hitToDie = 3;
    public PlantType plantType;
}

public enum PlantType
{
    wheat, tomato
}

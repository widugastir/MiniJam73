using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public int Result (int diseMaxValue)
    {
        int trigger = (int)Random.Range(1, diseMaxValue + 1);
        return trigger;
    }

    
}

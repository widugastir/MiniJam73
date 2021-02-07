using UnityEngine;

public class DiceController : MonoBehaviour
{
    public static int RollD6 ()
    {
        return Random.Range(1, 7);
    }

    
}

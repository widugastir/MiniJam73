using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesController : MonoBehaviour
{
    private List<SOQuest> PullQest;
    private int CurrentQuest;
    private void Start()
    {
        PullQest = new List<SOQuest>();
    }

    private void ChengeCurrentQuest(int amount)
    {
       
    }

    private void OnEnable()
    {
        //GameManager.Instance.OnResourceChange += ChengeCurrentQuest;
    }

    private void OnDisable()
    {
        //GameManager.Instance.OnResourceChange -= ChengeCurrentQuest;
    }

    
}

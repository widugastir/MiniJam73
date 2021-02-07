using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingController : MonoBehaviour
{
    [SerializeField] private GameObject [] _trainingPanels;
    public GameObject [] _buttonsInfo;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Training", 0) != 1)
            Invoke(nameof(SetUnsetActivePanel), 1f);
    }

    public void SetUnsetActivePanel(int index)
    {
        if(_trainingPanels[index].activeSelf == false)
        {
            _trainingPanels[index].SetActive(true);
            _buttonsInfo[index].GetComponent<Animator>().enabled = true;
        }
        else
        {
        _trainingPanels[index].SetActive(false);
        _buttonsInfo[index].GetComponent<Animator>().enabled = false;
        if(PlayerPrefs.GetInt("Training") != 1) 
            PlayerPrefs.SetInt("Training", 1);
        }
    }

    public void SetUnsetActivePanel()
    {
        SetUnsetActivePanel(0);
    }

    public void DeleteALLKey ()
    {
        PlayerPrefs.DeleteAll();
    }
}

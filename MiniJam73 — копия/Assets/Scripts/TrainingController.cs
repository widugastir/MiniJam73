using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingController : MonoBehaviour
{
    [SerializeField] private GameObject [] _trainingPanels;
    public GameObject [] _buttonsInfo;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Training", 0) != 2)
            Invoke(nameof(SetUnsetActivePanel), 1f);
    }

    public void SetUnsetActivePanel(int index)
    {
        print("go!");
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

    public void SetPlayerPrefsMap()
    {
        if(PlayerPrefs.GetInt("TrainingMap") != 1) 
        {
            PlayerPrefs.SetInt("TrainingMap", 1);
        }
    }
    public void SetPlayerPrefsBattle()
    {
        if(PlayerPrefs.GetInt("TrainingBattle") != 1) 
        {
            PlayerPrefs.SetInt("TrainingBattle", 1);
        }
    }

    public void DeleteALLKey ()
    {
        PlayerPrefs.DeleteAll();
    }
}

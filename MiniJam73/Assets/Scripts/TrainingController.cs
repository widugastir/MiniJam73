using UnityEngine.UI;
using UnityEngine;

public class TrainingController : MonoBehaviour
{
    [SerializeField] private GameObject [] _trainingPanels;
    [SerializeField] private GameObject _map;
    [SerializeField] private ScrollRect _mapScroll;
    private Vector3 _baseMapPosition;
    public GameObject [] _buttonsInfo;

    private void Start()
    {
        _baseMapPosition = _map.transform.position;
        if (PlayerPrefs.GetInt("Training", 0) != 1)
            SetUnsetActivePanel(0);
    }

    public void SetUnsetActivePanel(int index)
    {
        if(_trainingPanels[index].activeSelf == false)
        {
            _map.transform.position = _baseMapPosition;
            _mapScroll.velocity = Vector2.zero;
            Time.timeScale = 0f;
            _trainingPanels[index].SetActive(true);
            _buttonsInfo[index].GetComponent<Animator>().enabled = true;
        }
        else
        {
            Time.timeScale = 1f;
            _trainingPanels[index].SetActive(false);
                _buttonsInfo[index].GetComponent<Animator>().enabled = false;
            if(PlayerPrefs.GetInt("Training") != 1) 
                PlayerPrefs.SetInt("Training", 1);
        }
    }

    public void DeleteALLKey ()
    {
        PlayerPrefs.DeleteAll();
    }
}

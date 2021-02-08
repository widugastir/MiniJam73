using UnityEngine.UI;
using UnityEngine;

public class TrainingController : MonoBehaviour
{
    [SerializeField] private GameObject [] _trainingPanels;
    [SerializeField] private GameObject _map;
    [SerializeField] private ScrollRect _mapScroll;
    private Vector3 _baseMapPosition;
    public GameObject [] _buttonsInfo;
    [SerializeField] private GameObject [] _startTrainingButtons;
    [SerializeField] private GameObject _startPanelInfo;
    private bool _startTrigger = false;

    private void Start()
    {
        _baseMapPosition = _map.transform.position;
        if (PlayerPrefs.GetInt("Training", 0) != 1)
        {
            
            _startTrigger = true;
            SetUnsetActivePanel(0);
            _startPanelInfo.GetComponent<Button>().enabled = true;
        }
    }

    public void SetUnsetActivePanel(int index)
    {
        if(_trainingPanels[index].activeSelf == false)
        {
            if(_startTrigger)
            {
                foreach(GameObject i in _startTrainingButtons)
                {
                    i.SetActive(false);
                }
            }
            _map.transform.position = _baseMapPosition;
            _mapScroll.velocity = Vector2.zero;
            Time.timeScale = 0f;
            _trainingPanels[index].SetActive(true);
            if(!_startTrigger)
                _buttonsInfo[index].GetComponent<Animator>().enabled = true;
            else
            {
                _startTrainingButtons[0].SetActive(true);
                //ClicChangeBlokTraining();
            }
        }
        else
        {
            Time.timeScale = 1f;
            _trainingPanels[index].SetActive(false);
                _buttonsInfo[index].GetComponent<Animator>().enabled = false;
            if(_startTrigger)
            {
                foreach(GameObject i in _startTrainingButtons)
                {
                    i.SetActive(true);
                }
                _startTrigger = false;
            }
            if(PlayerPrefs.GetInt("Training") != 1) 
                PlayerPrefs.SetInt("Training", 1);
        }
    }

    public void ClicChangeBlokTraining()
    {
        for(int i = 0; i < _startTrainingButtons.Length; i++)
        {
            if(_startTrainingButtons[i].activeSelf == true && i < _startTrainingButtons.Length - 1) 
            {
                print("go" + i);
                _startTrainingButtons[i].SetActive(false);
                _startTrainingButtons[i + 1].SetActive(true);
                if(i == _startTrainingButtons.Length - 2)
                {
                    _buttonsInfo[0].GetComponent<Animator>().enabled = true;
                    _startPanelInfo.GetComponent<Button>().enabled = false;
                }
                break;
            }
        }
    }

    public void DeleteALLKey ()
    {
        PlayerPrefs.DeleteAll();
    }
}

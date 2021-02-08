using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Inctance;
    [SerializeField] private GameObject _initialPanel;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private GameObject _exitPanel;
    [SerializeField] private TextMeshProUGUI _messageText;
    

    private GameObject _lastPanel;

    private void Awake() 
    {
        if(Inctance != null)
        {
            Destroy(gameObject);
            return;
        }
        Inctance = this;
    }

    private void Start()
    {
        EnablePanel(_initialPanel);
    }
    public void ExitPanel (bool trigger)
    {
        if(!trigger) 
            _exitPanel.SetActive(true);
        else
            _exitPanel.SetActive(false);
    }

    public void SetMessagePanel(string text)
    {
        _messageText.text = text;
        _messagePanel.SetActive(true);
        StartCoroutine(DsableMessagePanel());
    }

    private System.Collections.IEnumerator DsableMessagePanel()
    {
        yield return new WaitForSecondsRealtime(3f);
        _messagePanel.SetActive(false);
    }

    public void EnablePanel(GameObject panel, bool closePanel = true)
    {
        if (_lastPanel != null && closePanel)
            _lastPanel.SetActive(false);
        _lastPanel = panel;
        _lastPanel.SetActive(true);
    }
    public void EnablePanel(GameObject panel)
    {
        if (_lastPanel != null)
            _lastPanel.SetActive(false);
        _lastPanel = panel;
        _lastPanel.SetActive(true);
    }
    public void LoadNullLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

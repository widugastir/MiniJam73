using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Inctance;
    [SerializeField] private GameObject _initialPanel;
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
}

using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _initialPanel;
    private GameObject _lastPanel;

    private void Start()
    {
        EnablePanel(_initialPanel);
    }

    public void EnablePanel(GameObject panel)
    {
        if (_lastPanel != null)
            _lastPanel.SetActive(false);
        _lastPanel = panel;
        _lastPanel.SetActive(true);
    }
}

using UnityEngine.UI;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] private GameManager.ResourceType _resourceType;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnResourceChange += OnResourceChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnResourceChange -= OnResourceChange;
    }

    private void OnResourceChange(GameManager.ResourceType type, int amount)
    {
        if(_resourceType == type)
            _text.text = "" + amount;
    }
}

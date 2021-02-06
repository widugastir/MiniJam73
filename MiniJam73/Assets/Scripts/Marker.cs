using UnityEngine;

public class Marker : MonoBehaviour
{
    public int StarCounter;
    [SerializeField] private GameObject [] _stars;

    public void Start ()
    {
        for(int i = 0; i < StarCounter; i++)
        {
            _stars[i].SetActive(true);
        }
    }
    public void SetActivePanel(EventPanel panel)
    {
        panel.SelectedMarker = this;
        panel.gameObject.SetActive(true);
        SelectionMinions.Instance.TargetMarker = this;
    }

    public void ChangeStars()
    {
        for(int i = 0; i < _stars.Length; i++)
        {
            _stars[i].SetActive(false);
        }
    }
}

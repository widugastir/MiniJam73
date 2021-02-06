using UnityEngine;

public class Marker : MonoBehaviour
{
    public int StarCount;
    public void SetActivePanel(EventPanel panel)
    {
        panel.SelectedMarker = this;
        panel.gameObject.SetActive(true);
        SelectionMinions.Instance.TargetMarker = this;
        print(this + " SetActivePanel");
    }
}

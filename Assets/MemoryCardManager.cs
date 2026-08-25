using UnityEngine;

public class AvoidUIManager : MonoBehaviour
{
    public GameObject popup;

    public void ShowPopup()
    {
        popup.SetActive(true);
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }
}
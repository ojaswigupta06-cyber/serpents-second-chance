using UnityEngine;

public class HangmanButton : MonoBehaviour
{
    public GameObject rulesPopup;

    public void ShowRules()
    {
        rulesPopup.SetActive(true);
    }

    public void HideRules()
    {
        rulesPopup.SetActive(false);
    }
}
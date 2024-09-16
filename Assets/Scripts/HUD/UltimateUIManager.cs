using UnityEngine;
using UnityEngine.UI;

public class UltimateUIManager : MonoBehaviour
{
    private Color gray = Color.gray;

    private Color orange = new Color(255f / 255f, 185f / 255f, 21f / 255f, 1f);

    void Start()
    {
        GetComponent<Image>().color = gray;
    }

    public void OnChargeChange(bool val)
    {
        if (val)
        {
            GetComponent<Image>().color = orange;
        }
        else
        {
            GetComponent<Image>().color = gray;
        }
    }
}
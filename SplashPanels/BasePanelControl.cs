using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePanelControl : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text body;
    public Image image;
    public Button yes;
    public Button no;
    public abstract void FillData(BasePanelData data);

}

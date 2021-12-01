using UnityEngine;
using UnityEngine.UI;

public class Icons : MonoBehaviour
{
    public static Icons instance;

    public Image ThatlLearnYa;
    public Image Doh;
    public Image MegaSpace;

    public Image QuestionMark;

    public Image eateor;
    public Image meatemor;
    public Image heateor;
    public Image seateor;
    public Image feeteor;
    public Image beatleor;
    public Image beeteor;
    public Image sheteor;
    public Image sheeteor;
    public Image teateor;
    public Image animeteor;

    public Image MoonageDaveDream;
    public Image Pluto;
    public Image Mars;
    public Image Earth;
    public Image Uranus;
    public Image Saturn;
    public Image Jupiter;
    public Image Sun;

    public Image coin;
    public Image thumb;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}

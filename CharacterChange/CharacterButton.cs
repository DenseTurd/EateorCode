using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public int charIndex;
    public Button button;
    public void ChangeCharacter()
    {
        MeshSwapper.instance.MeshSwap(charIndex);

        AudioManager.instance.Click();

        UiController.instance.ClosePanel();
    }
}

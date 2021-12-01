using UnityEngine.UI;
public class BasePanelData
{
    public PanelType Type { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public Image Image { get; set; }
}

public enum PanelType
{
    Text,
    Image,
    TextAndImage,
    Swipe,
    ShortHighText,
    ShortLowText,
    PrizeText,
    Choice
}

public class ChoicePanelData : BasePanelData
{
    public delegate void Del();
    public Del Yes;
    public Del No;
    public ChoicePanelData()
    {
        Type = PanelType.Choice;
    }
}

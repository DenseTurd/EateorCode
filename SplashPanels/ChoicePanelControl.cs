public class ChoicePanelControl : BasePanelControl
{
    public delegate void DelegateMethod();
    ChoicePanelData _data;

    public override void FillData(BasePanelData data)
    {
        title.text = data.Title;
        body.text = data.Body;
        _data = data as ChoicePanelData;
    }

    public void Yes()
    {
        _data.Yes();
        Confirm();
        AudioManager.instance.Click();
    }
    public void No()
    {
        _data.No();
        Confirm();
        AudioManager.instance.Click();
    }

    void Confirm()
    {
        SplashManager.instance.CheckLastPanelClosed();
        SplashManager.instance.DisplayNextGeneralPanel();
        Destroy(gameObject);
    }
}

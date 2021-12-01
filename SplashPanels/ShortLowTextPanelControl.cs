public class ShortLowTextPanelControl : BasePanelControl
{
    public void Confirm()
    {
        SplashManager.instance.CheckLastPanelClosed();
        SplashManager.instance.DisplayNextGeneralPanel();
        AudioManager.instance.Click();
        Destroy(gameObject);
    }

    public override void FillData(BasePanelData data)
    {
        title.text = data.Title;
        body.text = data.Body;
    }
}

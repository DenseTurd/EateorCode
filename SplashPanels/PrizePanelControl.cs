public class PrizePanelControl : BasePanelControl
{
    public void Confirm()
    {
        SplashManager.instance.CheckLastPanelClosed();
        SplashManager.instance.DisplayNextGeneralPanel();
        AudioManager.instance.Click();
        RoundOverManager.instance.Continue();
        Destroy(gameObject);
    }

    public override void FillData(BasePanelData data)
    {
        body.text = data.Body;
    }
}

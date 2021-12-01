using UnityEngine;
public class SwipePanelControl : BasePanelControl
{
    public void Confirm()
    {
        SplashManager.instance.CheckLastPanelClosed();
        SplashManager.instance.DisplayNextGeneralPanel();
        Time.timeScale = TutorialManager.Instance.playSpeed;
        PlayerController.instance.rb.isKinematic = false;
        TutorialManager.Instance.ResetPlayerThings();
        AudioManager.instance.Click();
        Destroy(gameObject);
    }

    public override void FillData(BasePanelData data)
    {
        return;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Confirm();
        }

        if (Input.touchCount > 0)
        {
            Confirm();
        }
    }
}

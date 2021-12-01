using UnityEngine;

public class PlayerInput 
{

    public float rotateSpeed = 0.15f;

    Vector2 firstPoint;
    Vector2 secondPoint;

    Vector2 prevPos;
    float mouseRotateSpeed = 20;

    PlayerController _player;

    bool reverseHorizontal;
    bool reverseVertical;

    public void Init(PlayerController player)
    {
        prevPos = Input.mousePosition;
        rotateSpeed = PlayerPrefs.GetFloat("RotateSpeed");
        _player = player;
    }

    public void HandleDebugging()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PowerupSpawner.Instance.SpawnCosmicBurgerPowerup();
            AudioManager.instance.Click();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            PowerupSpawner.Instance.SpawnGravityPowerup();
            AudioManager.instance.Click();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            PowerupSpawner.Instance.SpawnSatiationPowerup();
            AudioManager.instance.Click();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PowerupSpawner.Instance.SpawnSlowmoPowerup();
            AudioManager.instance.Click();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            RoundOverManager.instance.RoundOver("TooSmol");
            AudioManager.instance.Click();
        }
    }

    public void HandleSwipe()
    {
        var rotateSpeedAdjustedForTimeScale = rotateSpeed;
        if (Time.timeScale < 1)
            rotateSpeedAdjustedForTimeScale = rotateSpeed * 2;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstPoint = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                secondPoint = touch.position;

                var rotateAmmountHor = firstPoint.x - secondPoint.x;
                var rotateAmmountVer = firstPoint.y - secondPoint.y;

                CorrectRotationBasedOnScreenPosition(firstPoint);

                if (reverseHorizontal)
                    rotateAmmountHor = -rotateAmmountHor;

                if (reverseVertical)
                    rotateAmmountVer = -rotateAmmountVer;

                if (Settings.InvertSwipe)
                {
                    rotateAmmountHor = -rotateAmmountHor;
                    rotateAmmountVer = -rotateAmmountVer;
                }

                _player.transform.Rotate(0, 0, (rotateAmmountHor + rotateAmmountVer) * rotateSpeedAdjustedForTimeScale, Space.Self);


                firstPoint = secondPoint;
            }
        }
    }

    public void HandleMouse() // swipe sensitivity wont change mouse swipe
    {
        var mouseRotateSpeedAdjustedForTimeScale = mouseRotateSpeed;
        if (Time.timeScale < 1)
            mouseRotateSpeedAdjustedForTimeScale = mouseRotateSpeed * 2;

        if (Input.GetMouseButton(0))
        {
            float correctDirectionRotationHorizontal = prevPos.x - Input.mousePosition.x;
            float correctDirectionRotationVertical = prevPos.y - Input.mousePosition.y;

            if (reverseHorizontal)
                correctDirectionRotationHorizontal = -correctDirectionRotationHorizontal;

            if (reverseVertical)
                correctDirectionRotationVertical = -correctDirectionRotationVertical;

            _player.transform.Rotate(0, 0, (correctDirectionRotationVertical + correctDirectionRotationHorizontal) * mouseRotateSpeedAdjustedForTimeScale * Time.deltaTime, Space.Self);
        }
        prevPos = Input.mousePosition;

        CorrectRotationBasedOnScreenPosition(prevPos);
    }

    void CorrectRotationBasedOnScreenPosition(Vector2 pos)
    {
        if (pos.y < Screen.height / 2)
        {
            reverseHorizontal = false;
        }
        else
        {
            reverseHorizontal = true;
        }

        if (pos.x < Screen.width / 2)
        {
            reverseVertical = true;
        }
        else
        {
            reverseVertical = false;
        }
    }
}

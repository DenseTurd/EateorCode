using UnityEngine;

public class ThumbsController : MonoBehaviour
{
    public GameObject leftThumb;
    public GameObject rightThumb;

    float rightThumbDelay;
    bool thumbActive;

    public void SortThumbs()
    {
        ActivateThumb(leftThumb);
        rightThumbDelay = 0.2f;
        thumbActive = true;
    }

    void ActivateThumb(GameObject thumb)
    {
        thumb.GetComponent<Thumb>().stopPosition = thumb.transform.position;
        thumb.transform.position = new Vector3(thumb.transform.position.x, thumb.transform.position.y - 400, thumb.transform.position.z);
        thumb.SetActive(true);
    }

    private void Update()
    {
        if (thumbActive)
            rightThumbDelay -= Time.deltaTime;

        if (thumbActive && rightThumbDelay <= 0f)
        {
            ActivateThumb(rightThumb);
            thumbActive = false;
        }
    }

    public void HideThumbs()
    {
        leftThumb.SetActive(false);
        rightThumb.SetActive(false);
        this.enabled = false;
    }
}

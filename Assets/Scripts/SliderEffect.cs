using UnityEngine;
using UnityEngine.UI;

public class SliderEffect : MonoBehaviour
{
    [SerializeField] private Image[] images;

    private void SetOpacity(float opacity)
    {
        foreach(var image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
        }
    }

    public void SetEnabled() => SetOpacity(1f);
    public void SetDisabled() => SetOpacity(0.4f);
}

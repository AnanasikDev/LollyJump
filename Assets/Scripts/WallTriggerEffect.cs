using UnityEngine;
using UnityEngine.UI;

public class WallTriggerEffect : MonoBehaviour
{
    private Image image;
    
    [Tooltip("Mask describes how player's position affects walls trigger effect intensity. Vertical or horizontal primarily")]
    public Vector2 mask;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetEffect(Vector2 playerPosition)
    {
        float v = Vector2.Distance((playerPosition * mask), transform.position * mask) / 2f;
        float a = 1 - Mathf.Clamp01(Mathf.Pow(v, 2));
        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
    }

    private void FixedUpdate()
    {
        SetEffect(PlayerController.instance.transform.position);
    }
}

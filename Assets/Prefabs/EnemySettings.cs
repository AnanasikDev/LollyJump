using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBall_", menuName = "GameAssets/EnemyBall", order = 1)]
public class EnemyData : ScriptableObject
{
    public Color color;
    
    [Tooltip("How high it will bounce")]
    public float bounciness;
    
    [Tooltip("How unpredictable is the direction whereat it bounces")][Range(0f, 4f)]
    public float unpredictability;
    
    [Tooltip("How many balls it will produce after death")]
    public int inheritance;
    
    [Tooltip("How many lives it has (How many times it will divide until complete death)")]
    public int maxLives;

    [Tooltip("How frequent it will spawn")]
    public float frequency;
}

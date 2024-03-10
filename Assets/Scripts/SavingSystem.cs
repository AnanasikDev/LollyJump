using UnityEngine;
public static class SavingSystem
{
    public static int lastScore;

    public static float difficulty = 0.4f;
    public static Vector2 buttonSize = new Vector2(0.25f, 0.25f);

    public static bool settingsOpened = false;

    public static GameStateController.State state = GameStateController.State.Freezed;
}

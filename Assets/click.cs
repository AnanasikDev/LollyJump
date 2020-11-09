using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    public GameObject player;
    private move sc;
    public GameObject Settings;
    private settings script;
    public void Start()
    {
        script = Settings.GetComponent<settings>();
        sc = player.GetComponent<move>();
    }
    public void MoveStop()
    {
        sc.x = 0;
    }
    public void MoveRight()
    {
        sc.x = 1;
        if (!sc.alive && !script.opened)
            sc.MoveStart();
    }
    public void MoveLeft()
    {
        sc.x = -1;
        if (!sc.alive && !script.opened)
            sc.MoveStart();
    }
    public void MoveUp()
    {
        sc.y = 1;
        if (!sc.alive && !script.opened)
            sc.MoveStart();
    }
    public void MoveUpStop()
    {
        sc.y = 0;
    }
}

using UnityEngine;

public class Main : MonoBehaviour
{
    void Start() {
        Game.Init(this);
        Game.GetComp<MainLogic>().Init();
    }

    void Update()
    {
        Game.Update();
    }

    void FixedUpdate()
    {
        Game.FixedUpdate();
    }

    void OnApplicationQuit()
    {
        Game.OnApplicationQuit();
    }
}

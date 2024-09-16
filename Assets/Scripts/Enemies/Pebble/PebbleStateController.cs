using UnityEngine;

public class PebbleStateController : StateController
{
    SpriteRenderer spriteRenderer;
    
    public override void Start()
    {
        base.Start();
        GameRestart(); 
    }


    public void GameRestart()
    { 
        TransitionToState(startState);
    }
}

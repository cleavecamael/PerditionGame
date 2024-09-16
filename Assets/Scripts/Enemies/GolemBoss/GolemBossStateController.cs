using UnityEngine;

public class GolemBossStateController : StateController
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

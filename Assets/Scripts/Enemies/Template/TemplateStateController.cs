using UnityEngine;

public class TemplateStateController : StateController
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack
{
    public string attackName;
    public int weight;
    public Action<BaseEnemyController> cue;
    public Action<BaseEnemyController> attack;
    public Action<BaseEnemyController> finish;
    public Func<BaseEnemyController, bool> isValid;


    public bool checkValidity(BaseEnemyController controller)
    {
        if (isValid == null)
        {
            return true;
        }
        else
        {
            return isValid(controller);
        }
    }

    public void invokeFinish(BaseEnemyController controller)
    {
        if (finish == null)
        {
            return;
        }
        else
        {
            finish(controller);
        }
    }
    public void invokeCue (BaseEnemyController controller)
    {
        if (cue == null)
        {
            return;
        }
        else
        {
            cue(controller);
        }
    }
    public EnemyAttack(string attackName, int weight,
        Action<BaseEnemyController> cueFunction,
        Action<BaseEnemyController> attackFunction,
        Action<BaseEnemyController> finishFunction)
    {
        this.attackName = attackName;
        this.weight = weight;
        this.cue = cueFunction;
        this.attack = attackFunction;
        this.finish = finishFunction;
    }
    public EnemyAttack(string attackName, int weight,
        Action<BaseEnemyController> cueFunction,
        Action<BaseEnemyController> attackFunction,
        Action<BaseEnemyController> finishFunction,
        Func<BaseEnemyController, bool> isValid)
    {
        this.attackName = attackName;
        this.weight = weight;
        this.cue = cueFunction;
        this.attack = attackFunction;
        this.finish = finishFunction;
        this.isValid = isValid;
    }
    public EnemyAttack()
    {
        this.attackName = "null";
        this.weight = 0;
        
    }
    public EnemyAttack(string attackName, int weight,Action<BaseEnemyController> attackFunction)
    {
        this.attackName = attackName;
        this.weight = weight;
        this.attack = attackFunction;
    }
    public EnemyAttack(string attackName, int weight,
       Action<BaseEnemyController> cueFunction,
       Action<BaseEnemyController> attackFunction
       )
    {
        this.attackName = attackName;
        this.weight = weight;
        this.cue = cueFunction;
        this.attack = attackFunction;
    
    }


}

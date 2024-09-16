using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BaseEnemyAttacks : MonoBehaviour
{
    // Start is called before the first frame update

    public List<EnemyAttack> attacks = new List<EnemyAttack>();
    public BaseEnemyController controller;

    private void Start()
    {
        controller = GetComponent<BaseEnemyController>();
    }
    public EnemyAttack chooseAttack()
    {
        List<int> weights = new List<int>();
        List<int> indexes = new List<int>();
         for(int i = 0; i < attacks.Count; i++)
        {
            EnemyAttack currAttack = attacks[i];
            int weight = currAttack.weight;
            if (currAttack.checkValidity(controller))
            {
                weights.Add(weight);
                indexes.Add(i);
            }

        }
         //if no eligible attacks
         if (indexes.Count == 0)
        {
            return new EnemyAttack();
        }
        int chosenIndex = indexes[RandomExtension.GetRandomWeightedIndex(weights)];
        return attacks[chosenIndex];
    }
    public float getDistance()
    {
        return ((controller.getPlayerPosition() - (Vector2)this.transform.position).magnitude);
    }
   
}

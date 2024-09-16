using UnityEngine;

public class EnemyMoveCalc : MonoBehaviour
{
    int stallCount = 0;
    Vector2 displacement = Vector2.zero;
    Vector2 computeObstacle(Vector2 obstaclePosition, Vector2 currentPosition)
    {
        Vector2 displacement = (currentPosition - obstaclePosition);
   
        if (displacement.magnitude < 3) 
        {
            return 10 * displacement.normalized / (Mathf.Pow(displacement.magnitude, 3f));
        }
        else
        {
            return Vector2.zero;
        }
        
    }


    Vector2 computePlayer(Vector2 playerPosition, Vector2 currentPosition)
    {
        return (playerPosition - currentPosition).normalized;
    }
    Vector2 computeEnemy(Vector2 enemyPosition, Vector2 currentPosition)
    {
        Vector2 displacement = currentPosition - enemyPosition;
        if (displacement.magnitude < 3)
        {
            return 0.1f * displacement.normalized;
        }
        else
        {
            return Vector2.zero;
        }
    
    }
    Vector2 computeCorpse(Vector2 corpsePosition, Vector2 currentPosition)
    {
        Vector2 displacement = currentPosition - corpsePosition;
        
        return 9999f * displacement.normalized;
       

    }

    public Vector2 enemyMove()
    {
        Vector2 vectorSum = Vector2.zero;

        Collider2D[] nearbyobjects = Physics2D.OverlapCircleAll(this.transform.position, 10);
    
        foreach (Collider2D col in nearbyobjects)
        {
           
            string tag = col.tag;
            if (tag == "Obstacles")
            {
   
                Vector2 position;
                //if enemy is inside an obstacle, push it away by comparing with center of obstacle

                if (IsInside(col, this.transform.position)){
 
                    position = col.transform.position;

                    this.transform.position = (Vector2) this.transform.position + ((Vector2) this.transform.position - position);
                  
                }
                //if outside, use the nearest colliding point as that's more accurate
                else
                {
                    position = col.ClosestPoint(this.transform.position);
                }
                vectorSum = vectorSum + computeObstacle(position, this.transform.position);
            }
            if (tag == "Enemy")
            {
           
               vectorSum = vectorSum + computeEnemy(col.transform.position, this.transform.position);
            }
            if (tag == "Corpse")
            {

                vectorSum = vectorSum + computeCorpse(col.transform.position, this.transform.position);
            }

        }
        vectorSum = vectorSum + computePlayer(GetComponent<BaseEnemyController>().getPlayerPosition(), this.transform.position);

        
        displacement = displacement + vectorSum.normalized;
        
        stallCount = stallCount + 1;
        if (displacement.magnitude > 5)
        {
           
            stallCount = 0;
            displacement = Vector2.zero;
        }
        if (stallCount > 5)
        {
            
            vectorSum = Quaternion.Euler(0, 0, 90) * (GetComponent<BaseEnemyController>().getPlayerPosition() - (Vector2) this.transform.position);
        }
        return vectorSum.normalized;
   }
    bool IsInside(Collider2D c, Vector3 point)
    {
        Vector3 closest = c.ClosestPoint(point);
        //Debug.Log("closest" + c.gameObject.name);
        //Debug.Log("point" + point);
        return closest == point;
    }
}

using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public EnemySpawnerFunctions testFunctions;
    void Start()
    {
        if (testFunctions.checkValidSpawn(new Vector2(50, 0))){
           testFunctions.spawn("Golem", new Vector2(50, 0));
            Debug.Log("spawning");
       
       }
        if (testFunctions.checkValidSpawn(new Vector2(-10f, 0)))
        {
           // testFunctions.spawn("Golem", new Vector2(-10f, 0));
        }
        if (testFunctions.checkValidSpawn(new Vector2(0, 66.6f)))
        {
         // testFunctions.spawn("Golem", new Vector2(0, 66.6f));    

       }
        if (testFunctions.checkValidSpawn(new Vector2(0, -66.6f)))
        {
            //testFunctions.spawn("Golem", new Vector2(0, -66.6f));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

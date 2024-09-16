using System.Collections;
using UnityEngine;

public class BaseEnemyVFX : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator blinkRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public IEnumerator fadeDeath()
    {
        for (int i = 1; i <= 20; i++)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.a = temp.a - 0.05f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(0.030f);
        }
        Destroy(gameObject);
}
    }

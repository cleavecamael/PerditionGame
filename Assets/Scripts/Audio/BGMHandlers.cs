using System.Collections.Generic;
using UnityEngine;

public class BGMHandlers : MonoBehaviour
{
    // Start is called before the first frame update
 public void playBossBGM()
    {
        GetComponent<BGMManager>().playBGM("BossTheme");
    }
    
}

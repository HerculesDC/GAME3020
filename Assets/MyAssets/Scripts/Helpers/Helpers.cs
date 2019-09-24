using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { START = 0,
                         INTRO = 1,
                         LVL1 = INTRO << 1,
                         LVL2 = INTRO << 2,
                         LVL3 = INTRO << 3,
                         PAUSE = INTRO <<4,
                         WIN = INTRO << 5,
                         LOSE = INTRO << 6,
                         STATE_COUNT = INTRO << 7 }

//just to be on the safe side...
public enum Tractor { LEFT = -1, AI = 0, RIGHT = 1 }

public class Helpers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

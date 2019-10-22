using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum GameStates : byte { START = 0,
                                INTRO = 1,
                                LVL1 = 1 << 1,
                                LVL2 = 1 << 2,
                                LVL3 = 1 << 3,
                                PAUSE = 1 <<4,
                                WIN = 1 << 5,
                                LOSE = 1 << 6,
                                STATE_COUNT = 1 << 7 }

//just to be on the safe side...
[System.Flags]
public enum Tractor : byte { NONE = 0,
                             AI = 1,
                             RIGHT = 1 << 1,
                             LEFT = 1 << 2, }

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

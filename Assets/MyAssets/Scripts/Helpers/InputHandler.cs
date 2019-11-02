using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputHandler {
    float Accelerate();
    float Steer();
    bool Brake();
}

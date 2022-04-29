using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    CharacterMovement movementScript;

    [HideInInspector]
    public int score = 0;

    public void increaseScore(int increase)
    {
        score += increase;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HackSceneReference;

public class Hackable : MonoBehaviour
{

    public EDifficulty difficulty;

    private bool isHacked;

    public void Hack()
    {
        isHacked = true;
        FindObjectOfType<HackEnterController>().Enter(transform, difficulty);
    }

}

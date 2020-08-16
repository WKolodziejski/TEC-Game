using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirstLayer : MonoBehaviour
{
    public List<BossTurret> turrets;
    public int numTurrets;

    public float turretsFireRate = 0.5f;
    public float turretsFireRateIncrease = 0.2f;

    void Start()
    {

    }
    
    void Update()
    {

    }

    public void UpdateFireRates()
    {
        int multiplier = numTurrets - turrets.Count;

        foreach (BossTurret turret in turrets)
            turret.SetFireRate(turretsFireRate + (turretsFireRateIncrease * multiplier));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    public RectTransform bar;
    [Range(0,1)] public float life = 1f;
    [Range(0,3)] public int extra;
    public GameObject lf1, lf2, lf3;
    public void FixedUpdate(){
        UpdateBar(life,extra);
    }
    public void UpdateBar(float LifePercentage, int extra){
        bar.localPosition = new Vector3((LifePercentage * 220f) - 220f, bar.localPosition.y, bar.localPosition.y);
        ExtraLifes(extra);
    }
    private void ExtraLifes(int n){
        if( n >= 1) lf1.SetActive(true);
        else lf1.SetActive(false);

        if( n >= 2) lf2.SetActive(true);
        else lf2.SetActive(false);

        if( n == 3) lf3.SetActive(true);
        else lf3.SetActive(false);

    }
}

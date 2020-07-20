using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    public void Hit(float damage)
    {
        GetComponent<TextMeshPro>().SetText(damage.ToString());
    }

}

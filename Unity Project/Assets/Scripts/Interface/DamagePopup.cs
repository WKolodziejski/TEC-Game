using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 0.5f);

        transform.SetParent(null);
        transform.position += Vector3.up * 2f;

        StartCoroutine(IAnim());
    }

    public void Hit(float damage)
    {
        GetComponent<TextMeshPro>().SetText("-" + damage);
    }

    private IEnumerator IAnim()
    {
        float y = transform.position.y + 0.5f;

        while (transform.position.y < y)
        {
            transform.position += Vector3.up * 0.05f;

            yield return null;
        }
    }

}

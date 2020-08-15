using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShell : MonoBehaviour
{

    public Transform insideLeft;
    public Transform insideRight;
    public Transform outsideLeft;
    public Transform outsideRight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Open();
    }

    public void Open()
    {
        StartCoroutine(IOpen());
    }

    private IEnumerator IOpen()
    {
        while (insideLeft.rotation.eulerAngles.z < 90)
        {
            insideLeft.Rotate(Vector3.forward, 0.5f);
            insideRight.Rotate(Vector3.forward, -0.5f);

            if (insideLeft.rotation.eulerAngles.z > 45)
            {
                outsideLeft.Rotate(Vector3.forward, 0.5f);
                outsideRight.Rotate(Vector3.forward, -0.5f);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

}

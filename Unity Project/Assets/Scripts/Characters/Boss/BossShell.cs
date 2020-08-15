using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShell : MonoBehaviour
{

    public Transform insideLeft;
    public Transform insideRight;
    public Transform outsideLeft;
    public Transform outsideRight;
    public GameObject boss;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
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
        audioSource.Play();

        while (insideLeft.localRotation.eulerAngles.z < 90f)
        {
            insideLeft.Rotate(Vector3.forward, 0.5f, Space.Self);
            insideRight.Rotate(Vector3.forward, -0.5f);

            if (insideLeft.localRotation.eulerAngles.z > 45f)
            {
                outsideLeft.Rotate(Vector3.forward, 0.5f);
                outsideRight.Rotate(Vector3.forward, -0.5f);
            }

            yield return new WaitForSeconds(0.05f);
        }

        GameObject o = Instantiate(boss, transform.position, Quaternion.identity);

        while (o.transform.position.y > 0)
        {
            o.transform.position -= Vector3.up * 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
    }

}

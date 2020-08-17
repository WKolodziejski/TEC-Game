using Cinemachine;
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
    public Hackable hackable;
    public GameObject audio2;
    public CinemachineVirtualCamera cam;

    private AudioSource audioSource;
    private bool hacked;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        hackable.SetAction(() =>
        {
            if (!hacked)
            {
                hacked = true;
                Open();
            }
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            StartCoroutine(Utils.FadeOut(audioSource));
    }

    public void Open()
    {
        StartCoroutine(IOpen());
    }

    private IEnumerator IOpen()
    {
        yield return new WaitForSeconds(1f);

        audio2.SetActive(true);

        yield return new WaitForSeconds(1f);

        FindObjectOfType<Player2D>().DisableControls();

        GameObject o = Instantiate(boss, transform.position, Quaternion.identity);

        cam.m_Priority = 20;
        cam.Follow = o.transform;
        cam.LookAt = o.transform;

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

        while (o.transform.position.y > 0)
        {
            o.transform.position -= Vector3.up * 0.05f;
            yield return new WaitForSeconds(0.025f);
        }

        cam.m_Priority = 0;

        FindObjectOfType<Player2D>().EnableControls();
    }

}

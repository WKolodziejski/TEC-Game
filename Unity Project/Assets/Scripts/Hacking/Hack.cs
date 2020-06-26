using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hack : MonoBehaviour
{

    public GameObject target;
    public GameObject cutscene;
    public GameObject audio;

    public CinemachineVirtualCamera hackCamera;
    public HackInterface hackInterface;
    public float holdTime = 2.0f;

    private bool held = false;
    private float startTime;
    private float timer;
    private HackInterface popup;

    void Start()
    {
        popup = Instantiate(hackInterface);
        DontDestroyOnLoad(audio);
    }

    void Awake()
    {
        SceneReferences.Instance.ReloadObjects();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            startTime = Time.time;
            timer = startTime;

            popup.SetActive(true);
        }

        if (Input.GetKey(KeyCode.H) && held == false)
        {
            timer += Time.deltaTime;
            float counter = (timer - startTime) / holdTime;

            if (counter > 0)
            {
                popup.setProgress(counter);
            }

            if (timer > (startTime + holdTime))
            {
                held = true;
                hackInterface.SetActive(false);

                StartCoroutine(ChangeScene());
            }
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            popup.SetActive(false);
        }
    }

    IEnumerator ChangeScene()
    {
        hackCamera.Follow = target.transform;
        cutscene.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        SceneReferences.Instance.PersistObjects();
        SceneManager.LoadScene(1);
    }

}

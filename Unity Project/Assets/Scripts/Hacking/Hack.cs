//não é mais usado???


/*using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hack : MonoBehaviour
{

    public GameObject cutscene;
    public AudioSource transition;
    public AudioSource soundtrack;
    public CinemachineVirtualCamera hackCamera;
    public HackInterface hackInterface;
    public float holdTime = 2.0f;

    private bool held = false;
    private float startTime;
    private float timer;
    //private HackInterface popup;
    private Transform target;

    void Start()
    {
        //popup = Instantiate(hackInterface);
        DontDestroyOnLoad(transition);
        DontDestroyOnLoad(soundtrack);
    }

    void Awake()
    {
        if (SceneReferences.Instance.IsPersisted())
        {
            //soundtrack.time = SceneReferences.Instance.GetAudioTime();
            SceneReferences.Instance.ReloadObjects();
        }
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H) && held == false)
        {
            startTime = Time.time;
            timer = startTime;

            target = FindTarget();
            
            hackInterface.GetComponent<RectTransform>().position = new Vector3(target.position.x, target.position.y + 1, target.position.z);
            hackInterface.SetActive(true);
            transition.Play();
        }

        if (Input.GetKey(KeyCode.H) && held == false)
        {
            timer += Time.deltaTime;
            float counter = (timer - startTime) / holdTime;

            if (counter > 0)
            {
                hackInterface.SetProgress(counter);
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
            hackInterface.SetActive(false);
            target = null;

            if (!held)
                transition.Stop();
        }
    }

    IEnumerator ChangeScene()
    {
        hackCamera.Follow = target.transform;
        cutscene.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        SceneReferences.Instance.PersistObjects();
        //SceneReferences.Instance.SetAudio(soundtrack);
        SceneManager.LoadScene(1);
    }

    Transform FindTarget()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Hackable");
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject o in objs)
        {
            Transform t = o.transform;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

}
*/
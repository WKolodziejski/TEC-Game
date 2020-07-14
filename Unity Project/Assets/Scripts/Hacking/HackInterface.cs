using System;
using UnityEngine;
using UnityEngine.UI;
using static HackSceneReference;

public class HackInterface : MonoBehaviour
{

    public float maxDist = 5f;
    public float holdTime = 2f;

    public GameObject particleThrow;
    public GameObject particlePlayer;
    public GameObject particleTarget;
    public GameObject particleSuccess;
    public AudioSource audioFail;
    
    private Text _text;
    private Image _progress;
    private RectTransform _position;
    private Transform _playerPosition;
    private Hackable _target;
    private GameObject _particlePlayer;
    private GameObject _particleTarget;
    private GameObject _particleSuccess;
    

    void Start()
    {
        _position = GetComponent<RectTransform>();
        _text = GetComponentInChildren<Text>(true);
        _progress = GetComponentInChildren<Image>(true);

        gameObject.SetActive(false);
    }

    public void SetPlayerPosition(Transform transform)
    {
        _playerPosition = transform;
    }

    public bool SetProgress(float countTime, float startTime)
    {

        float counter = (countTime - startTime) / holdTime;

        if (counter > 0 && counter < 1 && _target != null)
        {
            int c = (int)(counter * 100);

            _text.text = String.Format("{0:0}", c);
            _progress.fillAmount = counter;

            if (c % 10 == 0)
            {
                GameObject pt = Instantiate(particleThrow, _playerPosition.position, Quaternion.identity);
                pt.transform.LookAt(_target.transform);
                pt.GetComponent<Rigidbody2D>().AddForce(pt.transform.forward * 0.1f);
                Destroy(pt, 1f);
            }
        }

        if (countTime > (startTime + holdTime) && _target != null)
        {

            _target.Hack();

            FinishHacking();

            return true;
        }

        return false;
    }

    public void StartHacking()
    {
        FindTarget();

        if (_target == null)
        {
            audioFail.Play();
        }
        else
        {
            _position.position = new Vector3(_target.transform.position.x, _target.transform.position.y + 1, 0);

            _particlePlayer = Instantiate(particlePlayer, _playerPosition.transform);
            _particleTarget = Instantiate(particleTarget, _target.transform);

            gameObject.SetActive(true);
        }
    }

    public void CancelHacking()
    {
        gameObject.SetActive(false);

        _target = null;

        Destroy(_particlePlayer);
        Destroy(_particleTarget);
    }

    public void FinishHacking()
    {
        _particleSuccess = Instantiate(particleSuccess, _target.transform.position, Quaternion.identity);

        gameObject.SetActive(false);

        Destroy(_particlePlayer);
        Destroy(_particleTarget);

        _target = null;
    }

    public void DestroyPortal()
    {
        Destroy(_particleSuccess);
    }

    void FindTarget()
    {
        Hackable[] objs = GameObject.FindObjectsOfType<Hackable>();

        Hackable result = null;
        Vector3 currentPos = _playerPosition.position;

        float minDist = Mathf.Infinity;

        foreach (Hackable h in objs)
        {
            float dist = Vector3.Distance(h.transform.position, currentPos);

            if (dist < minDist && dist <= maxDist)
            {
                result = h;
                minDist = dist;
            }
        }

        _target = result;
    }

}

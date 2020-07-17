using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{

    public RectTransform bar;
    public Character character;

    [Range(0, 3)] public int extra = 3;
    public GameObject lf1, lf2, lf3;

    void Start()
    {
        character.SetOnDieListener(() =>
        {
            extra--;
            ExtraLifes();
            StartCoroutine(IDie());
        });
    }

    void Update()
    {
        bar.localPosition = new Vector3((character.hp / 3f * 220f) - 220f, bar.localPosition.y, bar.localPosition.y);
    }

    private void ExtraLifes()
    {
        lf1.SetActive(extra >= 1);
        lf2.SetActive(extra >= 2);
        lf3.SetActive(extra >= 3);

        //if (n == 0)
            //GAMEOVER
    }

    private IEnumerator IDie()
    {
        character.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        Vector3 cam = Camera.main.transform.position;

        character.hp = 3;
        character.transform.position = new Vector3(cam.x, cam.y, 0);
        character.gameObject.SetActive(true);
    }

}

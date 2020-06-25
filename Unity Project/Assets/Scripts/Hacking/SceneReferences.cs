using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferences : Singleton<SceneReferences>
{
    protected SceneReferences() { }

    private bool isPersisted;

    public void PersistObjects()
    {
        PersistPlayer();
        //TODO: persistir todas entidades e atributos da cena
        isPersisted = true;
    }

    public void ReloadObjects()
    {
        if (isPersisted)
        {
            ReloadPlayer();
            isPersisted = false;
        }
    }

    private Vector3 player_position;
    private Quaternion player_rotation;

    private void PersistPlayer()
    {
        Transform player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        player_position = player.position;
        player_rotation = player.rotation;
    }

    private void ReloadPlayer()
    {
        Transform player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        player.position = player_position;
        player.rotation = player_rotation;
    }

}

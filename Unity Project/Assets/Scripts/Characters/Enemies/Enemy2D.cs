using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2D : Enemy //TODO: mudar de facção
{
    private AIAssistant assistant;

    //CHAMAR BASE NOS FILHOS
    protected override void InitializeComponents()
    {
        assistant = GameObject.FindObjectOfType<AIAssistant>();
        SetEnabled(false);
    }

    protected override Transform GetTarget()
    {
        if (!target || target.IsDead() || !target.enabled)
            target = assistant.GetTarget(this.transform.position, this.tag);

        return target?.transform;
    }

    protected void LookAtTarget()
    {
        if (GetTarget() != null)
            transform.rotation = GetTargetRotation();
    }

    protected Quaternion GetTargetRotation()
    {
        if (GetTarget() != null)
            return transform.position.x < GetTarget().position.x ? Quaternion.identity : Quaternion.Euler(0f, 180f, 0f);
        else
            return Quaternion.identity;
    }

    protected int GetTargetMagnitude()
    {
        if (GetTarget() != null)
            return transform.position.x < GetTarget().position.x ? 1 : -1;
        else
            return 1;
    }

    public abstract void Attack();

    public override void SetEnabled(bool enabled)
    {
        if (this.enabled != enabled && !IsDead())
        {
            this.enabled = enabled;
            assistant.EnabledAI(this, enabled);
        }
    }

    public void ChangeFaction()
    {
        assistant.EnabledAI(this, false);
        tag = "Player";
        assistant.EnabledAI(this, true);
    }

    private void OnDestroy()
    {
        assistant.EnabledAI(this, false);
    }

    /*protected bool CanAttack()
    {
        float[] boundries = enemySpawner.getCameraBoundries();
        return transform.position.x > boundries[0] && transform.position.x < boundries[1] &&
            transform.position.y > boundries[2] && transform.position.y < boundries[3];
    }*/
}

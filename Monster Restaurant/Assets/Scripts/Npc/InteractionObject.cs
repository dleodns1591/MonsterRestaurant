using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FSMManager;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] protected GameObject interactionBtnObj;

    [SerializeField] private float interactionDistance;

    private PlayerFSM.PlayerFSM player;

    protected bool isInteractionEnable = false;

    protected virtual void Awake()
    {
        player = FSMManager.GameManager.Instance.player;
    }

    protected virtual void Update()
    {
        if (isInteractionEnable == false) return;

        var dis = Vector2.Distance(player.transform.position, transform.position);

        if (dis < interactionDistance)
        {
            player.CurNpc = this;
        }
        else
        {
            if(player.CurNpc == this)
            {
                BtnImgEnable(false);
            }
        }

    }

    public void BtnImgEnable(bool isEnalbe)
    {
        interactionBtnObj.SetActive(isEnalbe);
    }

    public virtual void InteractionEvent()
    {
        BtnImgEnable(false);
    }
}

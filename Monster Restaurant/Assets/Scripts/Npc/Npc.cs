using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Npc
{
    public class Npc : InteractionObject
    {
        private GameObject npcObj;

        private bool isSpawned = false;

        public bool IsSpawned => isSpawned;


        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            npcObj = transform.GetChild(0).gameObject;

            npcObj.SetActive(false);
            interactionBtnObj.SetActive(false);

            isSpawned = false;
            isInteractionEnable = false;
        }

        public void Spawn()
        {
            npcObj.SetActive(true);

            isSpawned = true;
            isInteractionEnable = true;
        }

        public override void InteractionEvent()
        {
            base.InteractionEvent();

            npcObj.SetActive(false);
            isSpawned = false;
            isInteractionEnable = false;
        }
    }
}

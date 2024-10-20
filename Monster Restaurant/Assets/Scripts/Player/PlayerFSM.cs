using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerFSM
{

    public enum PlayerState
    {
        Counter,
        Gathering,
        Cooking
    }

    public class PlayerFSM : MonoBehaviour
    {
        [SerializeField] private float moveSpd;

        public FSMMachine<PlayerFSM> FSMMachine = new FSMMachine<PlayerFSM>();

        private SpriteRenderer playerSr;

        private InteractionObject curInteraction;

        public InteractionObject CurNpc
        {
            get
            {
                return curInteraction;
            }

            set
            {

                if(curInteraction != null)
                {
                    curInteraction.BtnImgEnable(false);
                }

                curInteraction = value;
                curInteraction.BtnImgEnable(true);

            }
        }


        private void Awake()
        {
            RegistFSMState();

            FSMMachine.FSMStart(PlayerState.Counter);

            playerSr = gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            FSMMachine.UpdateFSM();
        }
        private void RegistFSMState()
        {
            FSMMachine.RegistState(PlayerState.Counter, new CounterState(this));

            FSMMachine.RegistState(PlayerState.Gathering, new GatheringState(this));

            FSMMachine.RegistState(PlayerState.Cooking, new CookingState(this));
        }
        public void ChangeState(PlayerState state)
        {
            FSMMachine.ChangeState(state);
        }

        public void FsmEnd()
        {
            FSMMachine.FSMEnd();
        }

        public void Move()
        {
            var vec = Input.GetAxis("Horizontal");

            Vector3 moveVec = new Vector3(vec, 0, 0);

            transform.Translate(moveVec * moveSpd * Time.deltaTime);

            // 캐릭터 회전
            var playerFlip = (moveVec.x < 0);
            playerSr.flipX = playerFlip;
        }

        public void Interaction()
        {
            if (Input.GetKeyDown(KeyCode.S) && curInteraction != null)
            {
                curInteraction.InteractionEvent();
            }
        }


    }

    public class CounterState : FSMState<PlayerFSM>
    {
        public CounterState(PlayerFSM root) : base(root)
        {
        }
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
            root.Move();
            root.Interaction();

        }



        public override void OnExit()
        {
        }
    }

    public class GatheringState : FSMState<PlayerFSM>
    {
        public GatheringState(PlayerFSM root) : base(root)
        {
        }
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
            root.Move();
            root.Interaction();
        }
        public override void OnExit()
        {
        }
    }

    public class CookingState : FSMState<PlayerFSM>
    {
        public CookingState(PlayerFSM root) : base(root)
        {
        }
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Npc;
using PlayerFSM;
using UnityEngine;

namespace FSMManager 
{

    public enum GameState
    {
        Working,
        Weekend,
        MapSelect
    }

    public class GameManager : Singleton<GameManager>
    {

        public PlayerFSM.PlayerFSM player;

        public FSMMachine<GameManager> FSMMachine = new FSMMachine<GameManager>();

        private void Awake()
        {
            RegistFSMState();

            FSMMachine.FSMStart(GameState.Working);
        }

        private void Update()
        {
            FSMMachine.UpdateFSM();
        }
        private void RegistFSMState()
        {
            FSMMachine.RegistState(GameState.Working, new WorkingState(this));

            FSMMachine.RegistState(GameState.Weekend, new WeekendState(this));

            FSMMachine.RegistState(GameState.MapSelect, new MapSelectState(this));
        }
        public void ChangeState(PlayerState state)
        {
            FSMMachine.ChangeState(state);
        }

        public void FsmEnd()
        {
            FSMMachine.FSMEnd();
        }

    }

    public class WorkingState : FSMState<GameManager>
    {
        public WorkingState(GameManager root) : base(root)
        {
        }
        public override void OnEnter()
        {
            NpcManager.Instance.EntryNpc();
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }

    public class WeekendState : FSMState<GameManager>
    {
        public WeekendState(GameManager root) : base(root)
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

    public class MapSelectState : FSMState<GameManager>
    {
        public MapSelectState(GameManager root) : base(root)
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


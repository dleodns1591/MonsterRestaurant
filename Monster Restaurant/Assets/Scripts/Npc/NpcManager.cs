using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Npc
{
    public class NpcManager : Singleton<NpcManager>
    {

        [SerializeField] private Npc[] sampleNpcs;

        public PlayerFSM.PlayerFSM player;

        [SerializeField] private float spawnDelay = 3f;
        private float spawnDelayCnt = 0;

        private bool isSpawnState = false;

        public void EntryNpc()
        {
            isSpawnState = true;
            spawnDelayCnt = 0;

            StartCoroutine(NpcSpawn());
        }


        private IEnumerator NpcSpawn()
        {
            while (isSpawnState)
            {
                spawnDelayCnt += Time.deltaTime;

                if(spawnDelayCnt > spawnDelay)
                {
                    // spawn
                    foreach(var npc in sampleNpcs)
                    {
                        if(npc.IsSpawned == false)
                        {
                            npc.Spawn();

                            spawnDelayCnt = 0;

                            break;
                        }
                    }
                }
                
                yield return null;
            }
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private List<MonsterController> pool = new();

    [SerializeField] GameObject monster;
    [SerializeField] float distance = 20f;

    private GameObject player;

    private void Start()
    {
        player = Global.Player;

        for (int i = 0; i < 10; i++)
            Spawn();
    }

    public void Spawn()
    {
        Vector3 pos = GetSpawnPos();
        MonsterController mc = GetClone(monster);

        //юс╫ц
        mc.Init(pos, new StatInfo(100, 100, 100, 100, 5));
    }

    private MonsterController GetClone(GameObject go)
    {
        foreach (MonsterController monster in pool)
            if (monster.isFree)
                return monster;

        return MakeNew(go);
    }

    private MonsterController MakeNew(GameObject go)
    {
        MonsterController monster = Instantiate(go, transform).GetComponent<MonsterController>();
        pool.Add(monster);
        return monster;
    }

    private Vector3 GetSpawnPos()
    {
        Vector3 playerPos = player.transform.position;
        float x = Random.Range(distance, distance * 2);
        float z = Random.Range(distance, distance * 2);

        Vector3 pos = new Vector3(playerPos.x + (Random.Range(0, 2) == 0 ? -x : x ),
            0, playerPos.z + (Random.Range(0, 2) == 0 ? -z : z));

        return pos;
    }
}

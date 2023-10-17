using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    private List<Transform> maps = new();

    [SerializeField] private int currentIdx;
    [SerializeField] private Transform currentMap;
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = Global.Player;
        foreach (Transform t in gameObject.transform)
            maps.Add(t);

        currentIdx = 4;
        currentMap = maps[currentIdx];
    }

    private void Update()
    {
        CheckPlayerPos();
    }

    private void CheckPlayerPos()
    {
        Transform playerPos = player.transform;
        if (playerPos.position.x > currentMap.position.x + Global.mapSize / 2) MoveToRight();
        if (playerPos.position.x < currentMap.position.x - Global.mapSize / 2) MoveToLeft();
        if (playerPos.position.z > currentMap.position.z + Global.mapSize / 2) MoveToUp();
        if (playerPos.position.z < currentMap.position.z - Global.mapSize / 2) MoveToDown();
        currentMap = maps[currentIdx];
    }

    private void MoveToRight()
    {
        int moveidx = ((currentIdx % 3) + 2) % 3;
        for(int i = 0; i<3; i++)
            maps[moveidx+i*3].Translate(new Vector3(3 * Global.mapSize, 0, 0));

        int temp = currentIdx / 3;
        currentIdx = ((currentIdx % 3) + 1) % 3 + temp * 3;
    }
    private void MoveToLeft()
    {
        int moveidx = ((currentIdx % 3) + 1) % 3;
        for (int i = 0; i < 3; i++)
            maps[moveidx + i * 3].Translate(new Vector3(-3 * Global.mapSize, 0, 0));

        int temp = currentIdx / 3;
        currentIdx = ((currentIdx % 3) + 2) % 3 + temp * 3;
    }
    private void MoveToUp()
    {
        int moveidx = (currentIdx + 6) % 9;
        int temp = moveidx / 3;
        moveidx %= 3;

        for (int i = 0; i < 3; i++)
            maps[(moveidx + i) % 3 + 3 * temp].Translate(new Vector3(0, 0, 3 * Global.mapSize));

        currentIdx = (currentIdx + 3) % 9;
    }
    private void MoveToDown()
    {
        int moveidx = (currentIdx + 3) % 9;
        int temp = moveidx / 3;
        moveidx %= 3;

        for (int i = 0; i < 3; i++)
            maps[(moveidx + i) % 3 + 3 * temp].Translate(new Vector3(0, 0, -3 * Global.mapSize));

        currentIdx = (currentIdx + 6) % 9;
    }
}

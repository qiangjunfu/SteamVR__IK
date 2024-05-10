using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;  // 玩家出生点
    private List<Player> players = new List<Player>();

    void Start()
    {

    }

    void Update()
    {

    }




    // 从对象池中添加玩家
    public void AddPlayer(int id, string name)
    {
        // 假设玩家的预制体名称为 "PlayerPrefab"，并且预制体已正确设置在 ComponentPoolManager 中
        Player newPlayer = ComponentPoolManager.Instance.GetObject<Player>("PlayerPrefab", spawnPos.position, Quaternion.identity);
        newPlayer.id = id;
        newPlayer.name = name;
        players.Add(newPlayer);
        Debug.Log("玩家添加成功: " + name);
    }

    // 回收玩家到对象池
    public void RemovePlayer(int id)
    {
        Player playerToRemove = players.Find(p => p.id == id);
        if (playerToRemove != null)
        {
            players.Remove(playerToRemove);
            ComponentPoolManager.Instance.RecycleObject(playerToRemove);
            Debug.Log("玩家删除成功: ID " + id);
        }
        else
        {
            Debug.Log("未找到玩家: ID " + id);
        }
    }

    // 更新玩家信息
    public void UpdatePlayer(int id, string newName)
    {
        Player player = players.Find(p => p.id == id);
        if (player != null)
        {
            player.name = newName;
            Debug.Log("玩家信息更新成功: " + newName);
        }
        else
        {
            Debug.Log("未找到玩家: ID " + id);
        }
    }

    // 查询玩家
    public Player GetPlayer(int id)
    {
        return players.Find(player => player.id == id);
    }
}

// Player 类
public class Player : MonoBehaviour
{
    public int id;
    public string name;
}

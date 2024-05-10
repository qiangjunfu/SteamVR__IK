using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;  // ��ҳ�����
    private List<Player> players = new List<Player>();

    void Start()
    {

    }

    void Update()
    {

    }




    // �Ӷ������������
    public void AddPlayer(int id, string name)
    {
        // ������ҵ�Ԥ��������Ϊ "PlayerPrefab"������Ԥ��������ȷ������ ComponentPoolManager ��
        Player newPlayer = ComponentPoolManager.Instance.GetObject<Player>("PlayerPrefab", spawnPos.position, Quaternion.identity);
        newPlayer.id = id;
        newPlayer.name = name;
        players.Add(newPlayer);
        Debug.Log("�����ӳɹ�: " + name);
    }

    // ������ҵ������
    public void RemovePlayer(int id)
    {
        Player playerToRemove = players.Find(p => p.id == id);
        if (playerToRemove != null)
        {
            players.Remove(playerToRemove);
            ComponentPoolManager.Instance.RecycleObject(playerToRemove);
            Debug.Log("���ɾ���ɹ�: ID " + id);
        }
        else
        {
            Debug.Log("δ�ҵ����: ID " + id);
        }
    }

    // ���������Ϣ
    public void UpdatePlayer(int id, string newName)
    {
        Player player = players.Find(p => p.id == id);
        if (player != null)
        {
            player.name = newName;
            Debug.Log("�����Ϣ���³ɹ�: " + newName);
        }
        else
        {
            Debug.Log("δ�ҵ����: ID " + id);
        }
    }

    // ��ѯ���
    public Player GetPlayer(int id)
    {
        return players.Find(player => player.id == id);
    }
}

// Player ��
public class Player : MonoBehaviour
{
    public int id;
    public string name;
}

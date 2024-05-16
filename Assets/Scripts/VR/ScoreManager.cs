using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class ScoreManager : MonoSingleTon<ScoreManager>, IManager
{
    private Dictionary<ICharacter, int> characterScores = new Dictionary<ICharacter, int>();
    private Dictionary<ICharacter, Dictionary<ICharacter, int>> characterHits = new Dictionary<ICharacter, Dictionary<ICharacter, int>>();
    private Dictionary<ICharacter, List<ICharacter>> characterKills = new Dictionary<ICharacter, List<ICharacter>>();


    public void Init()
    {
        // ³õÊ¼»¯Âß¼­
    }


    public void RecordHit(ICharacter attacker, ICharacter target, int damage, int score)
    {
        if (attacker == null || target == null)
        {
            Debug.LogError("Attacker or target is null");
            return;
        }

        // Update score
        if (!characterScores.TryGetValue(attacker, out int currentScore))
        {
            currentScore = 0;
        }
        int newScore = currentScore + score;
        characterScores[attacker] = newScore;

        if (score != 0)
        {
            MessageManager.Broadcast(GameEventType.ScoreChanged, attacker, newScore);
        }

        // Update hit records
        if (!characterHits.TryGetValue(attacker, out var hits))
        {
            hits = new Dictionary<ICharacter, int>();
            characterHits[attacker] = hits;
        }
        if (hits.TryGetValue(target, out int currentDamage))
        {
            hits[target] = currentDamage + damage;
        }
        else
        {
            hits[target] = damage;
        }


        if (target.IsDead())
        {
            RecordKill(attacker, target);
        }
    }

    private void RecordKill(ICharacter attacker, ICharacter target)
    {
        if (!characterKills.TryGetValue(attacker, out var kills))
        {
            kills = new List<ICharacter>();
            characterKills[attacker] = kills;
        }
        kills.Add(target);


        MessageManager.Broadcast(GameEventType.KillEvent, attacker, target);
    }


    #region Get

    public int GetScore(ICharacter character)
    {
        if (characterScores.TryGetValue(character, out int score))
        {
            return score;
        }
        return 0;
    }

    public Dictionary<ICharacter, int> GetHits(ICharacter character)
    {
        if (characterHits.TryGetValue(character, out var hits))
        {
            return new Dictionary<ICharacter, int>(hits);
        }
        return new Dictionary<ICharacter, int>();
    }

    public List<ICharacter> GetKills(ICharacter character)
    {
        if (characterKills.TryGetValue(character, out var kills))
        {
            return new List<ICharacter>(kills);
        }
        return new List<ICharacter>();
    }

    public Dictionary<ICharacter, int> GetAllCharacterScores()
    {
        return new Dictionary<ICharacter, int>(characterScores);
    }

    public Dictionary<ICharacter, Dictionary<ICharacter, int>> GetAllCharacterHits()
    {
        var result = new Dictionary<ICharacter, Dictionary<ICharacter, int>>();
        foreach (var kvp in characterHits)
        {
            result[kvp.Key] = new Dictionary<ICharacter, int>(kvp.Value);
        }
        return result;
    }

    public Dictionary<ICharacter, List<ICharacter>> GetAllCharacterKills()
    {
        var result = new Dictionary<ICharacter, List<ICharacter>>();
        foreach (var kvp in characterKills)
        {
            result[kvp.Key] = new List<ICharacter>(kvp.Value);
        }
        return result;
    }

    #endregion

}

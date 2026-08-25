using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource audioSource;
    public AudioClip snakeSound;
    public AudioClip ladderSound;

    void Awake()
    {
        instance = this;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public Dictionary<int, int> ladders = new Dictionary<int, int>()
    {
       {2,39},{3,35},{7,31},{23,55},{49,69},{45,66},{67,86},{62,83}
    };

    public Dictionary<int, int> snakes = new Dictionary<int, int>()
    {
        {97,19},{91,48},{85,45},{52,27},{33,6},{30,11},{78,40},{95,56},{42,16}
    };

    public void CheckLadderOrSnake(PlayerMovement player)
    {
        int pos = player.currentTile;

        // 🪜 LADDER
        if (ladders.ContainsKey(pos))
        {
            int target = ladders[pos];

            PlaySound(ladderSound, 1.0f);
            StartCoroutine(MovePlayerSmooth(player, target, true));
        }

        // 🐍 SNAKE
        else if (snakes.ContainsKey(pos))
        {
            int target = snakes[pos];

            PlaySound(snakeSound, 0.9f);

            // 🔥 LOCK movement BEFORE scene change
            player.isMoving = true;

            MainController.instance.TriggerMinigame(pos, target);
        }
    }

    void PlaySound(AudioClip clip, float basePitch, float duration = 0.5f)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.pitch = Random.Range(basePitch - 0.1f, basePitch + 0.1f);
            audioSource.clip = clip;
            audioSource.Play();

            StartCoroutine(FadeOutSound(duration));
        }
    }

    IEnumerator FadeOutSound(float duration)
    {
        float startVolume = audioSource.volume;

        yield return new WaitForSeconds(duration);

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime * 5;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    IEnumerator MovePlayerSmooth(PlayerMovement player, int targetTile, bool isLadder)
    {
        player.isMoving = true;

        float originalSpeed = player.moveSpeed;
        player.moveSpeed = isLadder ? 8f : 6f;

        Vector3 targetPos = player.tiles[targetTile].position;

        while (Vector3.Distance(player.transform.position, targetPos) > 0.01f)
        {
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                targetPos,
                player.moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        player.currentTile = targetTile;
        player.moveSpeed = originalSpeed;
        player.isMoving = false;
    }
}
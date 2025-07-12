using UnityEngine;

public class BallTester : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;

    void Start()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("❌ Ball prefab is not assigned!");
            return;
        }

        if (ballSpawnPoint == null)
        {
            Debug.LogError("❌ Spawn point not assigned!");
            return;
        }

        Vector3 spawnPos = ballSpawnPoint.position;
        GameObject ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        ball.transform.localScale = Vector3.one * 1.5f;
        ball.GetComponent<Renderer>().material.color = Color.magenta;
        Debug.Log("✅ Spawned ball at " + spawnPos);
    }
}

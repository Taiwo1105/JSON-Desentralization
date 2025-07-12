using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class WorkoutLoader : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public GameObject ballPrefab;
    public Button playPauseButton;
    public GameObject ballspawnpoint;

    private WorkoutData workoutData;
    private int currentWorkoutIndex = -1;
    private Coroutine spawnCoroutine;
    private bool isPlaying = false;
    private int currentBallIndex = 0; // 👈 Track where to resume

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("workouts");
        workoutData = JsonUtility.FromJson<WorkoutData>(jsonFile.text);

        Debug.Log("✅ Parsed workout count: " + workoutData.workoutInfo.Count);
        Debug.Log("👉 First workout ballCount: " + workoutData.workoutInfo[0].ballCount);

        titleText.text = workoutData.ProjectName;
        descriptionText.text = "Workout Description here";
        GenerateWorkoutButtons();

        playPauseButton.onClick.AddListener(TogglePlayPause);
        SetPlayPauseButtonText("Play");
    }

    void GenerateWorkoutButtons()
    {
        for (int i = 0; i < workoutData.workoutInfo.Count; i++)
        {
            int index = i;
            GameObject btn = Instantiate(buttonPrefab, buttonParent);

            TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null)
                btnText.text = workoutData.workoutInfo[i].workoutName;

            btn.GetComponent<Button>().onClick.AddListener(() => OnWorkoutSelected(index));
        }
    }

    void OnWorkoutSelected(int index)
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        isPlaying = false;
        currentBallIndex = 0; // 👈 Reset spawn index
        SetPlayPauseButtonText("Play");

        currentWorkoutIndex = index;
        descriptionText.text = workoutData.workoutInfo[index].description;
    }

    void TogglePlayPause()
    {
        if (currentWorkoutIndex < 0) return;

        if (isPlaying)
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
            SetPlayPauseButtonText("Play");
        }
        else
        {
            WorkoutDetail selectedWorkout = workoutData.workoutInfo[currentWorkoutIndex];
            spawnCoroutine = StartCoroutine(SpawnBalls(selectedWorkout));
            SetPlayPauseButtonText("Pause");
        }

        isPlaying = !isPlaying;
    }

    IEnumerator SpawnBalls(WorkoutDetail workout)
    {
        for (int i = currentBallIndex; i < workout.ballCount; i++)
        {
            Vector3 spawnPos = ballspawnpoint != null ? ballspawnpoint.transform.position : Vector3.zero;

            GameObject ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
            ball.transform.localScale = Vector3.one * 1.5f;
            ball.GetComponent<Renderer>().material.color = Color.red;

            Rigidbody rb = ball.GetComponent<Rigidbody>();

            float directionX = workout.ballDirection switch
            {
                "right" => 0.5f,
                "left" => -0.5f,
                "center" => 0f,
                _ => 0f
            };

            rb.AddForce(new Vector3(directionX, 1f, 1f) * 5f, ForceMode.Impulse);
            Debug.Log($"🟢 Ball {i + 1} spawned at {spawnPos}");

            currentBallIndex = i + 1; // 👈 Update index for resume
            yield return new WaitForSeconds(2f);
        }

        isPlaying = false;
        currentBallIndex = 0; // 👈 Reset after all balls are spawned
        SetPlayPauseButtonText("Play");
    }

    void SetPlayPauseButtonText(string value)
    {
        TextMeshProUGUI btnText = playPauseButton.GetComponentInChildren<TextMeshProUGUI>();
        if (btnText != null)
            btnText.text = value;
    }
}

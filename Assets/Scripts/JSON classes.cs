using System.Collections.Generic;

[System.Serializable]
public class WorkoutDetail
{
    public string workoutName;
    public string description;
    public int ballCount;
    public string ballDirection;
}

[System.Serializable]
public class WorkoutData
{
    public string ProjectName;
    public List<WorkoutDetail> workoutInfo;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    private enum Difficulty
    {
        SleepyGoblin, MightyGoblin, LegendaryGoblin
    }
    [SerializeField] private Difficulty difficulty;

    public void SetDifficulty()
    {
        if (difficulty == Difficulty.SleepyGoblin) MultiScene.multiScene.difficulty = 0;
        else if (difficulty == Difficulty.MightyGoblin) MultiScene.multiScene.difficulty = 1;
        else if (difficulty == Difficulty.LegendaryGoblin) MultiScene.multiScene.difficulty = 2;
        MultiScene.multiScene.UpdateDifficulty(MultiScene.multiScene.difficulty);
        gamemanager.userInterface.ButtonClickAudio();
    }
}

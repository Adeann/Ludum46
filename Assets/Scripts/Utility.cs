using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static GameObject gameObject;

    public static class GameState
    {
        public static bool isPaused;
        public static float gameTime = 0f;

        public static void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
        public static void UnPause()
        {
            isPaused = false;
            Time.timeScale = 1f;
        }

        public static void Toggle()
        {
            if (isPaused)
                UnPause();
            else
                Pause();
        }

    }


}

using System;

namespace Global
{
    public static class App
    {
        public static event Action EndGameEvent;

        public static void EndGame()
        {
            EndGameEvent?.Invoke();
        }
    }
}
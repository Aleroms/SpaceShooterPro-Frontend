using System.Collections.Generic;

namespace SpaceShooterPro.API
{
    [System.Serializable]
    public class HighScoreResponse
    {
        public string username;
        public int highscore;
    }

    // Wrapper for single-user response
    [System.Serializable]
    public class HighScoreResponseWrapper
    {
        public HighScoreResponse response;
    }

    // Wrapper for error response
    [System.Serializable]
    public class HighScoreErrorWrapper
    {
        public HighScoreError error;
    }

    [System.Serializable]
    public class HighScoreError
    {
        public string message;
    }

    // Wrapper for the list of highscores response
    [System.Serializable]
    public class HighScoreListResponseWrapper
    {
        public HighScoreListResponse response;
    }

    [System.Serializable]
    public class HighScoreListResponse
    {
        public List<HighScoreResponse> highscores;
    }
}

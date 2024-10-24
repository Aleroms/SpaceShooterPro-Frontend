namespace SpaceShooterPro.API
{
    [System.Serializable]
    public class HighScoreUpdateRequest
    {
        public string score;
    }

    [System.Serializable]
    public class HighScoreUpdateResponse
    {
        public string success;
    }

    [System.Serializable]
    public class HighScoreUpdateResponseWrapper
    {
        public HighScoreUpdateResponse response;
    }

    [System.Serializable]
    public class HighScoreErrorResponse
    {
        public string error;
    }

    [System.Serializable]
    public class HighScoreDetailedErrorResponse
    {
        public string error;
        public string message;
    }

    [System.Serializable]
    public class HighScoreErrorResponseWrapper
    {
        public HighScoreErrorResponse response;
    }
}

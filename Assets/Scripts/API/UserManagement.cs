namespace SpaceShooterPro.API
{
    public class AuthHeader
    {
        public string Authorization { get; set; }
    }

    [System.Serializable]
    public class DeleteUserResponse
    {
        public string success;
    }

    [System.Serializable]
    public class DeleteUserResponseWrapper
    {
        public DeleteUserResponse response;
    }

    [System.Serializable]
    public class LogoutResponse
    {
        public string success;
    }

    [System.Serializable]
    public class LogoutResponseWrapper
    {
        public LogoutResponse response;
    }
}

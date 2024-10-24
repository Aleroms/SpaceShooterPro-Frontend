using System.Collections.Generic;
namespace SpaceShooterPro.API
{
    [System.Serializable]
    public class AuthRequest
    {
        public string username;
        public string password;
    }

    [System.Serializable]
    public class AuthResponse
    {
        public string success;
        public string session_token;
    }

    [System.Serializable]
    public class AuthResponseWrapper
    {
        public AuthResponse response;
    }

    [System.Serializable]
    public class ErrorResponseWrapper
    {
        public ErrorResponse error;
    }

    [System.Serializable]
    public class ErrorResponse
    {
        public string message;
    }
}

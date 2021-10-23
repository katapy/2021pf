using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    public class Config : MonoBehaviour
    {
        public enum Server
        {
            DockerOnLinux,
            Docker,
            Heroku
        }

        [SerializeField]
        private Server server;

        public static string URL { get; private set; }

        public static string WS { get; private set; }

        private void Awake()
        {
            switch (server)
            {
                case Server.DockerOnLinux:
                    URL = "http://192.168.1.10:8000";
                    WS = "ws://192.168.1.10:8000";
                    break;
                case Server.Docker:
                    URL = "http://localhost:8000";
                    WS = "ws://localhost:8000";
                    break;
                case Server.Heroku:
                    URL = "https://docker-python-001.herokuapp.com";
                    WS = "ws://docker-python-001.herokuapp.com";
                    break;
                default:
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandDrop.Utils
{
    public static class APIUtility
    {
        private static string pusherKey = "de9851a0744ee8227c97";
        private static string pusherSecretKey = "7a898025cb24b73f148e";
        private static string pusherCluster = "us2";
        private static string pusherAppId = "576132";
        public static string PusherKey
        {
            get
            {
                return pusherKey;
            }
        }
        public static string PusherSecretKey
        {
            get
            {
                return pusherSecretKey;
            }
        }
        public static string PusherCluster
        {
            get
            {
                return pusherCluster;
            }
        }
        public static string PusherAppId
        {
            get
            {
                return pusherAppId;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPresenter.Service.CloudSyncService
{
    public static class ServerDataQueue
    {
        private static Stack<string> _queueData;

        static ServerDataQueue()
        {
            _queueData = new Stack<string>();
        }

        public static void Push(string path)
        {
            _queueData.Push(path);
        }

        public static string Pop()
        {
            return _queueData.Pop();
        }
    }
}
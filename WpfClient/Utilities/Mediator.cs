﻿namespace WpfClient.Utilities
{
    class Mediator
    {
        private static IDictionary<string, Action<object>> actions = new Dictionary<string, Action<object>>();

        public static void Register(string token, Action<object> callback)
        {
            if (!actions.ContainsKey(token))
            {
                actions[token] = callback;
            }
        }

        public static void Notify(string token, object args)
        {
            if (actions.ContainsKey(token))
            {
                actions[token](args);
            }
        }
    }
}

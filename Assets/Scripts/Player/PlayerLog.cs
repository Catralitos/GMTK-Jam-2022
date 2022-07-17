using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerLog : MonoBehaviour
    {
        private List<string> EventLog = new List<string>();
        private string guiText = "";

        public Label label;
        public int maxLines = 10;

        private void OnGUI()
        {
            label.text = guiText;
        }

        public void AddEvent(string eventString)
        {
            EventLog.Add(eventString);

            if (EventLog.Count >= maxLines)
                EventLog.RemoveAt(0);

            guiText = "";

            foreach (string logEvent in EventLog)
            {
                guiText += logEvent;
                guiText += "\n";
            }
        }
    }
}
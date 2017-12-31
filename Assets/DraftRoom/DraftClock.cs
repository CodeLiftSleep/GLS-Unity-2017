using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Draft
{
    public class DraftClock : MonoBehaviour
    {

        public static int Minutes = 10;
        public static int Seconds = 0;
        public static float m_timeLeft;
        public static string TimeDisplay = "10:00";
        private void Awake()
        {
            m_timeLeft = GetInitialTime();
        }

        private static float GetInitialTime()
        {
            return Minutes * 60f + Seconds;
        }

        public static int GetLeftMinutes()
        {
            return Mathf.FloorToInt(m_timeLeft / 60f);
        }

        public static int GetLeftSeconds()
        {
            return Mathf.FloorToInt(m_timeLeft % 60f);
        }
        // Update is called once per frame
        public static void Update()
        {
            if (m_timeLeft > 0f)
            {
                //Update Countdown Clock
                m_timeLeft -= Time.deltaTime;
                Minutes = GetLeftMinutes();
                Seconds = GetLeftSeconds();

                if (m_timeLeft > 0f)
                {
                    TimeDisplay = Minutes + ":" + Seconds.ToString("00");
                }
                else
                {
                    TimeDisplay = "00:00";
                }

            }
        }
    }
}
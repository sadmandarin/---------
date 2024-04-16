using System;

namespace MainPage
{
    [Serializable]
    public class TestPlayerData
    {
        public JsonDateTime TimerStartTime;

        public TestPlayerData()
        {
            TimerStartTime = (JsonDateTime)DateTime.Now;
        }
    }
}
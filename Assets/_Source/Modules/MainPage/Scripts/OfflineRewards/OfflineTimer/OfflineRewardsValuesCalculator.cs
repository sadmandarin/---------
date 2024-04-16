using UnityEngine;

namespace MainPage
{
    internal class OfflineRewardsValuesCalculator : MonoBehaviour
    {
        [SerializeField] private float _totalHoursAvailable = 8;
        [SerializeField] private int _maxCoins = 8000;
        [SerializeField] private int _maxGems = 120;
        
        internal void CalculateRewards(float timeLeft, float coinsPerHour, float gemsPerHour, 
            out int coinsAccumulated, out int gemsAccumulated)
        {
            float secondsSpend = _totalHoursAvailable * 3600 - timeLeft;
            float hoursSpend = secondsSpend / 3600;
            coinsAccumulated = Mathf.Clamp((int)(hoursSpend * coinsPerHour), 0, _maxCoins);
            gemsAccumulated = Mathf.Clamp((int)(hoursSpend * gemsPerHour), 0, _maxGems);
        }
    }
}

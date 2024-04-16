using DG.Tweening;
using UnityEngine;

namespace GridBoard
{
    internal class UnitToBoardMover : MonoBehaviour
    {
        [SerializeField] private BoardRoot _boardRoot;
        [SerializeField] private BarracksRoot _barracksRoot;

        internal Sequence MoveUnitToBoardOrBarracks(LightBallTrail ballTrail, string name, int level)
        {
            if (_boardRoot.CanAddNewUnitsToBoard)
            {
                ballTrail.Init(_boardRoot.FreeSpotPositionInWorld(), true);
                return DOTween.Sequence().Append(ballTrail.Move()).
                                          AppendCallback(() =>{ _boardRoot.AddUnitToFreeSpot(name, level);
                });
            }
            else
            {
                ballTrail.Init(_barracksRoot.BarracksButtonPosition, false);
                return DOTween.Sequence().Append(ballTrail.Move()).
                                          AppendCallback(() => { _barracksRoot.AddToBarracksByName(name, level);});
            }
        
        }
    }
}

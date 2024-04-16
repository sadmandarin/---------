using AutoBattler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattlerAndBoardTest : MonoBehaviour
{
    [SerializeField] private LevelConfigBaseSO _levelConfig;
    [SerializeField] private AutoBattlerAndBoardRoot _root;

    private void Start()
    {
        _root.Init(_levelConfig);
    }
}

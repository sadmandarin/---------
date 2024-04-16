using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridBoard
{
    internal class LuckyCardDialog : MonoBehaviour
    {
        internal Action OnSpawnAnotherDialog;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private LuckyCardDialogCardSpawner _cardSpawner;
        [SerializeField] private LuckyCardDialogCardCollecter _cardCollecter;
        [SerializeField] private LuckyCardDialogMoreButton _moreButton;

        private UnitToBoardMover _unitToBoardMover;
        private List<LuckyCardContainer> _cards;

        internal void Init(UnitToBoardMover unitToBoardMover)
        {
            _unitToBoardMover = unitToBoardMover;
            _cards = _cardSpawner.SpawnCards();
        }

        private void HandleOnCardsSpawned()
        {
            _cardCollecter.Init(_unitToBoardMover, _cards);
        }

        private void OnEnable()
        {
            InitCamera();
            _cardSpawner.OnCardsSpawned += HandleOnCardsSpawned;
            _moreButton.OnMoreLuckyDialogPressed += SpawnAnotherDialog;
        }

        private void OnDisable()
        {
            _cardSpawner.OnCardsSpawned -= HandleOnCardsSpawned;
            _moreButton.OnMoreLuckyDialogPressed -= SpawnAnotherDialog;
        }

        private void InitCamera()
        {
            var cameraGameobject = GameObject.FindGameObjectWithTag("CanvasCamera");
            if (cameraGameobject.TryGetComponent(out Camera camera))
            {
                _canvas.worldCamera = camera;
            }
        }

        private void DestroyDialog()
        {
            Destroy(gameObject);
        }

        private void SpawnAnotherDialog()
        {
            OnSpawnAnotherDialog?.Invoke();
            DestroyDialog();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class CastleGraph : MonoBehaviour
    {
        public GameObject[] castles;

        public List<GameObject> connectCastles;

        public LineRenderer lineRendererPrefab; // Префаб Line Renderer для визуализации дороги

        public TrailRenderer trailRenderer;

        List<LineRenderer> roadVert;

        //public List<CastleNode> castleObjects;
        private void Start()
        {
            roadVert = new List<LineRenderer>();

            connectCastles = new List<GameObject>();

            InitializeRoad();
        }

        private void InitializeRoad()
        {
            bool[,] matrixRoad = new bool[castles.Length, castles.Length];

            SetRoadBetweenCastles(0, 1, true);

            SetRoadBetweenCastles(1, 2, true);
        }

        public void SetRoadBetweenCastles(int castle1Index, int castle2Index, bool hasRoad)
        {
            if (castle1Index >= 0 && castle1Index < castles.Length && castle2Index >= 0 && castle2Index < castles.Length)
            {
                SetRoadToCastle(castles[castle1Index], hasRoad);

                SetRoadToCastle(castles[castle2Index], hasRoad);

                BuildRoads(castle1Index, castle2Index);
            }
        }

        public void SetRoadToCastle(GameObject otherCastle, bool hasRoad)
        {
            if (hasRoad)
            {
                if (!connectCastles.Contains(otherCastle))
                {
                    connectCastles.Add(otherCastle);
                    Debug.DrawLine(gameObject.transform.position, otherCastle.transform.position, Color.red, 10f);
                }
            }
            else
            {
                connectCastles.Remove(otherCastle);
            }
        }

        void BuildRoads(int castle1Index, int castle2Index)
        {
            
            Transform startVertex = connectCastles[castle1Index].transform;

            Transform endVertex = connectCastles[castle2Index].transform;

            // Создаем новый Line Renderer
            LineRenderer roadRenderer = Instantiate(lineRendererPrefab, transform);

            roadVert.Add(roadRenderer);


            roadRenderer.SetPosition(0, startVertex.position);

            roadRenderer.SetPosition(1, endVertex.position);

            trailRenderer.emitting = true;

            trailRenderer.Clear(); // Очищаем предыдущие точки

            //Vector3[] linePositions = new Vector3[lineRendererPrefab.positionCount];

            //lineRendererPrefab.GetPositions(linePositions);

            //foreach (var position in linePositions)
            //{
            //    trailRenderer.AddPosition(position);
            //}            
        }
    }
}

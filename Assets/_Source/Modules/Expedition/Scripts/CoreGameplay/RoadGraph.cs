using System.Collections.Generic;
using UnityEngine;

namespace Expedition
{
    public class RoadGraph : MonoBehaviour
    {
        [SerializeField]
        private GameObject spriteRendererPrefab;

        [SerializeField]
        private GameObject upSpriteRendererPrefab;

        [SerializeField]
        private GameObject downSpriteRendererPrefab;

        [SerializeField]
        private Sprite upRoadSprite;

        [SerializeField] 
        private Sprite downRoadSprite;
        
        private Graph graph;

        private GameManager gameManager;

        void Start()
        {
            graph = GameObject.Find("Graph").GetComponent<Graph>();

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            CreateRoad();
        }

        void CreateLineRenderer(CastleNode startCastle, CastleNode endCastle)
        {
            float distanceBetweenCastles = Vector3.Distance(startCastle.transform.position, endCastle.transform.position);

            int segments = Mathf.Max(1, Mathf.RoundToInt(distanceBetweenCastles / 0.4f));

            for (int i = 0; i < segments; i++)
            {

                // Создаем новый сегмент дороги
                GameObject upRoadSegment = Instantiate(upSpriteRendererPrefab, transform);

                upRoadSegment.transform.Rotate(30f, 0, 0);

                GameObject downRoadSegment = Instantiate(downSpriteRendererPrefab, transform);

                downRoadSegment.transform.Rotate(30f, 0, 0);

                // Получаем компонент SpriteRenderer для сегмента
                SpriteRenderer upSpriteRenderer = upRoadSegment.GetComponent<SpriteRenderer>();

                SpriteRenderer downSpriteRenderer = downRoadSegment.GetComponent<SpriteRenderer>();

                upSpriteRenderer.sortingOrder = 2;

                downSpriteRenderer.sortingOrder = 1;

                // Располагаем сегмент между замками
                float t = i / (float)(segments - 1);
                Vector3 position = Vector3.Lerp(startCastle.transform.position, endCastle.transform.position, t);

                upRoadSegment.transform.position = position + new Vector3(0, -0.5f, 0);

                downRoadSegment.transform.position = position + new Vector3(0, -0.4f, 0);
            }
        }

        void CreateRoad()
        {
            switch (gameManager.level)
            {
                case 1:
                    graph.AddRoadBetweenCastles(gameManager.castle[0], graph.castleNodes[0], graph.castleNodes[1]);

                    graph.AddRoadBetweenCastles(gameManager.castle[1], graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    break;
                
                case 2:
                    graph.AddRoadBetweenCastles(gameManager.castle[0], graph.castleNodes[0], graph.castleNodes[3]);

                    graph.AddRoadBetweenCastles(gameManager.castle[1], graph.castleNodes[1], graph.castleNodes[3]);

                    graph.AddRoadBetweenCastles(gameManager.castle[2], graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    break;

                case 3:

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    break;
                case 4:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[6]);
                    break;
                case 5:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);
                    break;
                case 6:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[7]);
                    break;
                case 7:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);
                    break;

                case 8:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[9]);
                    break;
                case 9:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);
                    break;
                case 10:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);
                    break;
                case 11:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[8]);

                    break;
                case 12:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[5]);
                    break;

                case 13:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[8]);
                    break;
                case 14:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[0]);
                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[4]);
                    break;
                case 15:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[9]);

                    break;
                case 16:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);
                    break;
                case 17:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[9]);
                    break;
                case 18:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[11]);
                    break;

                case 19:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);
                    break;
                case 20:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[11]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[5]);
                    break;
                case 21:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[4]);
                    break;
                case 22:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);
                    break;
                case 23:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[7]);
                    break;
                case 24:

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);
                    break;
                case 25:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);
                    break;
                case 26:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[7]);
                    break;
                case 27:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[3]);
                    break;
                case 28:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[3]);
                    break;
                case 29:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[6]);
                    break;
                case 30:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);
                    
                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[7]);
                    break;

                case 31:
                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[5]);
                    break;

                case 32:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[2]);
                    break;
                case 33:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[8]);
                    break;
                case 34:
                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[0]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[0]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);
                    break;
                case 35:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[6], graph.castleNodes[8]);
                    break;
                case 36:
                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[0]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[12]);

                    CreateLineRenderer(graph.castleNodes[11], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[11], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[11], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[11], graph.castleNodes[12]);

                    CreateLineRenderer(graph.castleNodes[12], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[12], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[6]);
                    break;
                case 37:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[3]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[10], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[8]);
                    break;
                case 38:
                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[0]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[8]);
                    break;
                case 39:
                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[0]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[1]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[5]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[9], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[8], graph.castleNodes[7]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);
                    break;
                case 40:
                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[0], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[2]);

                    CreateLineRenderer(graph.castleNodes[1], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[2], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[5], graph.castleNodes[6]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[7], graph.castleNodes[10]);

                    CreateLineRenderer(graph.castleNodes[4], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[8]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[9]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[4]);

                    CreateLineRenderer(graph.castleNodes[3], graph.castleNodes[2]);
                    break;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public enum PiceType
    {
        NORMAL,
        COUNT,
    };

    [Header("GridTile")]
    [SerializeField] private int wigth;
    [SerializeField] private int height;
    private GamePice[,] Pices;

    [System.Serializable]
    public struct PiecePrefab
    {
        public PiceType type;
        public GameObject Prefab;
    };

    [Header("Gear Piece")]
    public PiecePrefab[] piecePrefabs;

    [Header("Dictionary")]
    private Dictionary<PiceType, GameObject> PicePrefabDict;

    void Start()
    {
        PicePrefabDict = new Dictionary<PiceType, GameObject>();

        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!PicePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                PicePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].Prefab);
            }
        }

        Pices = new GamePice[wigth, height];

        for (int x = 0; x < wigth; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newPiece = (GameObject)Instantiate(PicePrefabDict[PiceType.NORMAL], GetWorldPostion(x, y), Quaternion.identity);
                newPiece.name = "Piece(" + x + "," + y + ")";
                newPiece.transform.parent = transform;

                Pices[x, y] = newPiece.GetComponent<GamePice>();
                Pices[x, y].Init(x, y, this, PiceType.NORMAL);
            }
        }

    }

    /// <summary>
    /// Get The World Center Point.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    Vector2 GetWorldPostion(int x, int y)
    {
        return new Vector2(transform.position.x - wigth / 2.0f + x, transform.position.y + height / 2.0f - y);
    }

}

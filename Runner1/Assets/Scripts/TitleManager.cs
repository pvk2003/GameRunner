using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;

    public float tileLength = 30;
    public int numberOfTiles = 8;
    public int totalNumOfTiles = 8;

    public float zSpawn = 0;

    private Transform playerTransform;

    private int previousIndex;

    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(); // Tile đầu tiên
            else
                SpawnTile(Random.Range(0, Mathf.Min(totalNumOfTiles, tilePrefabs.Length))); // Tile tiếp theo
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 30 >= zSpawn - (numberOfTiles * tileLength))
        {
            int index = Random.Range(0, totalNumOfTiles);

            // Đảm bảo không lặp lại index trước đó
            while (index == previousIndex)
            {
                index = Random.Range(0, totalNumOfTiles);
            }

            SpawnTile(index); // Sinh tile mới
            DeleteTile();     // Xóa tile cũ
        }
    }


    public void SpawnTile(int index = 0)
    {
        // Tạo một tile từ prefab
        GameObject tile = tilePrefabs[index];

        // Tạo tile mới nếu tile được chọn đang hoạt động
        if (tile.activeInHierarchy)
        {
            for (int i = 0; i < tilePrefabs.Length; i++)
            {
                if (!tilePrefabs[i].activeInHierarchy)
                {
                    tile = tilePrefabs[i];
                    break;
                }
            }
        }

        // Đặt vị trí và kích thước cho tile
        tile.transform.position = Vector3.forward * zSpawn;
        tile.transform.rotation = Quaternion.identity;
        tile.SetActive(true);

        // Thêm tile vào danh sách đang hoạt động
        activeTiles.Add(tile);
        zSpawn += tileLength;

        // Cập nhật chỉ số trước đó
        previousIndex = index;
    }


    private void DeleteTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
        PlayerManager.score += 3;  // Tăng điểm
    }

}

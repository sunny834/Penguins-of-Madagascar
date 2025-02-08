using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    //GamePlay
    private float ChunkSpawnZ;
    private Queue<Chunk> activeChunks=new Queue<Chunk>();
    private List<Chunk> chunkpool=new List<Chunk>();

    //Configurable field
    [SerializeField] private int FirstChunkSpawnPositions=-20;
    [SerializeField] private int ChunkOnSize=5;
    [SerializeField] private float DeSpawnDistance = 5.0f;
    [SerializeField] private List<GameObject> Chunksprefabs;
    [SerializeField] private Transform CameraTransform;
    private void Start()
    {
        if (Chunksprefabs.Count==0)
        {
            Debug.Log("Didn't assign any chunk prefab");
            return;
            
        }
        if(!CameraTransform)
        {
            CameraTransform = Camera.main.transform;
            Debug.Log("auto assigned the camera transform to camera main");
        }
    }
            
    private void Update()
    {
        ScanPosition();
    }
    private void ScanPosition()
    {


    }
    private void ScanNewChunk()
    {

    }
    private void DeleteLastChunk()
    {

    }
    public void ResetWorld()
    {
        
    }
}

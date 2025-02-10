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

    #region TO DELETE
    private void Awake()
    {
        ResetWorld();
    }
    #endregion
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
        float cameraZ=CameraTransform.position.z;
        Chunk LastChunk= activeChunks.Peek();
        if (cameraZ >= LastChunk.transform.position.z + LastChunk.chunkLength + DeSpawnDistance)
        { 
            SpawnNewChunk();
            DeleteLastChunk();
        }

    }
    private void SpawnNewChunk()
    {
        int randomIndex = Random.Range(0, Chunksprefabs.Count);
        Chunk chunk = chunkpool.Find(x=>!x.gameObject.activeSelf && x.name == (Chunksprefabs[randomIndex].name + "(Clone)"));
        if(!chunk)
        {
            GameObject go = Instantiate(Chunksprefabs[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }
        chunk.transform.position =  new Vector3(0,0,ChunkSpawnZ);
        ChunkSpawnZ += chunk.chunkLength;
        activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }
    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkpool.Add(chunk);
    }
    public void ResetWorld()
    {
        ChunkSpawnZ = FirstChunkSpawnPositions;
        for (int i = activeChunks.Count; i != 0; i--)
        {
            DeleteLastChunk();

        }
        for (int i = 0;i<ChunkOnSize;i++)
        {
            SpawnNewChunk();
        }
    }
}

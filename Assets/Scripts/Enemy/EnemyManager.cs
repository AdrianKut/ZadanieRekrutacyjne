using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private EnemyInstance m_EnemyInstanceObject = null;
    [SerializeField] private Transform m_EnemyInstanceTransform = null;
    [SerializeField] private int m_AmountOfObjects;
    [SerializeField] private float m_SpawnDelay = 0.05f;
    private List<EnemyInstance> m_ListGameObjectPool;

    private Coroutine m_Coroutine;

    [SerializeField] private BoxCollider2D m_SpawnArea;

    protected override void Awake()
    {
        base.Awake();
        InstantiatePoolObjects();
    }

    private void Start()
    {
        m_Coroutine = StartCoroutine( SpawnEnemies() );
    }

    private void OnDestroy()
    {
        if( m_Coroutine != null )
        {
            StopCoroutine( m_Coroutine );
            m_Coroutine = null;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds delay = new WaitForSeconds( m_SpawnDelay );
        for( int i = 0; i < m_AmountOfObjects; i++ )
        {
            EnemyInstance pooledObject = GetPooledObject();
            yield return delay;
        }
    }

    private void InstantiatePoolObjects()
    {
        m_ListGameObjectPool = new List<EnemyInstance>();
        for( int i = 0; i < m_AmountOfObjects; i++ )
        {
            EnemyInstance newEnemy = Instantiate( m_EnemyInstanceObject, m_EnemyInstanceTransform );
            newEnemy.transform.position = GetRandomPosition();
            newEnemy.Initialize( i + 1 );
            newEnemy.name = "Enemy_" + (i + 1);
            newEnemy.gameObject.SetActive( false );
            m_ListGameObjectPool.Add( newEnemy );
        }
    }

    public EnemyInstance GetPooledObject()
    {
        for( int i = 0; i < m_ListGameObjectPool.Count; i++ )
        {
            if( m_ListGameObjectPool[i].gameObject.activeInHierarchy == false )
            {
                m_ListGameObjectPool[i].gameObject.SetActive( true );
                return m_ListGameObjectPool[i];
            }
        }

        return null;
    }

    private Vector2 GetRandomPosition()
    {
        Vector3 min = m_SpawnArea.bounds.min;
        Vector3 max = m_SpawnArea.bounds.max;

        float x = Random.Range( min.x, max.x );
        float y = Random.Range( min.y, max.y );

        return new Vector2( x, y );
    }
}

using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    //敌人的Prefab
    public Transform m_enemy;

    //生成的敌人数量
    public int m_enemyCount = 0;

    //敌人的最大生成数量
    public int m_maxEnemy = 3;

    //生成敌人的时间间隔
    public float m_timer = 0;

    protected Transform m_transform;


	// Use this for initialization
	void Start () {
        m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        if ( m_enemyCount >= m_maxEnemy )
        {
            return;
        }

        m_timer -= Time.deltaTime;
        if ( m_timer <= 0 )
        {
            m_timer = Random.value * 30.0f;

            if ( m_timer < 10 )
            {
                m_timer = 10;
            }

            Transform obj = (Transform)Instantiate( m_enemy, m_transform.position, Quaternion.identity );

            Enemy enemy = obj.GetComponent<Enemy>();

            enemy.init( this );
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon( transform.position, "item.png", true );
    }
}

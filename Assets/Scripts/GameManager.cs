﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    //游戏得分
    int m_score = 0;

    //游戏最高得分
    static int m_hiscore = 0;

    //弹药数量
    int m_ammo = 100;

    //游戏主角
    Player m_player;

    //UI文字
    UILabel txt_ammo;
    UILabel txt_hiscore;
    UILabel txt_life;
    UILabel txt_score;

	// Use this for initialization
	void Start () {
        Instance = this;

        //获得主角
        m_player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Player>();

        //获得设置的UI文字
        txt_ammo = this.transform.FindChild( "txt_ammo" ).GetComponent<UILabel>();
        txt_hiscore = this.transform.FindChild( "txt_hiscore" ).GetComponent<UILabel>();
        txt_life = this.transform.FindChild( "txt_life" ).GetComponent<UILabel>();
        txt_score = this.transform.FindChild( "txt_score" ).GetComponent<UILabel>();

	}
	
	// Update is called once per frame
	void Update () {
    
	}

    //更新分数
    public void SetScore(int score)
    {
        m_score += score;

        if ( m_score > m_hiscore )
        {
            m_hiscore = m_score;
        }

        txt_score.text = "Score " + m_score;
        txt_hiscore.text = "Hiscore " + m_hiscore;
    }

    //更新弹药
    public void SetAmmo( int ammo )
    {
        m_ammo -= ammo;

        if ( m_ammo <= 0 )
        {
            m_ammo = 100 - m_ammo;
        }

        txt_ammo.text = m_ammo.ToString() + "/100";
    }

    public void SetLife( int life )
    {
        txt_life.text = life.ToString();
    }

    void OnGUI()
    {
        if ( m_player.m_life <= 0 )
        {
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;

            GUI.skin.label.fontSize = 40;

            GUI.Label( new Rect( 0, 0, Screen.width, Screen.height ), "Game Over" );

            GUI.skin.label.fontSize = 30;

            if ( GUI.Button( new Rect( Screen.width * 0.5f - 150, Screen.height * 0.75f, 300, 40 ), "Try again" ) )
            {
                Application.LoadLevel( Application.loadedLevelName );
            }
        }
    }
}

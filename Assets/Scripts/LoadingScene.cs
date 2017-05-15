using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

    //先声明一个静态字符串变量来保存要加载场景的名称
    public static string LoadingName;

    //引用UI组件
    public UISlider slider;
    public UILabel label;


    //声明一个异步进度变量
    AsyncOperation asyn;

	// Use this for initialization
	void Start () {
        if ( slider == null )
        {
            Debug.Log( "进度条组件丢失！" );
        }

        if ( label == null )
        {
            Debug.Log( "进度显示文字丢失！" );
        }

        //进入这个场景就立即协程加载新场景
        StartCoroutine( "BeginLoading" );
	}
	
	// Update is called once per frame
	void Update () {
        //更新UI
        slider.value = asyn.progress;
        label.text = "加载进度：[FF0000]" + ( slider.value * 100 ).ToString( ".00" ) + "%";
	}

    IEnumerator BeginLoading()
    {
        asyn = Application.LoadLevelAsync( LoadingName );
        yield return asyn;
    }

    //设计一个封装好的静态函数
    public static void LoadNewScene(string value)
    {
        LoadingName = value;
        Application.LoadLevel( "LoadingScene" );
    }
}

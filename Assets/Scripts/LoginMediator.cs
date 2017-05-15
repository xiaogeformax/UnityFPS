using UnityEngine;
using System.Collections;

public class LoginMediator : MonoBehaviour {

    public UIButton btnSure;

    public UIButton btnCancel;

    public UIInput lblName;

    public UIInput lblPwd;

    void Start()
    {
        if ( btnSure != null )
        {
            EventDelegate edSure = new EventDelegate( this, "onBtnSureClickHandler" );

            //EventDelegate.Add( btnSure.onClick, edSure );

            btnSure.onClick.Add( edSure );
        }

        if ( btnCancel != null )
        {
            EventDelegate edSure = new EventDelegate( this, "onBtnCancelClickHandler" );

            //EventDelegate.Add( btnSure.onClick, edSure );

            btnCancel.onClick.Add( edSure );
        }
    }

    void Update()
    {

    }

    public void onBtnSureClickHandler()
    {
        //Debug.Log( "onBtnSureClickHandler" );
        if ( lblName == null || lblName.value.Length == 0 )
        {
            Debug.Log( "请输入名称！" );
            return;
        }
        if ( lblPwd == null || lblPwd.value.Length == 0 )
        {
            Debug.Log( "请输入密码！" );
            return;
        }

        //Debug.Log( "名称：" + lblName.value );
        //Debug.Log( "密码：" + lblPwd.value );

        //LoadingScene.LoadNewScene( "demo" );

        Server.Instance.Login( lblName.value, lblPwd.value );
    }

    public void onBtnCancelClickHandler()
    {
        //Debug.Log( "onBtnCancelClickHandler" );
        Application.Quit();
    }

}

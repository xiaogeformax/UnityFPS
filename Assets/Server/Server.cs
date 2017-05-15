using UnityEngine;
using System.Collections;
using UnityNetwork;
public class Server : MonoBehaviour {

    public static Server Instance = null;

    ServerManager _SvrMgr;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad( this );

        Instance = this;

        _SvrMgr = new ServerManager();
        _SvrMgr.Start();
	}
	
	// Update is called once per frame
	void Update () {
        _SvrMgr.Update();
	}

    public void Login(string name,string pwd)
    {
        Debug.Log( "name = " + name + "; pwd = " + pwd );

        NetBitStream stream = new NetBitStream();
        stream.BeginWrite( (ushort)Protocol.ID.LOGIN );

        LoginProto.Login login = new LoginProto.Login();
        login.name = name;
        login.pwd = pwd;

        System.IO.MemoryStream mstream = new System.IO.MemoryStream();
        ProtoBuf.Serializer.Serialize<LoginProto.Login>( mstream, login );
        byte[] bs = mstream.ToArray();

        stream.WriteBodyBytes( bs );
        stream.EncodeHeader();

        _SvrMgr.Send( stream );
    }
}

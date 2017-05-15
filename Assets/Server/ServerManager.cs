using UnityEngine;
using System.Collections;
using UnityNetwork;

public class ServerManager : NetworkManager {

    //客户端对象
    NetTCPClient client = null;

	// Use this for initialization
	public void Start () {
        client = new NetTCPClient();

        client.Connect( "192.168.1.102", 5000 );
	}

    public void Send( NetBitStream stream )
    {
        client.Send( stream );
    }
	
	// Update is called once per frame
	public override void Update () {
        NetPacket packet = null;

        for ( packet = GetPacket(); packet != null; )
        {
            ushort msgid = 0;
            packet.TOID( out msgid );

            switch(msgid)
            {
                case (ushort)MessageIdentifiers.ID.CONNECTION_REQUEST_ACCEPTED:
                    {
                        Debug.Log( "连接到服务器" );
                        break;
                    }
                case (ushort)MessageIdentifiers.ID.CONNECTION_ATTEMPT_FAILED:
                    {
                        Debug.Log( "连接服务器失败，请退出" );
                        break;
                    }
                case (ushort)MessageIdentifiers.ID.CONNCET_LOST:
                    {
                        Debug.Log( "丢失服务器的连接，请按任意键退出" );
                        break;
                    }
                case (ushort)Protocol.ID.LOGIN:
                    {
                        NetBitStream stream = new NetBitStream();

                        stream.BeginRead2( packet );

                        stream.DecodeHeader();

                        byte[] bs = stream.ReadBodyBytes();
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream( bs );
                        CommonProto.Response response = ProtoBuf.Serializer.Deserialize<CommonProto.Response>( mstream );

                        Debug.Log( "responseid = "+response.id + "; " + response.desc );

                        if ( response.id == 0 ) { 
                            LoadingScene.LoadNewScene( "demo" );
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            packet = null;
        }
	}
}

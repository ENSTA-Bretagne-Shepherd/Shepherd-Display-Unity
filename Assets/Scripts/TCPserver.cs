using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

public class TCPserver:MonoBehaviour{
	// Global access
	public static TCPserver tcpserver;

	// Connection state
	public bool isConnection;
	public Thread mThread;
	public TcpListener server;

	// Received data
	// private string data;
	public Queue<String> TaskQueue = new Queue<String>();
	public object _queueLock = new object();

	// Access methods
	public Queue<String> getTaskQueue(){ 
		return TaskQueue;
	}
	public object getQueueLock(){
		return _queueLock;
	}

	void Awake(){
		tcpserver = (tcpserver == null) ? this : tcpserver;
	}

	// Use this for initialization
	void Start(){
		isConnection=false;

		print ("StartThread");
		ThreadStart ts = new ThreadStart(ServerThread);
		mThread = new Thread(ts);
		mThread.Start();

	}
	void Update () {
		// lock (_queueLock)
		// {
		// 	if (TaskQueue.Count > 0) {
		// 		string data=TaskQueue.Dequeue ();
		// 		updateAuv (data);
		// 	}
		// }
	}

	void OnApplicationQuit(){
		//server.Stop();
		mThread.Abort (); 
	}

	void ServerThread()
	{  
		TcpListener server=null;  
		try
		{
			// Set the TcpListener on port 13000.
			Int32 port = 13000;
			//IPAddress localAddr = IPAddress.Parse("127.0.0.1");

			// TcpListener server = new TcpListener(port);
			server = new TcpListener(IPAddress.Any, port);

			// Start listening for client requests.
			server.Start();

			// Buffer for reading data
			Byte[] bytes = new Byte[256];

			// Enter the listening loop.
			while(true)
			{
				//Thread.Sleep(10);
				Debug.Log("Waiting for a connection... ");

				// Perform a blocking call to accept requests.
				// You could also user server.AcceptSocket() here.
				TcpClient client = server.AcceptTcpClient();  
				if(client!=null){

					Debug.Log("Connected!");
				}


				// Get a stream object for reading and writing
				NetworkStream stream = client.GetStream();

				string tmp;

				// Loop to receive all the data sent by the client.
				while(stream.Read(bytes, 0, bytes.Length) != 0)
				{  
					// Translate data bytes to a ASCII string.
					string[] data = System.Text.Encoding.ASCII.GetString(bytes).Split('\n');
					
					// if(data[data.Length-1][data[data.Length-1].Length-1] != '}'){
					// 	tmp = data[data.Length-1];
					// 	data = data.Take(data.Count()-1).ToArray();
					// }else{
					// 	tmp = "";
					// }
					// data[0] = tmp + data[0];

					//add situation message to queue
					lock (_queueLock)
					{
						foreach(var s in data){
							if(s.Length != 0){
								if (TaskQueue.Count < 100)
									TaskQueue.Enqueue(s);
									Debug.Log("Queue: " + s);
							}
						}
					}


					// // Process the data sent by the client.
					// data = data.ToUpper();

					// byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);


					// // Send back a response.
					// stream.Write(msg, 0, msg.Length);
					// Debug.Log("Sent:"+ data);          
				}

				// Shutdown and end connection
				client.Close();
			}
		}
		catch(SocketException e)
		{
			Debug.Log("SocketException:"+ e);
		}
		finally
		{
			// Stop listening for new clients.
			server.Stop();
		}

		//yield return null;

	}

	// void updateAuv(string data)
	// {
	// 	Debug.Log ("enter update");

	// 	//Auv indexAuv speed x y z
	// 	// string[] splitData = data.Split (" "[0]);
	// 	// int indexAuv = int.Parse(splitData [1]);
	// 	// int speed = int.Parse(splitData [2]);
	// 	// Vector3 target = new Vector3 (int.Parse(splitData [3]), int.Parse(splitData [4]), int.Parse(splitData [5]));

	// 	// auvs[indexAuv].GetComponent<AuvBehaviour>().setSpeed(speed);
	// 	// auvs[indexAuv].GetComponent<AuvBehaviour>().setTarget (target);

	// 	// Debug.Log("Order : auv  "+ indexAuv+"  move to " + target+" speed  : "+speed);
	// 	AuvSerialized auv = AuvSerialized.CreateFromJSON(data);
	// 	Debug.Log("Order : auv  " + auv.name + "  move to " + auv.x + "," + auv.y);
		
	// }
}	
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace WhiteWhale {
	class Program {
		/// <summary>
		/// The URI for the WebSocket
		/// </summary>
		static string url = "wss://socket.fleetfreedom.com/";
		/// <summary>
		/// Unique identifier of the Company you want to use for the test.
		/// </summary>
		static ulong companyId = 4000;
		/// <summary>
		/// The WebSocket client
		/// </summary>
		static WebSocket socket;
		/// <summary>
		/// The request identifier so that you correlate responses.
		/// </summary>
		static uint reqId = 0;
		/// <summary>
		/// username, password, session identifier, and UserAgent.
		/// </summary>
		static string email, pass, ghostId, ua = "WhiteWhale/1.0 (" +
			Environment.MachineName.ToLower() + "; " +
			Process.GetCurrentProcess().Id +
			(Environment.Is64BitProcess ? "x86" : "x64") +
			"; debug" +
		") Name/Example Socket Client";

		/// <summary>
		/// Runs the example program.
		/// </summary>
		/// <param name="args">Either one string as session identifier, or two strings as username and password.</param>
		static void Main(string[] args) {
			if (args.Length != 1 && args.Length != 2) {
				// no args; display usage notes
				Console.WriteLine("WhiteWhale usage:");
				Console.WriteLine("WhiteWhale [sessionId]");
				Console.WriteLine("WhiteWhale [username] [password]");
				return;
			} else if (args.Length == 1) {
				// append session identifier to connection string for resuming a session
				url += args[0];
			} else if (args.Length == 2) {
				// two arguments; store username and password
				email = args[0];
				pass = args[1];
			}

			// create the socket with the Fleet Freedom URI (and append session identifier for resuming a session)
			socket = new WebSocket(url);
			socket.Opened += opened;
			socket.Error += errored;
			if (!string.IsNullOrEmpty(email)) socket.MessageReceived += doLogin;	// automatic login
			socket.MessageReceived += received;
			socket.Closed += closed;

			// wait for session to be initialized
			socket.Open();
			while (string.IsNullOrEmpty(ghostId)) Thread.Sleep(16);

			// example subscription
			var subscription = new {
				company = new {
					id = companyId
				},
				// see https://apis.fleetfreedom.com/wss/#enum-subscriptiontypes for a full list
				subscriptionTypes = new string[] {
					"assetGeneral",
					"assetAdvanced"
				}
			};

			// press any key to begin subscription
			Console.ReadKey(true);
			send("subscribe", subscription);

			// press any key for a list of assets (example command)
			Console.ReadKey(true);
			send("getAssetsList", new {
				company = new {
					id = companyId
				}
			});

			// press any key to end subscriptions
			Console.ReadKey(true);
			send("unsubscribe", subscription);

			// press any key to logout
			Console.ReadKey(true);
			send("logout", new { });

			// wait for socket to close
			while (socket.State != WebSocketState.Closed) Thread.Sleep(16);
		}

		/// <summary>
		/// Write a message to the console window and to the log file.
		/// </summary>
		/// <param name="msg"></param>
		static void write(string msg) {
			Console.WriteLine(msg);
			Debug.WriteLine(msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
		}
		/// <summary>
		/// Send a message to the socket server.
		/// </summary>
		/// <param name="command">The command name, see https://apis.fleetfreedom.com/wss/#methods for a full list.</param>
		/// <param name="json">The object used to define the </param>
		static void send(string command, object obj) {
			dynamic json = JObject.FromObject(obj);
			json.reqId = ++reqId;
			write("--------- sending ---------");
			write(command + " " + JsonConvert.SerializeObject(json, Formatting.Indented));
			write("--------- sending ---------");
			socket.Send(command + " " + JsonConvert.SerializeObject(json));
		}

		/// <summary>
		/// Handler for when the socket connection to the server is established.
		/// An established connection does not mean the service is ready for commands.
		/// You must wait for the "connectionResponse" message first before sending any commands.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void opened(object sender, EventArgs e) {
			write("--------- socket open ---------");
			write("socket state: " + socket.State.ToString());
			write("--------- socket open ---------");
		}
		/// <summary>
		/// An auto-login handler which is only raised once.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void doLogin(object sender, MessageReceivedEventArgs e) {
			socket.MessageReceived -= doLogin;
			send("login", new {
				username = email,
				password = pass,
				userAgent = ua
			});
		}
		/// <summary>
		/// Handles socket protocol errors.
		/// Protocol errors are not the same as Fleet Freedom error messages.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Contains the exception object</param>
		static void errored(object sender, ErrorEventArgs e) {
			write("--------- socket error ---------");
			write(e.Exception.Message);
			write("--------- socket error ---------");
		}
		/// <summary>
		/// Logs all received messages from the server.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void received(object sender, MessageReceivedEventArgs e) {
			string msgName = e.Message.Substring(0, e.Message.IndexOf(" "));
			var msgJSON = JsonConvert.DeserializeObject<JObject>(e.Message.Substring(msgName.Length + 1));

			write("--------- received ---------");
			write(msgName + " " + JsonConvert.SerializeObject(msgJSON, Formatting.Indented));
			write("--------- received ---------");
			switch (msgName) {
				case "connectionResponse":
					write("socket is ready for commands!");
					// see https://apis.fleetfreedom.com/wss/#enum-errorcodes for a full list of errors
					if ((int)msgJSON["errorCode"] > 0) {
						write("errorCode: " + msgJSON["errorCode"].ToString());
					} else {
						ghostId = msgJSON["ghostId"].ToString();
						write("session: " + ghostId);
					}
					break;
				case "loginResponse":
					write("logged in!");
					// save your session identifier
					ghostId = msgJSON["ghostId"].ToString();
					write("session: " + ghostId);
					break;
				case "logoutResponse":
					write("logged out!");
					socket.Close("bye");
					break;
			}
		}
		/// <summary>
		/// Handler for the socket's connection state being closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void closed(object sender, EventArgs e) {
			write("--------- socket closed ---------");
			write("socket state: " + socket.State.ToString());
			write("--------- socket closed ---------");
		}
	}
}
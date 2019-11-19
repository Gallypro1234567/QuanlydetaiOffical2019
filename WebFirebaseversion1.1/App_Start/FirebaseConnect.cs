using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVCFirebaseOffical.App_Start
{
    public class FirebaseConnect
    {
        public static FirestoreDb connectFB(string path)
        {
            //string path = Server.MapPath("connectFB/BaaSconn.json");
            GoogleCredential cred = GoogleCredential.FromFile(path);
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.Host,
                          FirestoreClient.DefaultEndpoint.Port,
                          cred.ToChannelCredentials());
            FirestoreClient client = FirestoreClient.Create(channel);
            FirestoreDb db = FirestoreDb.Create("quanlydetaioffical2019", client);
            return db;
        }
    }
}
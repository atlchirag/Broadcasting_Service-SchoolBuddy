using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Messaging;

namespace TrackerAlertNotifications
{
    public class General
    {
        
        public static readonly string connectionString = "Data Source={server};Initial Catalog={catalog};User ID={id};Password={password};Max Pool Size=32767;TrustServerCertificate=True;";

        // ✅ Execute INSERT, UPDATE, DELETE Queries
        public static void DML(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToLogFile($"SQL Error: {ex.Message}", AppDomain.CurrentDomain.BaseDirectory, "dbError.txt");
            }
        }

        // ✅ Execute SELECT Queries
        public static DataTable SelectQuery(string query)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                WriteToLogFile($"SQL Error: {ex.Message}", AppDomain.CurrentDomain.BaseDirectory, "dbError.txt");
                return null;
            }
        }

        // ✅ Write Logs to File
        public static void WriteToLogFile(string msg, string folderPath, string logFile)
        {
            string logFilePath = Path.Combine(folderPath, logFile);
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {msg}");
                }
            }
            catch { }
        }
    }

    public class Firebase
    {
        string pathToServiceAccountKey = AppDomain.CurrentDomain.BaseDirectory;

        public Firebase()
        {
            var defaultApp = FirebaseApp.DefaultInstance;
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    //Credential = GoogleCredential.FromFile($"{pathToServiceAccountKey}\\schoolbuddy-4fc6d-firebase-adminsdk-xh2kk-f674f1807a.json"),
                    Credential = GoogleCredential.FromFile($"{pathToServiceAccountKey}\\schoolbuddy-4fc6d-firebase-adminsdk-xh2kk-5fac47bd25.json"),
                });
            }
        }

        // ✅ Send FCM Notification to a Single User
        public async Task SendNotification(string auidoriuid, string msg, string uid)
        {
            try
            {
                var message = new Message()
                {
                    Token = auidoriuid,
                    Notification = new Notification()
                    {
                        Title = "Broadcast Alert",
                        Body = msg
                    }
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                General.WriteToLogFile($"{uid}: {msg} - Notification Sent", AppDomain.CurrentDomain.BaseDirectory, "notification.txt");
            }
            catch (Exception ex)
            {
                General.WriteToLogFile($"Notification Error: {uid}: {msg} - {ex.Message}", AppDomain.CurrentDomain.BaseDirectory, "notificationerror.txt");
            }
        }
    }
}

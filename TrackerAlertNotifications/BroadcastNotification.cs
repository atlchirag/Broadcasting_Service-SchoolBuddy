using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using TrackerAlertNotifications;

namespace BroadcastService
{
    public class BroadcastNotification
    {
        public static void Start()
        {
            while (true)
            {
                try
                {
                    List<(int msgId, int userId, int routeId, string message)> pendingMessages = GetPendingMessages();
                    Firebase firebase = new Firebase();

                    foreach (var msg in pendingMessages)
                    {
                        List<string> tokens = msg.routeId > 0
                            ? GetTokensByRoute(msg.routeId)
                            : GetTokensByUser(msg.userId);

                        foreach (string token in tokens)
                        {
                            Task.Run(() => firebase.SendNotification(token, msg.message, msg.userId.ToString()));
                        }

                        LogBroadcast(msg.msgId, msg.userId, msg.routeId, msg.message);
                        MarkMessageDone(msg.msgId);
                    }

                    leaverequest.GetLeaveMessages();

                }
                catch (Exception ex)
                {
                    General.WriteToLogFile($"Broadcast Error: {ex.Message}", AppDomain.CurrentDomain.BaseDirectory, "broadcastError.txt");
                }

                Thread.Sleep(300000); // 5 minutes
            }
        }

        private static List<(int msgId, int userId, int routeId, string message)> GetPendingMessages()
        {
            List<(int, int, int, string)> messages = new List<(int, int, int, string)>();
            DataTable dt = General.SelectQuery("SELECT id, sys_user_id, route_id, message FROM bs_broadcast_msg WHERE is_pending = 1");
            //Console.WriteLine(dt);
            foreach (DataRow row in dt.Rows)
            {
                messages.Add((Convert.ToInt32(row["id"]), Convert.ToInt32(row["sys_user_id"]), Convert.ToInt32(row["route_id"]), row["message"].ToString()));
            }

            return messages;
        }

        private static List<string> GetTokensByUser(int userId)
        {
            List<string> tokens = new List<string>();
            DataTable dt = General.SelectQuery($"SELECT auid, iuid FROM bs_user_master WHERE sys_user_id = {userId}");

            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["auid"].ToString()))
                    tokens.Add(row["auid"].ToString());
                if (!string.IsNullOrEmpty(row["iuid"].ToString()))
                    tokens.Add(row["iuid"].ToString());
            }

            return tokens;
        }

        private static List<string> GetTokensByRoute(int routeId)
        {
            List<string> tokens = new List<string>();
            
            string query = $@"select um.auid,um.iuid from bs_student_master_backup as smb join bs_route_students as rs on smb.Id = rs.student_id join bs_user_master as um on smb.mobile_no1 = um.bs_user_name where rs.route_id = {routeId}";

            //Console.WriteLine(query);
            DataTable dt = General.SelectQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["auid"].ToString()))
                    tokens.Add(row["auid"].ToString());
                if (!string.IsNullOrEmpty(row["iuid"].ToString()))
                    tokens.Add(row["iuid"].ToString());
            }
                    
            return tokens;
        }

        private static void MarkMessageDone(int msgId)
        {
            General.DML($"UPDATE bs_broadcast_msg SET is_pending = 0 WHERE id = {msgId}");
        }

        private static void LogBroadcast(int msgId, int userId, int routeId, string message)
        {
            string query = $@"INSERT bs_broadcast_msg_log  
                              (message_id, sys_user_id, route_id, message, alert_sent_on) 
                              VALUES ({msgId}, {userId}, {routeId}, '{message.Replace("'", "''")}', GETDATE())";
            General.DML(query);
        }
    }
}

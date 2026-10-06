using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TrackerAlertNotifications
{
    public class leaverequest
    {
        public static void GetLeaveMessages()
        {
            Firebase firebase = new Firebase();
            List<(int,int,int,string)> messages = new List<(int,int,int,string)>();
            DataTable dt = General.SelectQuery(@"
    SELECT 
l.id,
        l.is_approved,
        l.applied_date,
        s.student_name,
        l.remark,
student_id,
bs_user_id
    FROM bs_leave_master l
    INNER JOIN bs_student_master_backup s 
        ON s.id = l.student_id
    WHERE l.ispending = 1 and is_approved IN (1, 2)
");

            foreach (DataRow row in dt.Rows)
            {
                int statusValue = Convert.ToInt32(row["is_approved"]);

                string status = "";

                if (statusValue == 0)
                {
                    status = "Pending";
                }
                else if (statusValue == 1)
                {
                    status = "Approved";
                }
                else if (statusValue == 2)
                {
                    status = "Rejected";
                }
                else
                {
                    status = "Unknown";
                }

                string studentName = row["student_name"]?.ToString() ?? "";
                string id = row["id"]?.ToString() ?? "";

                string student_id = row["student_id"]?.ToString() ?? "";
                string parent_id = row["bs_user_id"]?.ToString() ?? "";

                string leaveDate = row["applied_date"] == DBNull.Value
                    ? ""
                    : Convert.ToDateTime(row["applied_date"]).ToString("dd MMM yyyy");

                string remark = row["remark"] == DBNull.Value
                    ? ""
                    : row["remark"].ToString();

                string msg =
                    $"SchoolBuddy: Leave request for {studentName} dated {leaveDate} " +
                    $"has been {status}.";

                if (!string.IsNullOrWhiteSpace(remark))
                {
                    msg += $" Remark: {remark}";
                }


            messages.Add((Convert.ToInt32(row["student_id"]), Convert.ToInt32(row["id"]), Convert.ToInt32(row["bs_user_id"]), msg));

                try { 

                General.DML(
                    "INSERT INTO bs_notification_parent (parent_id, type, message, date_time, student_id) " +
                    $"VALUES ({parent_id}, 'LEAVE', '{msg.Replace("'", "''")}', GETDATE(), {student_id})");

                DataTable dtfcm = General.SelectQuery($@"
SELECT DISTINCT CAST(pl.fcm AS VARCHAR(MAX)) AS fcm
FROM bs_parent_login_log pl
WHERE pl.bs_user_id = {parent_id}
  AND pl.fcm IS NOT NULL
  AND LTRIM(RTRIM(CAST(pl.fcm AS VARCHAR(MAX)))) <> '';"
                    );

                if (dtfcm == null || dtfcm.Rows.Count == 0)
                {
                        General.DML($"update bs_leave_master set ispending=0 where id={id}");
                        continue;
                }

                List<string> tokens = dtfcm.AsEnumerable()
                    .Select(r => r["fcm"]?.ToString()?.Trim() ?? "")
                    .ToList();

                    foreach (string token in tokens)
                    {
                        Task.Run(() => firebase.SendLeaveNotification(token, msg,student_id.ToString()));
                    }

                    General.DML($"update bs_leave_master set ispending=0 where id={id}");

                }
            catch (Exception ex)
            {
                    General.WriteToLogFile($"Broadcast Leave Error: {ex.Message}", AppDomain.CurrentDomain.BaseDirectory, "broadcastLeaveError.txt");

                }
            }

           
        }

        
    }
}

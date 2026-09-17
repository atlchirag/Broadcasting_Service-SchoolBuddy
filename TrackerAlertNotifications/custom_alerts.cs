using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Device.Location;

namespace TrackerAlertNotifications
{
    class custom_alerts
    {
        //public void sendMessage(int sys_user_id, string message, int id)
        //{
        //    DataTable mobile_table = General.SelectQuery("select mobile from alert_mobile where alert_setting_id=" + id);
        //    if (mobile_table != null)
        //    {
        //        if (mobile_table.Rows.Count > 0)
        //        {
        //            foreach (DataRow mobile_row in mobile_table.Rows)
        //            {
        //                Thread t = new Thread(new ThreadStart(() => General.SendSms(message, mobile_row["mobile"].ToString(), sys_user_id, id)));
        //                t.Start();
        //                Thread.Sleep(100);
        //            }
        //        }
        //    }

        //}
        //public async Task sendEmail(string message, int id)
        //{
        //    DataTable email_table = General.SelectQuery("select email from alertbyemail where alert_setting_id=" + id);
        //    if (email_table != null)
        //    {
        //        if (email_table.Rows.Count > 0)
        //        {
        //            string email = email_table.Rows[0]["email"].ToString();
        //            List<string> cc_mails = new List<string>();
        //            for (int i = 1; i < email_table.Rows.Count; i++)
        //            {
        //                cc_mails.Add(email_table.Rows[i]["email"].ToString());
        //            }
        //            await General.SendEmail("Alert Mail", message, email, cc_mails, null, id);
        //            await General.SendEmail("Alert Mail", message, email, cc_mails, null, id);
        //            //Thread t = new Thread(new ThreadStart(() => General.SendEmail("Alert Mail", message, email, cc_mails, null, id)));
        //            //t.Start();
        //            //Thread.Sleep(100);
        //        }
        //    }

        //}
        //public double GetDistance(string slat, string slng, string elat, string elon)
        //{
        //    try
        //    {
        //        var sCoord = new GeoCoordinate(Convert.ToDouble(slat), Convert.ToDouble(slng));
        //        var eCoord = new GeoCoordinate(Convert.ToDouble(elat), Convert.ToDouble(elon));
        //        var dis = Math.Round(sCoord.GetDistanceTo(eCoord), 2);

        //        return dis;
        //    }
        //    catch { return 0.0; }
        //}
        //public async Task checkLimitStatus(DataRow dr)
        //{

        //    int notification_id = Convert.ToInt32(dr["notification_id"]);
        //    if (notification_id == 7)
        //    {
        //        await checkSpeedStatus(dr);
        //    }
        //    else if (notification_id == 9)
        //    {

        //        await checkGeofenceStatus(dr);

        //    }
        //    else if (notification_id == 8)
        //    {

        //        await checkIdleStatus(dr);

        //    }

        //    else if (notification_id == 13)
        //    {

        //       await checkToeStatus(dr);

        //    }
        //    else if (notification_id == 14)
        //    {

        //        await checkBatteryStatus(dr);

        //    }


        //}

        //public async Task checkSpeedStatus(DataRow dr)
        //{
        //    int notification_id = Convert.ToInt32(dr["notification_id"]);
        //    int id = Convert.ToInt32(dr["id"]);
        //    int user_id = Convert.ToInt32(dr["sys_user_id"]);
        //    int sys_service_id = Convert.ToInt32(dr["sys_service_id"]);
        //    byte last_value = Convert.ToByte(dr["last_value"]);
        //    string input_value = dr["input"].ToString();
        //    string input_message = dr["message"].ToString();
        //    string veh_reg = dr["veh_reg"].ToString();
        //    int offset = Convert.ToInt32(dr["utc_offset"]);
        //    DataTable status_table = General.SelectQuery("select " + input_value + " , gps_time,gps_latitude,gps_longitude from latest_telemetry where sys_service_id=" + sys_service_id);

        //    if (status_table != null)
        //    {
        //        if (status_table.Rows.Count > 0)
        //        {

        //            double gps_speed = Convert.ToInt32(status_table.Rows[0][input_value]);

        //            DataTable parameter_table = General.SelectQuery("select limit from alert_parameter where alert_setting_id=" + id);
        //            if (parameter_table != null)
        //            {
        //                if (parameter_table.Rows.Count > 0)
        //                {
        //                    double limit = Convert.ToDouble(parameter_table.Rows[0]["limit"]);
        //                    byte recent_value = 0;
        //                    if (gps_speed > limit)
        //                    {

        //                        recent_value = 1;

        //                    }

        //                    if (last_value != recent_value)
        //                    {
        //                        if (recent_value == 1)
        //                        {
        //                            double latitude = Convert.ToDouble(status_table.Rows[0]["gps_latitude"]);
        //                            double longitude = Convert.ToDouble(status_table.Rows[0]["gps_longitude"]);
        //                            string location = await General.GetLocationFromLatLong(latitude, longitude);
        //                            if (location.Contains("fasttrack"))
        //                            {
        //                                location = "";
        //                            }
        //                            string main_msg = string.Format(input_message, veh_reg, limit, location, Convert.ToDateTime(status_table.Rows[0]["gps_time"]).AddMinutes(offset).ToString("dd/MM/yyyy HH:mm"));



        //                            string message = "Dear Customer,your vehicle " + veh_reg + " exceeded the speed limit of " + limit + " km/hr";






        //                            if (Convert.ToByte(dr["is_notification"]) == 1)
        //                            {
        //                                await General.SendNotification(user_id, message, id, latitude, longitude, main_msg);
        //                                //Thread t = new Thread(new ThreadStart(() => General.SendNotification(user_id, message, id, latitude, longitude, main_msg)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);
        //                            }
        //                            if (Convert.ToByte(dr["is_email"]) == 1)
        //                            {
        //                                await sendEmail(message, id);
        //                                //Thread t = new Thread(new ThreadStart(() => sendEmail(message, id)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);

        //                            }
        //                            if (Convert.ToByte(dr["is_sms"]) == 1)
        //                            {
        //                                //Thread t = new Thread(new ThreadStart(() => sendMessage(user_id, message, id)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);

        //                            }
        //                            await General.DML("insert into alert_log(alert_setting_id,message,sent_on,gps_latitude,gps_longitude) values (" + id + ",'" + message + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + latitude + "," + longitude + ")");
        //                        }
        //                        await General.DML("update alert_setting set last_value=" + recent_value + " ,last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id);
        //                        //Thread t1 = new Thread(new ThreadStart(() => General.DML("update alert_setting set last_value=" + recent_value + " ,last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id)));
        //                        //t1.Start();
        //                        //Thread.Sleep(50);
        //                        //  General.DML("insert into app_notification_log values(" + user_id + "," + sys_service_id + "," + notification_id + ",'" + message + "',getdate())");

        //                    }

        //                }

        //            }
        //        }

        //    }

        //}

        //public async Task  checkGeofenceStatus(DataRow dr)
        //{
        //    int notification_id = Convert.ToInt32(dr["notification_id"]);
        //    int id = Convert.ToInt32(dr["id"]);
        //    int user_id = Convert.ToInt32(dr["sys_user_id"]);
        //    int sys_service_id = Convert.ToInt32(dr["sys_service_id"]);
        //    byte last_value = Convert.ToByte(dr["last_value"]);
        //    string input_value = dr["input"].ToString();
        //    string input_message = dr["message"].ToString();
        //    string veh_reg = dr["veh_reg"].ToString();
        //    int offset = Convert.ToInt32(dr["utc_offset"]);
        //    DataTable status_table = General.SelectQuery("select " + input_value + " , gps_time from latest_telemetry where sys_service_id=" + sys_service_id);

        //    if (status_table != null)
        //    {
        //        if (status_table.Rows.Count > 0)
        //        {

        //            string gps_latitude = status_table.Rows[0]["gps_latitude"].ToString();
        //            string gps_longitude = status_table.Rows[0]["gps_longitude"].ToString();
        //            DataTable parameter_table = General.SelectQuery("select limit,parameter1,parameter2 from alert_parameter where alert_setting_id=" + id);
        //            if (parameter_table != null)
        //            {
        //                if (parameter_table.Rows.Count > 0)
        //                {
        //                    double radius = Convert.ToDouble(parameter_table.Rows[0]["limit"]) * 1000;
        //                    string latitude = parameter_table.Rows[0]["parameter1"].ToString();
        //                    string longitude = parameter_table.Rows[0]["parameter2"].ToString();
        //                    double distance = GetDistance(gps_latitude, gps_longitude, latitude, longitude);
        //                    byte recent_value = 0;
        //                    if (distance > radius)
        //                    {

        //                        recent_value = 1;

        //                    }

        //                    if (last_value != recent_value)
        //                    {

        //                        if (recent_value == 1)
        //                        {
        //                            double latitude1 = Convert.ToDouble(status_table.Rows[0]["gps_latitude"]);
        //                            double longitude1 = Convert.ToDouble(status_table.Rows[0]["gps_longitude"]);
        //                            string location = await General.GetLocationFromLatLong(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
        //                            if (location.Contains("fasttrack"))
        //                            {
        //                                location = "";
        //                            }
        //                            string message = string.Format(input_message, veh_reg, radius, Convert.ToDateTime(status_table.Rows[0]["gps_time"]).AddMinutes(offset).ToString("dd/MM/yyyy HH:mm"), location);
        //                            if (Convert.ToByte(dr["is_notification"]) == 1)
        //                            {
        //                                await General.SendNotification(user_id, message, id, latitude1, longitude1, message);
        //                                //Thread t = new Thread(new ThreadStart(() => General.SendNotification(user_id, message, id, latitude1, longitude1, message)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);
        //                            }
        //                            if (Convert.ToByte(dr["is_email"]) == 1)
        //                            {
        //                                await sendEmail(message, id);
        //                                //Thread t = new Thread(new ThreadStart(() => sendEmail(message, id)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);

        //                            }
        //                            if (Convert.ToByte(dr["is_sms"]) == 1)
        //                            {
        //                                //Thread t = new Thread(new ThreadStart(() => sendMessage(user_id, message, id)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);

        //                            }
        //                            await General.DML("insert into alert_log(alert_setting_id,message,sent_on,gps_latitude,gps_longitude) values (" + id + ",'" + message + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + latitude1 + "," + longitude1 + ")");
        //                        }
        //                        await General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id);
        //                        //Thread t1 = new Thread(new ThreadStart(() => General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id)));
        //                        //t1.Start();
        //                        //Thread.Sleep(50);


        //                    }

        //                }

        //            }
        //        }

        //    }



        //}

        //public async Task  checkIdleStatus(DataRow dr)
        //{
        //    int notification_id = Convert.ToInt32(dr["notification_id"]);
        //    int id = Convert.ToInt32(dr["id"]);
        //    int user_id = Convert.ToInt32(dr["sys_user_id"]);
        //    int sys_service_id = Convert.ToInt32(dr["sys_service_id"]);
        //    byte last_value = Convert.ToByte(dr["last_value"]);
        //    string input_value = dr["input"].ToString();
        //    string input_message = dr["message"].ToString();
        //    string veh_reg = dr["veh_reg"].ToString();
        //    int offset = Convert.ToInt32(dr["utc_offset"]);
        //    DataTable status_table = General.SelectQuery("select " + input_value + " , gps_time,gps_latitude,gps_longitude from latest_telemetry where sys_service_id=" + sys_service_id);

        //    if (status_table != null)
        //    {
        //        if (status_table.Rows.Count > 0)
        //        {

        //            double idle_since = Convert.ToInt32(status_table.Rows[0][input_value]);

        //            DataTable parameter_table = General.SelectQuery("select limit from alert_parameter where alert_setting_id=" + id);
        //            if (parameter_table != null)
        //            {
        //                if (parameter_table.Rows.Count > 0)
        //                {
        //                    double limit = Convert.ToDouble(parameter_table.Rows[0]["limit"]);
        //                    byte recent_value = 0;
        //                    if (idle_since > limit)
        //                    {

        //                        recent_value = 1;

        //                    }

        //                    if (last_value != recent_value)
        //                    {
        //                        if (recent_value == 1)
        //                        {
        //                            double latitude = Convert.ToDouble(status_table.Rows[0]["gps_latitude"]);
        //                            double longitude = Convert.ToDouble(status_table.Rows[0]["gps_longitude"]);
        //                            string location = await General.GetLocationFromLatLong(latitude, longitude);
        //                            if (location.Contains("fasttrack"))
        //                            {
        //                                location = "";
        //                            }
        //                            //string main_message = string.Format(input_message, veh_reg, sense_message, location, Convert.ToDateTime(status_table.Rows[0]["gps_time"]).AddMinutes(offset).ToString("dd/MM/yyyy HH:mm"));

        //                            string main_message = string.Format(input_message, veh_reg, limit, location, Convert.ToDateTime(status_table.Rows[0]["gps_time"]).AddMinutes(offset).ToString("dd/MM/yyyy HH:mm"));
        //                            string message = "Dear Customer,your vehicle " + veh_reg + " is idle for " + limit + " minutes";

        //                            if (Convert.ToByte(dr["is_notification"]) == 1)
        //                            {
        //                                await General.SendNotification(user_id, message, id, latitude, longitude, main_message);
        //                                //Thread t = new Thread(new ThreadStart(() => General.SendNotification(user_id, message, id, latitude, longitude, main_message)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);
        //                            }
        //                            if (Convert.ToByte(dr["is_email"]) == 1)
        //                            {
        //                                await sendEmail(message, id);
        //                                //Thread t = new Thread(new ThreadStart(() => sendEmail(message, id)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);

        //                            }
        //                            if (Convert.ToByte(dr["is_sms"]) == 1)
        //                            {
        //                                //Thread t = new Thread(new ThreadStart(() => sendMessage(user_id, message, id)));
        //                                //t.Start();
        //                                //Thread.Sleep(100);

        //                            }
        //                            await General.DML("insert into alert_log(alert_setting_id,message,sent_on,gps_latitude,gps_longitude) values (" + id + ",'" + message + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + latitude + "," + longitude + ")");
        //                        }
        //                        await General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id);
        //                        //Thread t1 = new Thread(new ThreadStart(() => General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id)));
        //                        //t1.Start();
        //                        //Thread.Sleep(50);
        //                        // General.DML("insert into app_notification_log values(" + user_id + "," + sys_service_id + "," + notification_id + ",'" + message + "',getdate())");

        //                    }

        //                }

        //            }
        //        }

        //    }

        //}


        //public async Task checkToeStatus(DataRow dr)
        //{
        //    int notification_id = Convert.ToInt32(dr["notification_id"]);
        //    int id = Convert.ToInt32(dr["id"]);
        //    int user_id = Convert.ToInt32(dr["sys_user_id"]);
        //    int sys_service_id = Convert.ToInt32(dr["sys_service_id"]);
        //    byte last_value = Convert.ToByte(dr["last_value"]);
        //    string input_value = dr["input"].ToString();
        //    string input_message = dr["message"].ToString();
        //    string veh_reg = dr["veh_reg"].ToString();
        //    int offset = Convert.ToInt32(dr["utc_offset"]);
        //    DataTable status_table = General.SelectQuery("select " + input_value + " , gps_time,gps_latitude,gps_longitude from latest_telemetry where sys_service_id=" + sys_service_id);

        //    if (status_table != null)
        //    {
        //        if (status_table.Rows.Count > 0)
        //        {
        //            byte i2 = Convert.ToByte(status_table.Rows[0]["i2"]);

        //            double gps_speed = Convert.ToDouble(status_table.Rows[0]["gps_speed"]);
        //            byte recent_value = 0;
        //            if (recent_value != last_value)
        //            {
        //                if (gps_speed > 2 && i2 == 0)
        //                {
        //                    recent_value = 1;
        //                    double latitude = Convert.ToDouble(status_table.Rows[0]["gps_latitude"]);
        //                    double longitude = Convert.ToDouble(status_table.Rows[0]["gps_longitude"]);
        //                    string location = await General.GetLocationFromLatLong(latitude, longitude);
        //                    if (location.Contains("fasttrack"))
        //                    {
        //                        location = "";
        //                    }
        //                    string message = string.Format(input_message, veh_reg, Convert.ToDateTime(status_table.Rows[0]["gps_time"]).AddMinutes(offset).ToString("dd/MM/yyyy HH:mm"));
        //                    if (Convert.ToByte(dr["is_notification"]) == 1)
        //                    {
        //                        await General.SendNotification(user_id, message, id, latitude, longitude, message);
        //                        //Thread t = new Thread(new ThreadStart(() => General.SendNotification(user_id, message, id, latitude, longitude, message)));
        //                        //t.Start();
        //                        //Thread.Sleep(100);
        //                    }
        //                    if (Convert.ToByte(dr["is_email"]) == 1)
        //                    {
        //                        await sendEmail(message, id);
        //                        //Thread t = new Thread(new ThreadStart(() => sendEmail(message, id)));
        //                        //t.Start();
        //                        //Thread.Sleep(100);

        //                    }
        //                    if (Convert.ToByte(dr["is_sms"]) == 1)
        //                    {
        //                        //Thread t = new Thread(new ThreadStart(() => sendMessage(user_id, message, id)));
        //                        //t.Start();
        //                        //Thread.Sleep(100);

        //                    }
        //                    await General.DML("insert into alert_log(alert_setting_id,message,sent_on,gps_latitude,gps_longitude) values (" + id + ",'" + message + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + latitude + "," + longitude + ")");
        //                }
        //                await General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id);
        //                //Thread t1 = new Thread(new ThreadStart(() => General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id)));
        //                //t1.Start();
        //                //Thread.Sleep(50);
        //            }

        //        }

        //    }

        //}

        //public async Task checkBatteryStatus(DataRow dr)
        //{
        //    int notification_id = Convert.ToInt32(dr["notification_id"]);
        //    int id = Convert.ToInt32(dr["id"]);
        //    int user_id = Convert.ToInt32(dr["sys_user_id"]);
        //    int sys_service_id = Convert.ToInt32(dr["sys_service_id"]);
        //    byte last_value = Convert.ToByte(dr["last_value"]);
        //    string input_value = dr["input"].ToString();
        //    string input_message = dr["message"].ToString();
        //    string veh_reg = dr["veh_reg"].ToString();
        //    int offset = Convert.ToInt32(dr["utc_offset"]);
        //    DataTable status_table = General.SelectQuery("select " + input_value + " , gps_time,gps_latitude,gps_longitude from latest_telemetry where sys_service_id=" + sys_service_id);

        //    if (status_table != null)
        //    {
        //        if (status_table.Rows.Count > 0)
        //        {
        //            byte i1 = Convert.ToByte(status_table.Rows[0]["i1"]);

               
        //            byte recent_value = 0;
        //            if (recent_value != last_value)
        //            {
        //                if ( i1 == 0)
        //                {
        //                    recent_value = 1;
        //                    double latitude = Convert.ToDouble(status_table.Rows[0]["gps_latitude"]);
        //                    double longitude = Convert.ToDouble(status_table.Rows[0]["gps_longitude"]);
        //                    string location = await General.GetLocationFromLatLong(latitude, longitude);
        //                    if (location.Contains("fasttrack"))
        //                    {
        //                        location = "";
        //                    }
        //                    string message = string.Format(input_message, veh_reg,location, Convert.ToDateTime(status_table.Rows[0]["gps_time"]).AddMinutes(offset).ToString("dd/MM/yyyy HH:mm"));
        //                    if (Convert.ToByte(dr["is_notification"]) == 1)
        //                    {
        //                        await General.SendNotification(user_id, message, id, latitude, longitude, message);
        //                        //Thread t = new Thread(new ThreadStart(() => General.SendNotification(user_id, message, id, latitude, longitude, message)));
        //                        //t.Start();
        //                        //Thread.Sleep(100);
        //                    }
        //                    if (Convert.ToByte(dr["is_email"]) == 1)
        //                    {
        //                        await sendEmail(message, id);
        //                        //Thread t = new Thread(new ThreadStart(() => sendEmail(message, id)));
        //                        //t.Start();
        //                        //Thread.Sleep(100);

        //                    }
        //                    if (Convert.ToByte(dr["is_sms"]) == 1)
        //                    {
        //                        //Thread t = new Thread(new ThreadStart(() => sendMessage(user_id, message, id)));
        //                        //t.Start();
        //                        //Thread.Sleep(100);

        //                    }
        //                    await General.DML("insert into alert_log(alert_setting_id,message,sent_on,gps_latitude,gps_longitude) values (" + id + ",'" + message + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + latitude + "," + longitude + ")");
        //                }
        //                await General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id);
        //                //Thread t1 = new Thread(new ThreadStart(() => General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id)));
        //                //t1.Start();
        //                //Thread.Sleep(50);
        //            }

        //        }

        //    }


        //}


    }
}

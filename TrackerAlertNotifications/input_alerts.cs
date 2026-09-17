//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace TrackerAlertNotifications
//{
//    class input_alerts
//    {

//        public async Task checkInputStatus(DataRow dr)
//        {
//            try
//            {
//                int id = Convert.ToInt32(dr["id"]);
//                int user_id = Convert.ToInt32(dr["sys_user_id"]);
//                int sys_service_id = Convert.ToInt32(dr["sys_service_id"]);
//                byte last_value = Convert.ToByte(dr["last_value"]);
//                byte high_sense = Convert.ToByte(dr["high_sense"]);
//                string high_message = dr["high_message"].ToString();
//                string low_message = dr["low_message"].ToString();
//                string input_value = dr["input"].ToString();
//                int offset = Convert.ToInt32(dr["utc_offset"]);
//                string input_message = dr["message"].ToString();
//                string veh_reg = dr["veh_reg"].ToString();
//                int notification_id = Convert.ToInt32(dr["notification_id"]);
//                DataTable status_table = General.SelectQuery("select " + input_value + " , gps_time,gps_latitude,gps_longitude from latest_telemetry where sys_service_id=" + sys_service_id);
//                if (status_table != null)
//                {
//                    if (status_table.Rows.Count > 0)
//                    {
//                        byte recent_value = Convert.ToByte(status_table.Rows[0][input_value]);
//                        if (recent_value != last_value)
//                        {
//                            string message = "";
//                            DataTable parameter_table = General.SelectQuery("select input_on,input_off from alert_parameter where alert_setting_id=" + id);
//                            if (parameter_table != null)
//                            {
//                                if (parameter_table.Rows.Count > 0)
//                                {
//                                    byte input_on = Convert.ToByte(parameter_table.Rows[0]["input_on"]);
//                                    byte input_off = Convert.ToByte(parameter_table.Rows[0]["input_off"]);
//                                    if ((recent_value == 1 && input_on == 1) || (recent_value == 0 && input_off == 1))
//                                    {

//                                        string sense_message = "";
//                                        if (high_sense == 1 && recent_value == 1)
//                                        {
//                                            sense_message = high_message;

//                                        }
//                                        else if (high_sense == 1 && recent_value == 0)
//                                        {

//                                            sense_message = low_message;
//                                        }
//                                        else if (high_sense == 0 && recent_value == 1)
//                                        {
//                                            sense_message = low_message;

//                                        }
//                                        else
//                                        {
//                                            sense_message = high_message;
//                                        }
//                                        double latitude = Convert.ToDouble(status_table.Rows[0]["gps_latitude"]);
//                                        double longitude = Convert.ToDouble(status_table.Rows[0]["gps_longitude"]);
//                                        string location =  await General.GetLocationFromLatLong(latitude, longitude);
//                                        if (location.Contains("fasttrack"))
//                                        {
//                                            location = "";
//                                        }
//                                        message = string.Format(input_message, veh_reg, sense_message, location, Convert.ToDateTime(status_table.Rows[0]["gps_time"]).AddMinutes(offset).ToString("dd/MM/yyyy HH:mm"));

//                                        string main_messsage = message;

//                                        string alert_status = string.Empty;

//                                        if (message.Contains("Main Power is OFF"))
//                                        {
//                                            alert_status = "MainPower Off";
//                                            message = "Dear Customer, your vehicle " + veh_reg + " Main Power is Off";


//                                        }
//                                        else if (message.Contains("Main Power is ON"))
//                                        {
//                                            alert_status = "MainPower On";
//                                            message = "Dear Customer, your vehicle " + veh_reg + " Main Power is ON";
//                                        }

//                                        else if (message.Contains("Ignition is ON"))
//                                        {
//                                            alert_status = "Ignition On";
//                                            message = "Dear Customer, your vehicle " + veh_reg + " Ignition is ON";
//                                        }
//                                        else if (message.Contains("Ignition is OFF"))
//                                        {
//                                            alert_status = "Ignition Off";
//                                            message = "Dear Customer, your vehicle " + veh_reg + " Ignition is OFF";
//                                        }









//                                        if (Convert.ToByte(dr["is_notification"]) == 1)
//                                        {
//                                            await General.SendNotification(user_id, message, id, latitude, longitude, main_messsage);
//                                            //Thread t = new Thread(new ThreadStart(() => General.SendNotification(user_id, message, id, latitude, longitude, main_messsage)));
//                                            //t.Start();
//                                            //Thread.Sleep(100);
//                                        }
//                                        if (Convert.ToByte(dr["is_email"]) == 1)
//                                        {
//                                            DataTable email_table = General.SelectQuery("select email from alertbyemail where alert_setting_id=" + id);
//                                            if (email_table != null)
//                                            {
//                                                if (email_table.Rows.Count > 0)
//                                                {
//                                                    string email = email_table.Rows[0]["email"].ToString();
//                                                    List<string> cc_mails = new List<string>();
//                                                    for (int i = 1; i < email_table.Rows.Count; i++)
//                                                    {
//                                                        cc_mails.Add(email_table.Rows[i]["email"].ToString());
//                                                    }
//                                                    await General.SendEmail("Alert Mail", message, email, cc_mails, null, id);
//                                                    //Thread t = new Thread(new ThreadStart(() => General.SendEmail("Alert Mail", message, email, cc_mails, null, id)));
//                                                    //t.Start();
//                                                    //Thread.Sleep(100);
//                                                }
//                                            }
//                                        }
//                                        if (Convert.ToByte(dr["is_sms"]) == 1)
//                                        {
//                                            //DataTable mobile_table = General.SelectQuery("select mobile from alert_mobile where alert_setting_id=" + id);
//                                            //if (mobile_table != null)
//                                            //{
//                                            //    if (mobile_table.Rows.Count > 0)
//                                            //    {
//                                            //        foreach (DataRow mobile_row in mobile_table.Rows)
//                                            //        {
//                                            //            Thread t = new Thread(new ThreadStart(() => General.SendSms(message, mobile_row["mobile"].ToString(), user_id, id)));
//                                            //            t.Start();
//                                            //            Thread.Sleep(100);
//                                            //        }
//                                            //    }
//                                            //}
//                                        }
//                                        await General.DML("insert into alert_log(alert_setting_id,message,sent_on,gps_latitude,gps_longitude) values (" + id + ",'" + message + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + latitude + "," + longitude + ")");
//                                    }
//                                }
//                            }
//                            await General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id);
//                            //Thread t1 = new Thread(new ThreadStart(() =>   await General.DML("update alert_setting set last_value=" + recent_value + ",last_updated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id=" + id)));
//                            //t1.Start();
//                            //Thread.Sleep(50);

//                            //Thread t2 = new Thread(new ThreadStart(() => General.DML("insert into app_notification_log values(" + user_id + "," + sys_service_id + "," + notification_id + ",'" + message + "',getdate()")));
//                            //t2.Start();


//                        }
//                    }


//                }
//            }
//            catch (Exception e)
//            {
//                await General.WriteToLogFile(e.Message, AppDomain.CurrentDomain.BaseDirectory, "statusExp.txt");
//            }
//        }
//    }
//}

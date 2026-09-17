//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;

//namespace TrackerAlertNotifications
//{
//    class watchman_alert
//    {


//        public async Task sendAlert(int id, int sys_service_id, string veh_no, int sys_user_id, bool is_parking = false)
//        {
//            try
//            {
//                DataTable input_table = General.SelectQuery("select i2,i8,i15,gps_speed from latest_telemetry where sys_service_id=" + sys_service_id);

//                if (input_table != null)
//                {

//                    if (input_table.Rows.Count > 0)
//                    {
//                        byte i2 = Convert.ToByte(input_table.Rows[0]["i2"]);
//                        byte i8 = Convert.ToByte(input_table.Rows[0]["i8"]);
//                        byte i15 = Convert.ToByte(input_table.Rows[0]["i15"]);
//                        double gps_speed = Convert.ToDouble(input_table.Rows[0]["gps_speed"]);

//                        if (gps_speed > 2 || i2 == 1 || i15 == 1 || i8 == 0)
//                        {
//                            string msg = "Dear User , your vehicle " + veh_no + " is in motion";
//                            if (!is_parking)
//                                await General.SendNotification(sys_user_id, msg, "watchman");
//                            else
//                                await General.SendNotification(sys_user_id, msg, "parking");
//                            if (!is_parking)
//                            {
//                                await General.DML("insert into tbl_watchman_alert_log(watchman_alert_id,message,sent_on) values(" + id + ",'" + msg + "',getdate())");
//                                await General.DML("update tbl_watchman_alert set sent_on=getdate() where id=" + id);
//                            }

//                            if (is_parking)
//                            {

//                                await General.DML("insert into tbl_parking_alert_log(watchman_alert_id,message,sent_on) values(" + id + ",'" + msg + "',getdate())");
//                                await General.DML("update tbl_parking_alert set sent_on=getdate() where id=" + id);

//                            }


//                        }


//                    }

//                }

//            }
//            catch (Exception e)
//            {
//                General.WriteToLogFile(e.Message, AppDomain.CurrentDomain.BaseDirectory, "watchman.txt");

//            }
//        }




//        public void senToeAlert(int id, int sys_service_id, string veh_no, int sys_user_id)
//        {
//            try
//            {
//                DataTable input_table = General.SelectQuery("select i2,gps_speed from latest_telemetry where sys_service_id=" + sys_service_id);

//                if (input_table != null)
//                {

//                    if (input_table.Rows.Count > 0)
//                    {
//                        byte i2 = Convert.ToByte(input_table.Rows[0]["i2"]);
                     
//                        double gps_speed = Convert.ToDouble(input_table.Rows[0]["gps_speed"]);

//                        if (gps_speed > 2 && i2 ==0 )
//                        {
//                            string msg = "Dear User , your vehicle " + veh_no + " is in motion";
                          
//                                General.SendNotification(sys_user_id, msg, "toe");
                           
                       
//                                General.DML("insert into tbl_toe_alert_log(toe_alert_id,message,sent_on) values(" + id + ",'" + msg + "',getdate())");
//                                General.DML("update tbl_toe_alert set sent_on=getdate() where id=" + id);

//                        }


//                    }

//                }

//            }
//            catch (Exception e)
//            {
//                General.WriteToLogFile(e.Message, AppDomain.CurrentDomain.BaseDirectory, "Toe.txt");

//            }
//        }


//    }
//}

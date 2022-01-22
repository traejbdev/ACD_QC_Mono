using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Mono.Data.Sqlite;

namespace LinuxQCDQC
{
    public class Setup
    {
        public bool Init_1()
        {
            #region Check TechTable
            if (!File.Exists("tech-table.sqlite"))
            {
                try
                {
                    SqliteConnection coi = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                    coi.Open();

                    string sql = "create table tech_records(id INTEGER PRIMARY KEY AUTOINCREMENT, profile_ref TEXT, profile_material TEXT, profile_thickness INTEGER, profile_type TEXT, profile_gases TEXT, profile_amps REAL, profile_volts REAL, profile_kerf REAL, profile_speed REAL, profile_ignition_height REAL, profile_pierce_height REAL, profile_cut_height REAL, profile_pierce_time REAL, profile_current_end REAL, profile_current_start REAL, profile_current_value REAL, profile_nozzle REAL, profile_shield REAL, profile_electrode REAL, last_updated TEXT) ";

                    SqliteCommand cmdi = new SqliteCommand(sql, coi);
                    cmdi.ExecuteNonQuery();

                    string sql2 = "INSERT INTO tech_records(profile_ref, profile_material, profile_thickness, profile_type, profile_gases, profile_amps, profile_volts, profile_kerf, profile_speed, profile_ignition_height, profile_pierce_height, profile_cut_height, profile_pierce_time, profile_current_end, profile_current_start, profile_current_value, profile_nozzle, profile_shield, profile_electrode, last_updated) VALUES ('3888', 'Mild Steel', '10', 'Production', 'GS2', '140', '140', '2', '3078', '4', '4', '4', '100', '100', '200', '3123', '123123', '12312', '234234', '23423423')";
                    SqliteCommand cmd2 = new SqliteCommand(sql2, coi);
                    cmd2.ExecuteNonQuery();
                    string sql3 = "INSERT INTO tech_records(profile_ref, profile_material, profile_thickness, profile_type, profile_gases, profile_amps, profile_volts, profile_kerf, profile_speed, profile_ignition_height, profile_pierce_height, profile_cut_height, profile_pierce_time, profile_current_end, profile_current_start, profile_current_value, profile_nozzle, profile_shield, profile_electrode, last_updated) VALUES ('3828', 'Stainless Steel', '10', 'Production', 'GS2', '140', '140', '2', '3078', '4', '4', '4', '100', '100', '200', '3123', '123123', '12312', '234234', '23423423')";
                    SqliteCommand cmd3 = new SqliteCommand(sql3, coi);
                    cmd3.ExecuteNonQuery();
                    string sql4 = "INSERT INTO tech_records(profile_ref, profile_material, profile_thickness, profile_type, profile_gases, profile_amps, profile_volts, profile_kerf, profile_speed, profile_ignition_height, profile_pierce_height, profile_cut_height, profile_pierce_time, profile_current_end, profile_current_start, profile_current_value, profile_nozzle, profile_shield, profile_electrode, last_updated) VALUES ('3818', 'Stainless Steel', '6', 'Production', 'GS2', '140', '140', '2', '3078', '4', '4', '4', '100', '100', '200', '3123', '123123', '12312', '234234', '23423423')";
                    SqliteCommand cmd4 = new SqliteCommand(sql4, coi);
                    cmd4.ExecuteNonQuery();
                    string sql5 = "INSERT INTO tech_records(profile_ref, profile_material, profile_thickness, profile_type, profile_gases, profile_amps, profile_volts, profile_kerf, profile_speed, profile_ignition_height, profile_pierce_height, profile_cut_height, profile_pierce_time, profile_current_end, profile_current_start, profile_current_value, profile_nozzle, profile_shield, profile_electrode, last_updated) VALUES ('3858', 'Mild Steel', '3', 'Production', 'GS2', '140', '140', '2', '3078', '4', '4', '4', '100', '100', '200', '3123', '123123', '12312', '234234', '23423423')";
                    SqliteCommand cmd5 = new SqliteCommand(sql5, coi);
                    cmd5.ExecuteNonQuery();
                    coi.Close();
                    return true;


                }
                catch (SqliteException ea){
                    MessageBox.Show(ea.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
            #endregion
        }
        public bool Init_2()
        {
            #region Check Profile Photo Reference Table

            if (!File.Exists("record_pic_ref.sqlite"))
            {
                try
                {
                    //Create Database File
                    //Table Creation
                    SqliteConnection coi = new SqliteConnection("Data Source=profile_pic_ref.sqlite;Version=3");
                    coi.Open();

                    string sql = "create table profile_photo_db (id INTEGER PRIMARY KEY AUTOINCREMENT, photo_ref INTEGER, photo_src TEXT)";

                    SqliteCommand cmd = new SqliteCommand(sql, coi);
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (SqliteException ea)
                {
                    MessageBox.Show(ea.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
            #endregion
        }
        public bool Check_Tech_Records()
        {
            if (!File.Exists("tech-table.sqlite"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

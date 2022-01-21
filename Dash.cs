using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mono.Data.Sqlite;
using Microsoft.Office.Interop;

namespace LinuxQCDQC
{
    public partial class Dash : Form
    {
        public Dash()
        {
            InitializeComponent();
        }
        public string set_material;
        public int set_thickness;
        public SqliteConnection modify_conn;
        public DataSet modify_dt;
        public BindingSource bind_source;
        public SqliteDataAdapter modify_sada;

        private void Dash_Load(object sender, EventArgs e)
        {
            #region Pre-populating calls
            AutoFillDatagrid();
            #endregion

        }

        private void search_record_btn_Click(object sender, EventArgs e)
        {

        }

        private void create_record_btn_Click(object sender, EventArgs e)
        {
        }

        private void Create_Tech_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        public void GetThickness_List()
        {
            try
            {
                SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                conn.Open();
                thickness_combo.DisplayMember = "profile_thickness";
                string sql = "SELECT profile_thickness FROM tech_records WHERE profile_material = '" + set_material + "'";
               
                SqliteDataAdapter ada = new SqliteDataAdapter("SELECT profile_thickness FROM tech_records WHERE profile_material = '"+set_material+"'", conn);
                DataTable table = new DataTable();
                ada.Fill(table);
                thickness_combo.DataSource = table;
                conn.Close();



            }
            catch (SqliteException ea)
            {
                MessageBox.Show(ea.ToString());
                
            }
        }
        public void GetMaterial_List()
        {
            try
            {
                SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                conn.Open();
                material_combo.DisplayMember = "profile_material";

                SqliteDataAdapter ada = new SqliteDataAdapter("SELECT profile_material FROM tech_records DESC", conn);
                DataTable table = new DataTable();
                ada.Fill(table);
                material_combo.DataSource = table;
                conn.Close();


            }
            catch(SqliteException ea)
            {
                MessageBox.Show(ea.ToString());
            }
        }
        public void UpdateSearch(string Material, int Thickness)
        {
            try
            {
                //string sql = "SELECT id, profile_ref, profile_material, profile_thickness, profile_type, profile_gases, profile_amps, profile_volts, profile_kerf, profile_speed, profile_ignition_height, profile_pierce_height, profile_cut_height, profile_pierce_time, profile_current_end, profile_current_start, profile_current_value, profile_nozzle, profile_shield, profile_electrode, last_updated FROM tech_records WHERE profile_material = '" + Material + "' AND profile_thickness = '" + Thickness + "'";
                //SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3;");
                //SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sql, conn);
                //DataSet dt = new DataSet();
                //dataAdapter.Fill(dt);
                //Search_Results.DataSource = dt.Tables[0].DefaultView;
            }catch(Exception ea)
            {
                MessageBox.Show(ea.ToString());
            }
            
        }

        private void material_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_material = material_combo.Text;
            GetThickness_List();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Search_Results.Rows.Clear();
            if(material_combo.Text != "")
            {
                int thickness = Int32.Parse(thickness_combo.Text);
                if(thickness > 0)
                {
                    try
                    {
                        SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                        conn.Open();
                        SqliteCommand cmd = new SqliteCommand("SELECT * FROM tech_records WHERE profile_material='" + material_combo.Text + "' AND profile_thickness='" + thickness + "'", conn);
                        using (SqliteDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                Search_Results.Rows.Add(new object[] {
                                rd.GetValue(0),  // U can use column index
                                rd.GetValue(1),  // Or column name like this
                                rd.GetValue(2),
                                rd.GetValue(3),
                                rd.GetValue(4),
                                rd.GetValue(5),
                                rd.GetValue(6),
                                rd.GetValue(7),
                                rd.GetValue(8),
                                rd.GetValue(9),
                                rd.GetValue(10),
                                rd.GetValue(11),
                                rd.GetValue(12),
                                rd.GetValue(13),
                                rd.GetValue(14),
                                rd.GetValue(15),
                                rd.GetValue(16),
                                rd.GetValue(17),
                                rd.GetValue(18),
                                rd.GetValue(19),
                                rd.GetValue(20)
                                });
                            }
                        }
                        conn.Close();
                    }
                    catch(SqliteException ea)
                    {
                        MessageBox.Show(ea.ToString());
                    }
                }
            }
        }
        public void AutoFillDatagrid()
        {
            try
            {
                SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                conn.Open();
                SqliteCommand cmd = new SqliteCommand("SELECT * FROM tech_records", conn);
                using (SqliteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        Search_Results.Rows.Add(new object[] {
                                rd.GetValue(0),  // U can use column index
                                rd.GetValue(1),  // Or column name like this
                                rd.GetValue(2),
                                rd.GetValue(3),
                                rd.GetValue(4),
                                rd.GetValue(5),
                                rd.GetValue(6),
                                rd.GetValue(7),
                                rd.GetValue(8),
                                rd.GetValue(9),
                                rd.GetValue(10),
                                rd.GetValue(11),
                                rd.GetValue(12),
                                rd.GetValue(13),
                                rd.GetValue(14),
                                rd.GetValue(15),
                                rd.GetValue(16),
                                rd.GetValue(17),
                                rd.GetValue(18),
                                rd.GetValue(19),
                                rd.GetValue(20)
                                });
                    }
                }
                conn.Close();
            }
            catch (SqliteException ea)
            {
                MessageBox.Show(ea.ToString());
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void Modify_BTN_Click(object sender, EventArgs e)
        {
            if (profile_ref_txt.Text != "")
            {
                action_txt.Text = "Finding...";
                FillModifybyref(profile_ref_txt.Text);
            }
            else if(material_modify_combo.Text != "")
            {
                int thicko = Int32.Parse(thickness_modify_combo.Text);
                action_txt.Text = "Finding...";
                modifywithoutref(material_modify_combo.Text, thicko);
            }
        }
        public void modifywithoutref(string material, int thickness)
        {
            modify_datagrid.Rows.Clear();
            try
            {
                SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                conn.Open();
                string modify_withoutref = "SELECT * FROM tech_records WHERE profile_material='" + material + "' AND profile_thickness='" + thickness + "'";

                SqliteCommand cmd = new SqliteCommand("SELECT * FROM tech_records WHERE profile_material='" + material + "' AND profile_thickness='" + thickness + "'", conn);
                using (SqliteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
       
                        modify_datagrid.Rows.Add(new object[] {
                                rd.GetValue(0),  // U can use column index
                                rd.GetValue(1),  // Or column name like this
                                rd.GetValue(2),
                                rd.GetValue(3),
                                rd.GetValue(4),
                                rd.GetValue(5),
                                rd.GetValue(6),
                                rd.GetValue(7),
                                rd.GetValue(8),
                                rd.GetValue(9),
                                rd.GetValue(10),
                                rd.GetValue(11),
                                rd.GetValue(12),
                                rd.GetValue(13),
                                rd.GetValue(14),
                                rd.GetValue(15),
                                rd.GetValue(16),
                                rd.GetValue(17),
                                rd.GetValue(18),
                                rd.GetValue(19),
                                rd.GetValue(20)
                                });
                    }
                }

                conn.Close();
            }
            catch (SqliteException ea)
            {
                MessageBox.Show(ea.ToString());
            }
        }
        public void FillModifybyref(string profile_ref)
        {
            modify_datagrid.Rows.Clear();
            try
            {
                SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                conn.Open();

                SqliteCommand cmd = new SqliteCommand("SELECT * FROM tech_records WHERE profile_ref='" + profile_ref + "'", conn);
                using (SqliteDataReader rd = cmd.ExecuteReader())
                {
                    try
                    {
                        while (rd.Read())
                        {
                            modify_datagrid.Rows.Add(new object[] {
                                rd.GetValue(0),  // U can use column index
                                rd.GetValue(1),  // Or column name like this
                                rd.GetValue(2),
                                rd.GetValue(3),
                                rd.GetValue(4),
                                rd.GetValue(5),
                                rd.GetValue(6),
                                rd.GetValue(7),
                                rd.GetValue(8),
                                rd.GetValue(9),
                                rd.GetValue(10),
                                rd.GetValue(11),
                                rd.GetValue(12),
                                rd.GetValue(13),
                                rd.GetValue(14),
                                rd.GetValue(15),
                                rd.GetValue(16),
                                rd.GetValue(17),
                                rd.GetValue(18),
                                rd.GetValue(19),
                                rd.GetValue(20)
                                });
                        }
                    }catch(SqliteException ea)
                    {
                        action_txt.Text = ea.ToString();
                    }
                }
                conn.Close();
                action_txt.Text = "{0} Results Found";
            }
            catch (SqliteException ea)
            {
                
                MessageBox.Show(ea.ToString());
            }
        }
        public void UpdateDataset(string profile_ref, string material, int thickness)
        {
            try
            {
                SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                conn.Open();
                modify_sada = new SqliteDataAdapter("SELECT * FROM tech_records", conn);

                SqliteCommandBuilder cmdbuild = new SqliteCommandBuilder(modify_sada);
                modify_sada.Fill(modify_dt);


            }
            catch(SqliteException ea)
            {

            }
        }

        private void profile_ref_txt_TextChanged(object sender, EventArgs e)
        {
            if(profile_ref_txt.Text != "")
            {
                material_modify_combo.Enabled = false;
                thickness_modify_combo.Enabled = false;
            }
            else
            {
                material_modify_combo.Enabled = true;
                thickness_modify_combo.Enabled = true;
            }
        }
        #region Create Tech Record BTN
        private void add_tech_record_btn_Click(object sender, EventArgs e)
        {
            if(cprofile_thickness_combo.Text != "")
            {
                if(cprofile_material_combo.Text != "")
                {
                    int cthick = Int32.Parse(cprofile_thickness_combo.Text);
                    float camp = float.Parse(cprofile_amps_txt.Text);
                    float cvolt = float.Parse(cprofile_volts_txt.Text);
                    float ckerf = float.Parse(cprofile_kerf_txt.Text);
                    float cspeed = float.Parse(cprofile_speed_txt.Text);
                    float cigni = float.Parse(cprofile_ignition_txt.Text);
                    float cpiheight = float.Parse(cprofile_pierce_txt.Text);
                    float cpitime = float.Parse(cprofile_pierce_time_txt.Text);
                    float cctime = float.Parse(cprofile_cut_txt.Text);
                    float ccurend = float.Parse(cprofile_current_end_txt.Text);
                    float ccurstar = float.Parse(cprofile_current_start_txt.Text);
                    float ccur = float.Parse(cprofile_current_txt.Text);
                    float cnoz = float.Parse(cprofile_nozzle_txt.Text);
                    float cshie = float.Parse(cprofile_shield_txt.Text);
                    float celec = float.Parse(cprofile_electrode_txt.Text);

                    create_tech_rec(cprofile_ref_txt.Text, cprofile_material_combo.Text, cthick, cprofile_type_txt.Text, cprofile_gasses_txt.Text, camp, cvolt, ckerf, cspeed, cigni, cpiheight, cctime, cpitime, ccurend, ccurstar, ccur, cnoz, cshie, celec, DateTime.Now.ToString());
                }
                else
                {
                    MessageBox.Show("Please Enter Material");
                }
            }
            else
            {
                MessageBox.Show("Please enter thickness");
            }
        }
        #endregion

        #region Create Tech Record
        public void create_tech_rec(string profile_ref, string profile_material, int profile_thickness, string profile_type, string profile_gases, float profile_amps, float profile_volts, float profile_kerf, float profile_speed, float profile_ignition_height, float profile_pierce_height, float profile_cut_height, float profile_pierce_time, float profile_current_end, float profile_current_start, float profile_current_value, float profile_nozzle, float profile_shield, float profile_electrode, string last_updated)
        {
            try
            {
                SqliteConnection conn = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                conn.Open();
                var insert = conn.CreateCommand();
                insert.CommandText = "INSERT INTO tech_records (profile_ref, profile_material, profile_thickness, profile_type, profile_gases, profile_amps, profile_volts, profile_kerf, profile_speed, profile_ignition_height, profile_pierce_height, profile_cut_height, profile_pierce_time, profile_current_end, profile_current_start, profile_current_value, profile_nozzle, profile_shield, profile_electrode, last_updated) VALUES (@proref, @promat, @prothic, @protype, @progas, @proamps, @provolts, @prokerf, @prosped, @proignheight, @propiheight, @procutheight, @propitime, @procurend, @procurstart, @procurvalue, @pronoz, @proshield, @proelectro, @lastup) ";
                insert.Parameters.AddWithValue("@proref", profile_ref);
                insert.Parameters.AddWithValue("@promat", profile_material);
                insert.Parameters.AddWithValue("@prothic", profile_thickness);
                insert.Parameters.AddWithValue("@protype", profile_type);
                insert.Parameters.AddWithValue("@progas", profile_gases);
                insert.Parameters.AddWithValue("@proamps", profile_amps);
                insert.Parameters.AddWithValue("@provolts", profile_volts);
                insert.Parameters.AddWithValue("@prokerf", profile_kerf);
                insert.Parameters.AddWithValue("@prosped", profile_speed);
                insert.Parameters.AddWithValue("@proignheight", profile_ignition_height);
                insert.Parameters.AddWithValue("@propiheight", profile_pierce_height);
                insert.Parameters.AddWithValue("@procutheight", profile_cut_height);
                insert.Parameters.AddWithValue("@propitime", profile_pierce_time);
                insert.Parameters.AddWithValue("@procurend", profile_current_end);
                insert.Parameters.AddWithValue("@procurstart", profile_current_start);
                insert.Parameters.AddWithValue("@procurvalue", profile_current_value);
                insert.Parameters.AddWithValue("@pronoz", profile_nozzle);
                insert.Parameters.AddWithValue("@proshield", profile_shield);
                insert.Parameters.AddWithValue("@proelectro", profile_electrode);
                insert.Parameters.AddWithValue("@lastup", last_updated);
                insert.ExecuteNonQuery();
                MessageBox.Show("Tech record for " + profile_thickness + "mm " + profile_material + " has been added succesfully.");
            }
            catch(SqliteException ea)
            {
                MessageBox.Show(ea.ToString());
            }
        }
        #endregion

        #region Update DB from Modify
        private void mod_btn_Click(object sender, EventArgs e)
        {
            try
            {
                SqliteConnection coni = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                for (int item = 0; item <= modify_datagrid.Rows.Count - 1; item++)
                {
                    SqliteCommand cmdi = new SqliteCommand("UPDATE tech_records SET profile_ref=@proref, profile_material=@promat, profile_thickness=@prothic, profile_type=@protyp, profile_gases=@progas, profile_amps=@proamp, profile_volts=@provolt, profile_kerf=@prokerf, profile_speed=@prospeed, profile_ignition_height=@proigni, profile_pierce_height=@propiheight, profile_cut_height=@procutheight, profile_pierce_time=@propitime, profile_current_end=@procurend, profile_current_start=@procurstar, profile_current_value=@procur, profile_nozzle=@pronoz, profile_shield=@proshi, profile_electrode=@proelec, last_updated=@proupdat WHERE id=@proid", coni);
                    cmdi.Parameters.AddWithValue("@proid", modify_datagrid.Rows[item].Cells[0].Value);
                    cmdi.Parameters.AddWithValue("@proref", modify_datagrid.Rows[item].Cells[1].Value);
                    cmdi.Parameters.AddWithValue("@promat", modify_datagrid.Rows[item].Cells[2].Value);
                    cmdi.Parameters.AddWithValue("@prothic", modify_datagrid.Rows[item].Cells[3].Value);
                    cmdi.Parameters.AddWithValue("@protyp", modify_datagrid.Rows[item].Cells[4].Value);
                    cmdi.Parameters.AddWithValue("@progas", modify_datagrid.Rows[item].Cells[5].Value);
                    cmdi.Parameters.AddWithValue("@proamp", modify_datagrid.Rows[item].Cells[6].Value);
                    cmdi.Parameters.AddWithValue("@provolt", modify_datagrid.Rows[item].Cells[7].Value);
                    cmdi.Parameters.AddWithValue("@prokerf", modify_datagrid.Rows[item].Cells[8].Value);
                    cmdi.Parameters.AddWithValue("@prospeed", modify_datagrid.Rows[item].Cells[9].Value);
                    cmdi.Parameters.AddWithValue("@proigni", modify_datagrid.Rows[item].Cells[10].Value);
                    cmdi.Parameters.AddWithValue("@propiheight", modify_datagrid.Rows[item].Cells[11].Value);
                    cmdi.Parameters.AddWithValue("@procutheight", modify_datagrid.Rows[item].Cells[12].Value);
                    cmdi.Parameters.AddWithValue("@propitime", modify_datagrid.Rows[item].Cells[13].Value);
                    cmdi.Parameters.AddWithValue("@procurend", modify_datagrid.Rows[item].Cells[14].Value);
                    cmdi.Parameters.AddWithValue("@procurstar", modify_datagrid.Rows[item].Cells[15].Value);
                    cmdi.Parameters.AddWithValue("@procur", modify_datagrid.Rows[item].Cells[16].Value);
                    cmdi.Parameters.AddWithValue("@pronoz", modify_datagrid.Rows[item].Cells[17].Value);
                    cmdi.Parameters.AddWithValue("@proshi", modify_datagrid.Rows[item].Cells[18].Value);
                    cmdi.Parameters.AddWithValue("@proelec", modify_datagrid.Rows[item].Cells[19].Value);
                    cmdi.Parameters.AddWithValue("@proupdat", DateTime.Now.ToString());
                    coni.Open();
                    cmdi.ExecuteNonQuery();
                    coni.Close();
                }
                modify_datagrid.Rows.Clear();
                Search_Results.Rows.Clear();
                RefreshModify_Datagrid();
                AutoFillDatagrid();
                action_txt.Text = "All rows updated succesfully...";
            }catch(SqliteException ea)
            {
                MessageBox.Show(ea.ToString());
            }
            

        }
        public void RefreshModify_Datagrid()
        {
            try
            {
                modify_datagrid.Rows.Clear();
                SqliteConnection coni = new SqliteConnection("Data Source=tech-table.sqlite;Version=3");
                SqliteCommand cmd = new SqliteCommand("SELECT * FROM tech_records", coni);
                coni.Open();
                using (SqliteDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        modify_datagrid.Rows.Add(new object[] {
                                rd.GetValue(0),  // U can use column index
                                rd.GetValue(1),  // Or column name like this
                                rd.GetValue(2),
                                rd.GetValue(3),
                                rd.GetValue(4),
                                rd.GetValue(5),
                                rd.GetValue(6),
                                rd.GetValue(7),
                                rd.GetValue(8),
                                rd.GetValue(9),
                                rd.GetValue(10),
                                rd.GetValue(11),
                                rd.GetValue(12),
                                rd.GetValue(13),
                                rd.GetValue(14),
                                rd.GetValue(15),
                                rd.GetValue(16),
                                rd.GetValue(17),
                                rd.GetValue(18),
                                rd.GetValue(19),
                                rd.GetValue(20)
                                });
                    }
                }
                coni.Close();
            }catch(SqliteException ea)
            {
                MessageBox.Show(ea.ToString());
            }
        }
        #endregion

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Exported from QC";
                // storing header part in Excel  
                for (int i = 1; i < Search_Results.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = Search_Results.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < Search_Results.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < Search_Results.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = Search_Results.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // save the application  
                workbook.SaveAs("export-view.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application  
                app.Quit();
            }
            catch (Exception ea)
            {
                MessageBox.Show(ea.ToString());
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

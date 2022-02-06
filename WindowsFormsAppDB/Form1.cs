using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsAppDB
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommandBuilder scb;
        private int IdTool = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.toolsTableAdapter.Fill(this.agriculturalMachineryDataSet.Tools);

            this.orderTableAdapter.Fill(this.agriculturalMachineryDataSet.Order);

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString);
                con.Open();
                sda = new SqlDataAdapter("SELECT * FROM [Tools]", con);
                ds = new DataSet();
                sda.Fill(ds, "Tools");
                dataGridView4.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_Update_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    scb = new SqlCommandBuilder(sda);
            //    sda.Update(ds, "Order");

            //    MessageBox.Show("Information Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Update [Tools] set NameTool=@NameTool, NumberTool=@NumberTool where IdTool=@IdTool", connection);

                //  cmd.Parameters.Add("@NameTool", SqlDbType.VarChar, 100).Value = "NameTool";
                // cmd.Parameters.Add("@NumberTool", SqlDbType.BigInt)22;
                // cmd.Parameters.Add("@IdTool", SqlDbType.BigInt);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                for(int item = 0; item <= dataGridView4.Rows.Count - 1; item++)
                {
                    cmd.Parameters.AddWithValue("@IdTool", dataGridView4.Rows[item].Cells[0].Value);
                    cmd.Parameters.AddWithValue("@NameTool", dataGridView4.Rows[item].Cells[1].Value);
                    cmd.Parameters.AddWithValue("@NumberTool", dataGridView4.Rows[item].Cells[2].Value);
                    connection.Open();
                    MessageBox.Show(cmd.ExecuteNonQuery().ToString());
                    connection.Close();

                }



                //for(int item = 0; item <= dataGridView4.Rows.Count - 1; item++)
                //{

                //    using(SqlConnection sqlConnectionToUpdate = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
                //    {
                //        SqlCommand cm = new SqlCommand("Update [Tools] set NameTool=@NameTool, NumberTool=@NumberTool where IdTool=@IdTool", sqlConnectionToUpdate);
                //        cm.Parameters.AddWithValue("@NameTool", dataGridView4.Rows[item].Cells[1].Value);
                //        cm.Parameters.AddWithValue("@NumberTool", dataGridView4.Rows[item].Cells[2].Value);
                //        cm.Parameters.AddWithValue("@IdTool", dataGridView4.Rows[item].Cells[0].Value);

                //         sqlConnectionToUpdate.Open();
                //        //dataGridView4.DataSource = cm.ExecuteNonQuery();
                //        MessageBox.Show(cm.ExecuteScalar().ToString());
                //        sqlConnectionToUpdate.Close();

                //        // cm.ExecuteNonQuery();
                //        MessageBox.Show("sukses");


                //    }
                // }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(tB_IdCustomer.Text != string.Empty || tb_NameCustomer.Text != string.Empty || tB_PhoneNumber.Text != string.Empty || tb_Address.Text != string.Empty || tb_CurrentAccount.Text != string.Empty)
            {
                using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
                {
                    sqlConnection.Open();

                    //Додаємо Customers
                    SqlCommand commandCustomers = new SqlCommand(
                        "INSERT INTO [Customers] (IdCustomer, NameCustomer, PhoneNumber, Address, CurrentAccount) " +
                        "VALUES (@IdCustomer, @NameCustomer, @PhoneNumber, @Address, @CurrentAccount)", sqlConnection);

                    commandCustomers.Parameters.AddWithValue("IdCustomer", tB_IdCustomer.Text);
                    commandCustomers.Parameters.AddWithValue("NameCustomer", tb_NameCustomer.Text);
                    commandCustomers.Parameters.AddWithValue("PhoneNumber", tB_PhoneNumber.Text);
                    commandCustomers.Parameters.AddWithValue("Address", tb_Address.Text);
                    commandCustomers.Parameters.AddWithValue("CurrentAccount", tb_CurrentAccount.Text);

                    MessageBox.Show(commandCustomers.ExecuteNonQuery().ToString());
                }
            }
            else
                MessageBox.Show("Будь ласка, введіть дані!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(tb_IdTool.Text != string.Empty || tb_NameTool.Text != string.Empty || tb_NumberTool.Text != string.Empty)
            {
                using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
                {
                    sqlConnection.Open();

                    //Додаємо Tools
                    SqlCommand commandTools = new SqlCommand(
                    "INSERT INTO [Tools] (IdTool, NameTool, NumberTool) " +
                    "VALUES (@IdTool, @NameTool, @NumberTool)", sqlConnection);

                    commandTools.Parameters.AddWithValue("IdTool", tb_IdTool.Text);
                    commandTools.Parameters.AddWithValue("NameTool", tb_NameTool.Text);
                    commandTools.Parameters.AddWithValue("NumberTool", tb_NumberTool.Text);


                    MessageBox.Show(commandTools.ExecuteNonQuery().ToString());
                }
            }
            else
                MessageBox.Show("Будь ласка, введіть дані!");
        }
        private void tb_Insert_Click(object sender, EventArgs e)
        {





            if(tB_Id.Text != string.Empty || tB_CustomerId.Text != string.Empty || tb_ToolId.Text != string.Empty || tb_Number.Text != string.Empty)
            {
                using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
                {
                    sqlConnection.Open();

                    //Додаємо Order
                    SqlCommand commandOrder = new SqlCommand(
                    "INSERT INTO [Order] (Id, CustomerId, ToolId, Number, Date, Done_NotDone) " +
                    "VALUES (@Id, @CustomerId, @ToolId, @Number, @Date, @Done_NotDone)", sqlConnection);

                    commandOrder.Parameters.AddWithValue("Id", tB_Id.Text);
                    commandOrder.Parameters.AddWithValue("CustomerId", tB_CustomerId.Text);
                    commandOrder.Parameters.AddWithValue("ToolId", tb_ToolId.Text);
                    commandOrder.Parameters.AddWithValue("Number", tb_Number.Text);
                    commandOrder.Parameters.AddWithValue("Date", tb_Date.Text);
                    commandOrder.Parameters.AddWithValue("Done_NotDone", chB_Done_NotDone.Checked);

                    MessageBox.Show(commandOrder.ExecuteNonQuery().ToString());
                }
            }
            else
                MessageBox.Show("Будь ласка, введіть дані!");
        }




        private void btn_Album_Click(object sender, EventArgs e)
        {


        }


        private void btn_Perfomers_Click(object sender, EventArgs e)
        {

        }

        private void btn_RecordCompanys_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Clean_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        //{
        //    using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
        //    {
        //        if(comboBox3.SelectedIndex == 0)
        //        {
        //            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from [Tools]", sqlConnection);

        //            DataSet dataSet = new DataSet();

        //            dataAdapter.Fill(dataSet);
        //            dataGridView1.DataSource = dataSet.Tables[0];

        //        }
        //        if(comboBox3.SelectedIndex == 1)
        //        {

        //            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from [Customers]", sqlConnection);

        //            DataSet dataSet = new DataSet();

        //            dataAdapter.Fill(dataSet);
        //            dataGridView1.DataSource = dataSet.Tables[0];

        //        }
        //        if(comboBox3.SelectedIndex == 2)
        //        {

        //            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from [Order]", sqlConnection);

        //            DataSet dataSet = new DataSet();

        //            dataAdapter.Fill(dataSet);
        //            dataGridView1.DataSource = dataSet.Tables[0];

        //        }
        //    }

        //}


        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        //private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
        //    {
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter($"select Id as 'Код замовлення', IdTool as 'Код інструмента', NameTool as 'Назва інструмента' from [Order] join [Tools] on IdTool = ToolId where NameTool = N'{comboBox4.Text}'", sqlConnection);

        //        DataSet dataSet = new DataSet();

        //        dataAdapter.Fill(dataSet);
        //        dataGridView4.DataSource = dataSet.Tables[0];
        //    }
        //}

        private void agriculturalMachineryDataSet1BindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        //private void comboBox2_SelectedIndexChanged_2(object sender, EventArgs e)
        //{
        //    using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
        //    {
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter($"select Id as 'Код замовлення', IdTool as 'Код інструмента', NameTool as 'Назва інструмента' from [Order] join [Tools] on IdTool = ToolId where Id = {comboBox2.Text}", sqlConnection);
        //        DataSet dataSet = new DataSet();

        //        dataAdapter.Fill(dataSet);
        //        dataGridView4.DataSource = dataSet.Tables[0];
        //    }
        //}

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
            {
                if(comboBox5.SelectedIndex == 0)
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from [Tools]", sqlConnection);

                    DataSet dataSet = new DataSet();

                    dataAdapter.Fill(dataSet);
                    dataGridView1.DataSource = dataSet.Tables[0];

                }
                if(comboBox5.SelectedIndex == 1)
                {

                    SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from [Customers]", sqlConnection);

                    DataSet dataSet = new DataSet();

                    dataAdapter.Fill(dataSet);
                    dataGridView1.DataSource = dataSet.Tables[0];

                }
                if(comboBox5.SelectedIndex == 2)
                {

                    SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from [Order]", sqlConnection);

                    DataSet dataSet = new DataSet();

                    dataAdapter.Fill(dataSet);
                    dataGridView1.DataSource = dataSet.Tables[0];

                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter($"select Id as 'Код замовлення', IdTool as 'Код інструмента', NameTool as 'Назва інструмента' from [Order] join [Tools] on IdTool = ToolId where NameTool = N'{comboBox1.Text}'", sqlConnection);

                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);
                dataGridView2.DataSource = dataSet.Tables[0];
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter($"select Id as 'Код замовлення', IdTool as 'Код інструмента', NameTool as 'Назва інструмента' from [Order] join [Tools] on IdTool = ToolId where Id = {comboBox6.Text}", sqlConnection);
                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);
                dataGridView3.DataSource = dataSet.Tables[0];
            }
        }
        // SqlConnection sqlConnectionToUpdate = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString);
        public static bool ExistsRecord(string str)
        {
            string strSql = "select * from Tools where IdTool= '" + str + "'";
            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = strSql;
                cmd.CommandType = CommandType.Text;
                SqlDataReader datareader = cmd.ExecuteReader();
                return datareader.HasRows;
            }

        }

        public static void ExecuteSql(SqlParameter[] sqlParas, string strSql)
        {
            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand(strSql, connection))
                {
                    foreach(SqlParameter sp in sqlParas)
                    {
                        cmd.Parameters.Add(sp);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static bool UpdataFromDGVtoDB(DataGridView dgv)
        {
            try
            {
                for(int i = 0; i < dgv.Rows.Count - 1; i++)
                {
                    string strCADTYPE;
                    if(dgv.Rows[i].Cells[1].Value.ToString().StartsWith("["))
                    {
                        strCADTYPE = "1";
                    }
                    else
                    {
                        strCADTYPE = "2";
                    }

                    SqlParameter[] sqlParas = new SqlParameter[]
                    {
                        new SqlParameter("@IdTool", dgv.Rows[i].Cells[0].Value.ToString()),
                        new SqlParameter("@NameTool", dgv.Rows[i].Cells[1].Value.ToString()),
                        new SqlParameter("@NumberTool", dgv.Rows[i].Cells[2].Value.ToString())
                    };

                    if(ExistsRecord(dgv.Rows[i].Cells[0].Value.ToString()))
                    {
                        ExecuteSql(sqlParas, "update Tools set NameTool = @NameTool,NumberTool = @NumberTool where IdTool = @IdTool");
                    }
                    else
                    {
                        ExecuteSql(sqlParas, "insert into CorrespondFields values(@SdeLayerName,@CadLayerName,@SdeField,@CadField,@FieldType,@CADTYPE)");
                    }
                }
                    return true;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        } 
        private void btn_Up_Click(object sender, EventArgs e)
        {
            using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT o.Id, c.NameCustomer as 'Імя замовника', c.PhoneNumber as 'Телефон', c.Address as 'Адреса', c.CurrentAccount as 'Особовий рахунок', t.NameTool as 'Інструмент', t.NumberTool as 'Кількість інструментів', o.Date as 'Дата замовлення', o.Done_NotDone as 'Статус замовлення' FROM [Order] as o, Customers as c, Tools as t where o.CustomerId = c.IdCustomer and t.IdTool = o.ToolId", sqlConnection);
                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);
                dataGridView4.DataSource = dataSet.Tables[0];

            }
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Update [Tools] set NameTool=@NameTool, NumberTool=@NumberTool where IdTool=@IdTool", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@companyName", userName);
                cmd.Parameters.AddWithValue("@IdTool", dataGridView4.Rows[e.RowIndex].Cells[0].Value);
                cmd.Parameters.AddWithValue("@NameTool", dataGridView4.Rows[e.RowIndex].Cells[1].Value);
                cmd.Parameters.AddWithValue("@NumberTool", dataGridView4.Rows[e.RowIndex].Cells[2].Value);

                conn.Open();

                MessageBox.Show(cmd.ExecuteNonQuery().ToString());
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            UpdataFromDGVtoDB(dataGridView4);
            SelectTable("Tools");
        }

        void SelectTable(string NameTable)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["AgriculturalMachinery"].ConnectionString);
                con.Open();
                sda = new SqlDataAdapter($"SELECT * FROM [{NameTable}]", con);
                ds = new DataSet();
                sda.Fill(ds, "Tools");
                dataGridView4.DataSource = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

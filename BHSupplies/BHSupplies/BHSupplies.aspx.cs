using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;


namespace BHSupplies
{
    public partial class BHSupplies : System.Web.UI.Page
    {
        bool isPageRefreshed = false;
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        string cs2 = ConfigurationManager.ConnectionStrings["DBCS2"].ConnectionString;
        string FromEmail = ConfigurationManager.AppSettings["BHSuppliesFromEmail"].ToString();
        string BccEmail = ConfigurationManager.AppSettings["BHSuppliesBccEmail"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeQuantities();

                // Identify Environment 
                SqlConnectionStringBuilder csbuilder = new SqlConnectionStringBuilder(cs);

                if (csbuilder.DataSource == "WebAppsAGL" && csbuilder.InitialCatalog == "EmpathAPI")
                {
                    lblEnvironment.Text = "";
                }
                else
                {
                    lblEnvironment.Text = "( TEST )";
                }

                if (lblUserName.Text == "")
                {
                    UserInfo_Load();
                }

                if (lblUserName.Text == "")
                {
                    lblError.Text = "&nbsp;&nbsp;ERROR_00: ACCESS DENIED!!! Unable to verify your credentials! Please contact the Administrator.&nbsp;&nbsp;";
                }

                LocationDDL_Load();

                ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
                Session["SessionId"] = ViewState["ViewStateId"].ToString();
            }
            else
            {
                if (Session["SessionId"] == null) { Session["SessionId"] = ""; }
                if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                {
                    isPageRefreshed = true;
                    lblMessage.Text = "";
                    lblError.Text = "";
                    ClearScreen();
                }

                Session["SessionId"] = System.Guid.NewGuid().ToString();
                ViewState["ViewStateId"] = Session["SessionId"].ToString();
            }
        }
        //}
        protected void UserInfo_Load()
        {
            // Populate User's information from EmpathPortal.dbo.UserDetails
            lblError.Text = "";
            lblUserName.Text = "";
            ViewState["UserID"] = "";
            ViewState["UserInfo"] = "";
            ViewState["UserName"] = "";
            ViewState["UserFirstName"] = "";
            ViewState["UserLastName"] = "";
            ViewState["UserDepartment"] = "";
            ViewState["UserPhone"] = "";
            ViewState["UserEmail"] = "";


            string sUserID = Request.LogonUserIdentity.Name.ToString().Substring(11);

            SqlConnection con = new SqlConnection(cs);
            try
            {
                SqlCommand cmd = new SqlCommand("select NickName, FirstName, Lastname, UserID, Department, SystemPhone, SystemEmail from EmpathPortal.dbo.UserDetailsPlus where ADUserName = '" + sUserID + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                ViewState["UserID"] = reader["UserID"].ToString();
                ViewState["UserInfo"] = "User: " + reader["UserID"].ToString() + " " + reader["NickName"].ToString();
                ViewState["UserName"] = reader["NickName"];
                ViewState["UserFirstName"] = reader["FirstName"];
                ViewState["UserLastName"] = reader["LastName"];
                ViewState["UserTeam"] = reader["Department"];
                ViewState["UserPhone"] = reader["SystemPhone"];
                ViewState["UserEmail"] = reader["SystemEmail"];


                lblUserName.Text = reader["NickName"].ToString();
                reader.Close();
                lblUserName.ForeColor = System.Drawing.Color.Pink;
            }
            catch (Exception ex)
            {
                lblError.Text = "&nbsp;&nbsp;UNAUTHORIZED USER!!! ACCESS DENIED!!!&nbsp;&nbsp;";
                lblUserName.ForeColor = System.Drawing.Color.Yellow;
                lblUserName.Text = "UserID: ??? " + sUserID;
                divMain.Visible = false;
                divSubmit.Visible = false;

                //WriteLog("UserInfo_Load()", "Unable to load User Info from EmpathPortal.dbo.UserDetails table! " + lblError.Text);
            }
            finally { con.Close(); }

            txtFirstName.Text = ViewState["UserFirstName"].ToString();
            txtLastName.Text = ViewState["UserLastName"].ToString();
            txtEmail.Text = ViewState["UserEmail"].ToString();
            txtTeam.Text = ViewState["UserTeam"].ToString();

        }

        protected void ClearScreen()
        {
            ddlLocation.SelectedIndex = 0;
            ddlBuilding.SelectedIndex = 0;
            InitializeQuantities();
            ClearCheckBoxes();
        }

        protected void ClearCheckBoxes()
        {
            if (cbBags.Checked == true)
            {
                cbBags.Checked = false;
            }
            if (cbCleaners.Checked == true)
            {
                cbCleaners.Checked = false;
            }
            if (cbKitchen.Checked == true)
            {
                cbKitchen.Checked = false;
            }
            if (cbLaundry.Checked == true)
            {
                cbLaundry.Checked = false;
            }
            if (cbLiners.Checked == true)
            {
                cbLiners.Checked = false;
            }
            if (cbPaperGoods.Checked == true)
            {
                cbPaperGoods.Checked = false;
            }
            if (cbSoap.Checked == true)
            {
                cbSoap.Checked = false;
            }
            if (cbWater.Checked == true)
            {
                cbWater.Checked = false;
            }
        }

        protected void LocationDDL_Load()
        {
            // ********************************
            // Populate Location drop down list
            // ********************************
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Name FROM OS_Locations ORDER BY Name", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    int i = 1;
                    ListItem newItem = new ListItem();
                    newItem.Text = "";
                    newItem.Value = "0";
                    ddlLocation.Items.Add(newItem);

                    while (reader.Read())
                    {
                        newItem = new ListItem();
                        newItem.Text = reader["Name"].ToString();
                        newItem.Value = i++.ToString();
                        ddlLocation.Items.Add(newItem);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    lblError.Text = "&nbsp;&nbsp;ERROR_02: Unable to read data from MR_Location table! Please contact the Administrator.<br /> SQL ERROR: " + ex.Message + "&nbsp;&nbsp;";
                    //WriteLog("DropDownLists_Load()", lblError.Text);
                }
            }
        }

        protected void InitializeQuantities()
        {
            //initialize all quantities to zero
            for (int i = 1; i < 68; i++)
            {
                TextBox tb = this.FindControl("txt" + i) as TextBox;
                if (tb != null)
                {
                    tb.Text = "0";
                }

            }
            ddlJMops.SelectedIndex = 0;
            ddlDrWater.SelectedIndex = 0;
        }

        protected int CheckQuantities()
        {
            int BagFlag = 0;
            int CleanerFlag = 0;
            int KitchenFlag = 0;
            int LaundryFlag = 0;
            int LinerFlag = 0;
            int PaperGoodsFlag = 0;
            int SoapFlag = 0;
            int WaterFlag = 0;
            int FlagSum = 0;
            lblNote.Text = "";

            // Replace blank quantities with '0'.
            for (int j = 1; j < 68; j++)
            {
                if (Page.FindControl("txt" + j) != null)
                {
                    TextBox tb1 = this.FindControl("txt" + j) as TextBox;
                    if (tb1.Text == "")
                    {
                        tb1.Text = "0";
                    }
                }
            }


            for (int i = 1; i < 12; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;


                if (tb1 != null)
                {

                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbBags.Text = "Bags - quantity exceeded";
                        cbBags.ForeColor = System.Drawing.Color.Red;
                        break;
                    }


                    if (tb1.Text != "0" & tb1.Text != "")
                    {
                        BagFlag = 1;
                    }

                    if (BagFlag == 1)
                    {
                        cbBags.Text = "Bags - item(s) selected";
                        cbBags.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbBags.Text = "Bags";
                        cbBags.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            for (int i = 12; i < 28; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;

                if (tb1 != null)
                {
                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbCleaners.Text = "Cleaners - quantity exceeded";
                        cbCleaners.ForeColor = System.Drawing.Color.Red;
                        break;
                    }

                    if ((tb1.Text != "0" & tb1.Text != "")  || (ddlJMops.SelectedIndex != 0))
                    {
                        CleanerFlag = 1;
                    }

                    //if ((CleanerFlag == 0) && (ddlJMops.SelectedIndex != 0))
                    //{
                    //    CleanerFlag = 1;
                    //}

                    if (CleanerFlag == 1)
                    {
                        cbCleaners.Text = "Cleaners - item(s) selected";
                        cbCleaners.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbCleaners.Text = "Cleaners";
                        cbCleaners.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            for (int i = 28; i < 41; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;

                if (tb1 != null)
                {
                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbKitchen.Text = "Kitchen - quantity exceeded";
                        cbKitchen.ForeColor = System.Drawing.Color.Red;
                        break;
                    }

                    if (tb1.Text != "0" & tb1.Text != "")
                    {
                        KitchenFlag = 1;
                    }

                    if (KitchenFlag == 1)
                    {
                        cbKitchen.Text = "Kitchen - item selected";
                        cbKitchen.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbKitchen.Text = "Kitchen";
                        cbKitchen.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            for (int i = 41; i < 44; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;

                if (tb1 != null)
                {
                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbLaundry.Text = "Laundry - quantity exceeded";
                        cbLaundry.ForeColor = System.Drawing.Color.Red;
                        break;
                    }

                    if (tb1.Text != "0" & tb1.Text != "")
                    {
                        LaundryFlag = 1;
                    }

                    if (LaundryFlag == 1)
                    {
                        cbLaundry.Text = "Laundry - item(s) selected";
                        cbLaundry.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbLaundry.Text = "Laundry";
                        cbLaundry.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            for (int i = 44; i < 51; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;

                if (tb1 != null)
                {
                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbLiners.Text = "Liners - quantity exceeded";
                        cbLiners.ForeColor = System.Drawing.Color.Red;
                        break;
                    }

                    if (tb1.Text != "0" & tb1.Text != "")
                    {
                        LinerFlag = 1;
                    }

                    if (LinerFlag == 1)
                    {
                        cbLiners.Text = "Liners - item(s) selected";
                        cbLiners.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbLiners.Text = "Liners";
                        cbLiners.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            for (int i = 51; i < 60; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;

                if (tb1 != null)
                {
                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbPaperGoods.Text = "Paper Goods - quantity exceeded";
                        cbPaperGoods.ForeColor = System.Drawing.Color.Red;
                        break;
                    }

                    if (tb1.Text != "0" & tb1.Text != "")
                    {
                        PaperGoodsFlag = 1;
                    }

                    if (PaperGoodsFlag == 1)
                    {
                        cbPaperGoods.Text = "Paper Goods - item(s) selected";
                        cbPaperGoods.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbPaperGoods.Text = "Paper Goods";
                        cbPaperGoods.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            for (int i = 60; i < 66; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;

                if (tb1 != null)
                {
                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbSoap.Text = "Soap - quantity exceeded";
                        cbSoap.ForeColor = System.Drawing.Color.Red;
                        //cbPaper.Checked = true;
                        break;
                    }

                    if (tb1.Text != "0" & tb1.Text != "")
                    {
                        SoapFlag = 1;
                    }

                    if (SoapFlag == 1)
                    {
                        cbSoap.Text = "Soap - item(s) selected";
                        cbSoap.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbSoap.Text = "Soap";
                        cbSoap.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            for (int i = 66; i < 68; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                Label lbl1 = this.FindControl("lbl" + i) as Label;

                if (tb1 != null)
                {

                    if (int.Parse(tb1.Text) > 10)
                    {
                        lblNote.Text = "Quantity exceeded for " + lbl1.Text + " limit is 10.";
                        cbWater.Text = "Water - quantity exceeded";
                        cbWater.ForeColor = System.Drawing.Color.Red;
                        break;
                    }

                    if ((tb1.Text != "0" & tb1.Text != "") || (ddlDrWater.SelectedIndex != 0))
                    {
                        WaterFlag = 1;
                    }

                    //if ((WaterFlag == 0) && (ddlDrWater.SelectedIndex != 0))
                    //{
                    //    WaterFlag = 1;
                    //}

                    if (WaterFlag == 1)
                    {
                        cbWater.Text = "Water - item(s) selected";
                        cbWater.ForeColor = System.Drawing.Color.Purple;
                    }
                    else
                    {
                        cbWater.Text = "Water";
                        cbWater.ForeColor = System.Drawing.Color.Black;
                    }

                }

            }

            FlagSum = BagFlag + CleanerFlag + KitchenFlag + LaundryFlag + LinerFlag + PaperGoodsFlag + SoapFlag + WaterFlag;
            if (FlagSum == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

        protected void cbBags_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }

        protected void cbCleaners_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }

        protected void cbKitchen_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }

        protected void cbLaundry_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }

        protected void cbLiners_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }

        protected void cbPaperGoods_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }

        protected void cbSoap_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }

        protected void cbWater_CheckedChanged(object sender, EventArgs e)
        {
            CheckQuantities();
            lblMessage.Text = "";
            lblError.Text = "";
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Int32 SeqNo = 0;
            string TimeStamp = DateTime.Now.ToString();
            SqlConnection con = new SqlConnection(cs);
            SqlConnection con2 = new SqlConnection(cs);
            int quantity = CheckQuantities();
            string OrderAddress1 = "";
            string OrderAddress2 = "";
            string OrderCostCenter = "";

            if (!Session.IsNewSession)
            {

                if (!isPageRefreshed)
                {
                    if (quantity == 0)
                    {
                        lblMessage.Text = "";
                        lblError.Text = "Please select an item.";
                    }
                    else
                    {
                        lblError.Text = "";

                        // ********************************************************
                        // Obtain next Order ID number from sequence.
                        // ********************************************************
                        SqlCommand cmd = new SqlCommand("SELECT NEXT VALUE For BHS_Seq", con);
                        con.Open();
                        SeqNo = (Int32)cmd.ExecuteScalar();
                        con.Close();
                        string OrderID = string.Format("{0:00000}", SeqNo);

                        // Select Address and Cost Center from OS_Locations table. 
                        try
                        {
                            SqlCommand cmd1 = new SqlCommand("SELECT Address1, Address2, HKCostCenter FROM OS_Locations WHERE Name = '" + ddlLocation.SelectedItem.Text + "'", con);
                            con.Open();
                            SqlDataReader reader = cmd1.ExecuteReader();

                            reader.Read();

                            OrderAddress1 = reader["Address1"].ToString();
                            OrderAddress2 = reader["Address2"].ToString();
                            OrderCostCenter = reader["HKCostCenter"].ToString();

                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = "&nbsp;&nbsp;ERROR_05: Unable to read data from OS_Locations table! Please contact the Administrator.<br /> SQL ERROR: " + ex.Message + "&nbsp;&nbsp;";
                            //WriteLog("btnSubmit_Click()", lblError.Text);
                        }

                        //Create order record in the OS_Order table
                        try
                        {
                            String sql = "INSERT INTO BHS_Order (OrderID,FirstName,LastName,Email,Team,Location,Building,CostCenter,EnteredBy,DateCreated) VALUES (@OrderID,@FirstName,@LastName,@Email,@Team,@Location,@Building,@CostCenter,@EnteredBy,@DateCreated)";
                            using (SqlConnection connection = con2)
                            {
                                SqlCommand cmd2 = new SqlCommand(sql, con2);
                                cmd2.Parameters.AddWithValue("@OrderID", OrderID);
                                cmd2.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                                cmd2.Parameters.AddWithValue("@LastName", txtLastName.Text);
                                cmd2.Parameters.AddWithValue("@Email", txtEmail.Text);
                                cmd2.Parameters.AddWithValue("@Team", txtTeam.Text);
                                cmd2.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Text);
                                cmd2.Parameters.AddWithValue("@Building", (ddlBuilding.SelectedItem == null) ? "" : ddlBuilding.SelectedItem.Text);
                                cmd2.Parameters.AddWithValue("@CostCenter", OrderCostCenter);
                                cmd2.Parameters.AddWithValue("@EnteredBy", ViewState["UserName"].ToString());
                                cmd2.Parameters.AddWithValue("@DateCreated", TimeStamp);
                                cmd2.Connection.Open();
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = "&nbsp;&nbsp;ERROR_06: Unable to save order record! Error:" + ex.Message + "&nbsp;&nbsp;";
                            //WriteLog("SendConfirm()", lblMessage.Text);
                        }
                        finally
                        {
                            con2.Close();
                        }

                        // ************************************************************************************************************************************************
                        // Build the subject and body of the email. For each item ordered, list item and quantity in email and make an entry into the OS_OrderDetail table.
                        // ************************************************************************************************************************************************
                        string Subject = "";
                        var Client = new SmtpClient("webmail.thehospice.net");
                        StringBuilder sbody = new StringBuilder();

                        Subject = "Building Housekeeping Supply Order from " + txtFirstName.Text + " " + txtLastName.Text + " | Order ID: " + OrderID;

                        sbody.Append("<html><body>");

                        sbody.Append("<table><TR><TD>");

                        sbody.Append("<table align=left border=0 cellpadding=0 cellspacing=0 >");
                        sbody.Append("<TR><TD width=350><b>Name: </b></TD> <TD width=250> " + txtFirstName.Text + " " + txtLastName.Text + "</TD></TR>");
                        sbody.Append("<TR><TD width=350><b>Email: </b></TD> <TD width=250> " + txtEmail.Text + "</TD></TR>");
                        sbody.Append("<TR><TD width=350><b>Department: </b></TD> <TD width=250> " + txtTeam.Text + "</TD></TR>");
                        sbody.Append("<TR><TD width=350><b>Location: </b></TD> <TD width=250> " + ddlLocation.SelectedItem.Text + "</TD></TR>");
                        if (ddlLocation.SelectedItem.Text == "Mary J. Labyak Service Center")
                        {
                            sbody.Append("<TR><TD width=350><b>Building: </b></TD> <TD>" + ddlBuilding.SelectedItem.Text + "</TD></TR>");
                        }
                        sbody.Append("<TR><TD width=350></TD> <TD width=250> " + OrderAddress1 + "</TD></TR>");
                        sbody.Append("<TR><TD width=350></TD> <TD width=250> " + OrderAddress2 + "</TD></TR>");
                        sbody.Append("<TR><TD width=350><b>Cost Center: </b></TD> <TD width=250> " + OrderCostCenter + "</TD></TR>");
                        sbody.Append("<TR><TD width=350><b>Order Submitted: </b></TD> <TD width=250> " + TimeStamp + "</TD></TR>");
                        sbody.Append("<TR><TD width=350><b>Order ID: </b></TD> <TD width=250> " + OrderID + "</TD></TR>");
                        sbody.Append("</table><br />");
                        sbody.Append("</TD></TR><TR><TD>");

                        sbody.Append("<br />");
                        sbody.Append("<table align=left border=0 cellpadding=0 cellspacing=0 >");
                        sbody.Append("<TR><TD width=350><b><u>Item</u></b></TD> <TD width=60 align=right><b><u>Quantity</u></b></TD></TR>");
                        sbody.Append("</table><br />");

                        sbody.Append("</TD></TR><TR><TD>");

                        sbody.Append("<table align=left border=0 cellpadding=0 cellspacing=0 >");

                        for (int i = 1; i < 68; i++)
                        {
                            if (Page.FindControl("txt" + i) != null)
                            {
                                TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                                Label lb1 = this.FindControl("lbl" + i) as Label;
                                String ProdName = lb1.Text.Split('(', ')')[0];
                                ProdName = ProdName.TrimEnd(':');

                                //if (tb1 != null)
                                //{
                                if (tb1.Text != "0" & tb1.Text != "")
                                {
                                    sbody.Append("<TR><TD width=330><b> " + lb1.Text + "</b></TD> <TD width=60 align=right><b> " + tb1.Text.TrimStart('0') + "</b></TD></TR>");

                                    //Create order detail record in the OS_OrderDetail table
                                    try
                                    {
                                        using (SqlConnection con3 = new SqlConnection(cs))
                                        {
                                            SqlCommand cmd3 = new SqlCommand("INSERT INTO BHS_OrderDetail (OrderID,ProductName,Quantity) VALUES (@OrderID,@ProductName,@Quantity)", con3);
                                            cmd3.Parameters.AddWithValue("@OrderID", OrderID);
                                            cmd3.Parameters.AddWithValue("@ProductName", ProdName);
                                            cmd3.Parameters.AddWithValue("@Quantity", tb1.Text);
                                            cmd3.Connection.Open();
                                            cmd3.ExecuteNonQuery();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        lblError.Text = "&nbsp;&nbsp;ERROR_07: Unable to save order detail record! Error:" + ex.Message + "&nbsp;&nbsp;";
                                        //WriteLog("SendConfirm()", lblMessage.Text);
                                    }
                                }
                                if ((i == 24) && (ddlJMops.SelectedIndex != 0))
                                {
                                    sbody.Append("<TR><TD width=330><b> " + lbl25.Text + "</b></TD> <TD width=60 align=right><b> " + ddlJMops.SelectedItem.Text + "</b></TD></TR>");
                                    try
                                    {
                                        using (SqlConnection con5 = new SqlConnection(cs))
                                        {
                                            SqlCommand cmd5 = new SqlCommand("INSERT INTO BHS_OrderDetail (OrderID,ProductName,Quantity) VALUES (@OrderID,@ProductName,@Quantity)", con5);
                                            cmd5.Parameters.AddWithValue("@OrderID", OrderID);
                                            cmd5.Parameters.AddWithValue("@ProductName", lbl25.Text.TrimEnd(':'));
                                            cmd5.Parameters.AddWithValue("@Quantity", ddlJMops.SelectedItem.Text);
                                            cmd5.Connection.Open();
                                            cmd5.ExecuteNonQuery();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        lblError.Text = "&nbsp;&nbsp;ERROR_08: Unable to save order detail record! Error:" + ex.Message + "&nbsp;&nbsp;";
                                        //WriteLog("SendConfirm()", lblMessage.Text);
                                    }
                                }
                                if ((i == 64) && (ddlDrWater.SelectedIndex != 0))
                                {
                                    sbody.Append("<TR><TD width=330><b> " + lbl65.Text + "</b></TD> <TD width=60 align=right><b> " + ddlDrWater.SelectedItem.Text + "</b></TD></TR>");
                                    try
                                    {
                                        using (SqlConnection con6 = new SqlConnection(cs))
                                        {
                                            SqlCommand cmd6 = new SqlCommand("INSERT INTO BHS_OrderDetail (OrderID,ProductName,Quantity) VALUES (@OrderID,@ProductName,@Quantity)", con6);
                                            cmd6.Parameters.AddWithValue("@OrderID", OrderID);
                                            cmd6.Parameters.AddWithValue("@ProductName", lbl65.Text.TrimEnd(':'));
                                            cmd6.Parameters.AddWithValue("@Quantity", ddlDrWater.SelectedItem.Text);
                                            cmd6.Connection.Open();
                                            cmd6.ExecuteNonQuery();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        lblError.Text = "&nbsp;&nbsp;ERROR_09: Unable to save order detail record! Error:" + ex.Message + "&nbsp;&nbsp;";
                                        //WriteLog("SendConfirm()", lblMessage.Text);
                                    }
                                }
                                //}
                            }
                        }
                        sbody.Append("<TR><TD width=325>_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _</TD> <TD width=80 align=right></TD></TR>");
                        sbody.Append("<TR><TD width=325>This is an automated email. Please do not reply.</TD><TD width=80 ></TD></TR>");
                        sbody.Append("</table>");
                        sbody.Append("<br /></body></html>");


                        // *****************************
                        // Construct the email and send.
                        // *****************************
                        var MM = new MailMessage();
                        MM.IsBodyHtml = true;
                        MM.To.Add(ViewState["UserEmail"].ToString());
                        MM.From = new MailAddress(FromEmail);
                        MM.Bcc.Add(BccEmail);
                        MM.Subject = Subject;
                        MM.Body = sbody.ToString();

                        try
                        {
                            Client.Send(MM);
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = "&nbsp;&nbsp;ERROR_09: Unable to send Office Supplies Order email.<br />ERROR: " + ex.Message + "&nbsp; &nbsp;";
                            //WriteLog("SendConfirm()", lblMessage.Text);
                        }

                        InitializeQuantities();
                        CheckQuantities();
                        ClearCheckBoxes();
                        lblMessage.Text = "Order has been submitted.";

                    }
                }
                else
                {
                    ClearScreen();
                    lblMessage.Text = "";
                    lblError.Text = "&nbsp;&nbsp;ERROR_10: Please complete form before submitting.";
                }
            }
            else
            {
                ClearScreen();
                lblMessage.Text = "";
                lblError.Text = "&nbsp;&nbsp;ERROR_11: Session has timed out ... Please re-enter and submit.";
            }

        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedItem.Text == "Mary J. Labyak Service Center")
            {
                ddlBuilding.Enabled = true;
                ddlBuilding.SelectedIndex = 1;
            }
            else
            {
                ddlBuilding.SelectedIndex = 0;
                ddlBuilding.Enabled = false;
            }
        }

    }
}
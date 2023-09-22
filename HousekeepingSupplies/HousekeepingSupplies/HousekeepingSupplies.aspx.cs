using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HousekeepingSupplies
{
    public partial class HousekeepingSupplies : System.Web.UI.Page
    {
        bool isPageRefreshed = false;
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        string cs2 = ConfigurationManager.ConnectionStrings["DBCS2"].ConnectionString;
        string FromEmail = ConfigurationManager.AppSettings["HousekeepingSuppliesFromEmail"].ToString();
        string BccEmail = ConfigurationManager.AppSettings["HousekeepingSuppliesBccEmail"].ToString();

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
                }

                Session["SessionId"] = System.Guid.NewGuid().ToString();
                ViewState["ViewStateId"] = Session["SessionId"].ToString();

                if (isPageRefreshed == true)
                {
                    InitializeQuantities();
                }
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
                SqlCommand cmd = new SqlCommand("select NickName, FirstName, Lastname, UserID, Team, SystemPhone, SystemEmail from EmpathPortal.dbo.UserDetails where ADUserName = '" + sUserID + "'", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                ViewState["UserID"] = reader["UserID"].ToString();
                ViewState["UserInfo"] = "User: " + reader["UserID"].ToString() + " " + reader["NickName"].ToString();
                ViewState["UserName"] = reader["NickName"];
                ViewState["UserFirstName"] = reader["FirstName"];
                ViewState["UserLastName"] = reader["LastName"];
                //ViewState["UserDepartment"] = reader["Department"];
                ViewState["UserTeam"] = reader["Team"];
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
                    lblError.Text = "&nbsp;&nbsp;ERROR_01: Unable to read data from MR_Location table! Please contact the Administrator.<br /> SQL ERROR: " + ex.Message + "&nbsp;&nbsp;";
                    //WriteLog("DropDownLists_Load()", lblError.Text);
                }
            }
        }

        protected void BuildingDDL_Load()
        {
            // *************************************
            // Populate Building/Team drop down list
            // *************************************
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    String sqlcmd = "";
                    if (ddlLocation.SelectedItem.Text == "Mary J. Labyak Service Center")
                    {
                        sqlcmd = "SELECT Team FROM OS_CostCenter WHERE Location = 'MJLSC' ORDER BY Team";
                    }
                    if (ddlLocation.SelectedItem.Text == "South Community Service Center")
                    {
                        sqlcmd = "SELECT Team FROM OS_CostCenter WHERE Location = 'SCSC' ORDER BY Team";
                    }
                    if (ddlLocation.SelectedItem.Text == "North Community Service Center")
                    {
                        sqlcmd = "SELECT Team FROM OS_CostCenter WHERE Location = 'NCSC' ORDER BY Team";
                    }
                    ddlBuilding.Items.Clear();
                    SqlCommand cmd = new SqlCommand(sqlcmd, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    int i = 1;
                    ListItem newItem = new ListItem();
                    newItem.Text = "";
                    newItem.Value = "0";
                    ddlBuilding.Items.Add(newItem);

                    while (reader.Read())
                    {
                        newItem = new ListItem();
                        newItem.Text = reader["Team"].ToString();
                        newItem.Value = i++.ToString();
                        ddlBuilding.Items.Add(newItem);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    lblError.Text = "&nbsp;&nbsp;ERROR_02: Unable to read data from Cost Center table! Please contact the Administrator.<br /> SQL ERROR: " + ex.Message + "&nbsp;&nbsp;";
                    //WriteLog("DropDownLists_Load()", lblError.Text);
                }
            }
        }

        protected void InitializeQuantities()
        {
            //initialize all quantities to zero
            for (int i = 1; i < 7; i++)
            {
                TextBox tb = this.FindControl("txt" + i) as TextBox;
                if (tb != null)
                {
                    tb.Text = "0";
                }

            }
        }

        protected int CheckQuantities()
        {
            int QuantityFlag = 0;

            for (int i = 1; i < 7; i++)
            {
                TextBox tb1 = this.FindControl("txt" + i) as TextBox;

                if (tb1 != null)
                {
                    if (tb1.Text != "0" & tb1.Text != "")
                    {
                        QuantityFlag = 1;
                    }
                }
                else
                {
                    lblError.Text = "&nbsp;&nbsp;ERROR_03: Object not found! Please contact the Administrator.";
                }

            }

            if (QuantityFlag == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

            
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
                        SqlCommand cmd = new SqlCommand("SELECT NEXT VALUE For [dbo].HS_Seq", con);
                        con.Open();
                        SeqNo = (Int32)cmd.ExecuteScalar();
                        con.Close();
                        string OrderID = string.Format("{0:00000}", SeqNo);

                        // Select Cost Center from OS_CostCenter table if Location is MJLSC, NCSC, or SCSC
                        if ((ddlLocation.SelectedItem.Text == "Mary J. Labyak Service Center") || (ddlLocation.SelectedItem.Text == "North Community Service Center") || (ddlLocation.SelectedItem.Text == "South Community Service Center"))
                        {
                            String sqlstring = "";
                            if (ddlLocation.SelectedItem.Text == "Mary J. Labyak Service Center")
                            {
                                sqlstring = "SELECT HKCostCenter FROM OS_CostCenter WHERE Location = 'MJLSC' and  Team = '" + ddlBuilding.SelectedItem.Text + "'";
                            }
                            if (ddlLocation.SelectedItem.Text == "North Community Service Center")
                            {
                                sqlstring = "SELECT HKCostCenter FROM OS_CostCenter WHERE Location = 'NCSC' and  Team = '" + ddlBuilding.SelectedItem.Text + "'";
                            }
                            if (ddlLocation.SelectedItem.Text == "South Community Service Center")
                            {
                                sqlstring = "SELECT HKCostCenter FROM OS_CostCenter WHERE Location = 'SCSC' and  Team = '" + ddlBuilding.SelectedItem.Text + "'";
                            }
                            SqlConnection con4 = new SqlConnection(cs);

                            try
                            {
                                SqlCommand cmd4 = new SqlCommand(sqlstring, con4);
                                con4.Open();
                                SqlDataReader reader = cmd4.ExecuteReader();

                                reader.Read();
                                OrderCostCenter = reader["HKCostCenter"].ToString();
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                lblError.Text = "&nbsp;&nbsp;ERROR_04: Unable to read data from OS_CostCenter table! Please contact the Administrator.<br /> SQL ERROR: " + ex.Message + "&nbsp;&nbsp;";
                                //WriteLog("btnSubmit_Click()", lblError.Text);
                            }
                        }

                        // Select Address and Cost Center from OS_Locations table. 
                        try
                        {
                            SqlCommand cmd1 = new SqlCommand("SELECT Address1, Address2, HKCostCenter FROM OS_Locations WHERE Name = '" + ddlLocation.SelectedItem.Text + "'", con);
                            con.Open();
                            SqlDataReader reader = cmd1.ExecuteReader();

                            reader.Read();

                            OrderAddress1 = reader["Address1"].ToString();
                            OrderAddress2 = reader["Address2"].ToString();
                            if (OrderCostCenter == "")
                            {
                                OrderCostCenter = reader["HKCostCenter"].ToString();
                            }
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
                            String sql = "INSERT INTO HS_Order (OrderID,FirstName,LastName,Email,Team,Location,Building,CostCenter,EnteredBy,DateCreated) VALUES (@OrderID,@FirstName,@LastName,@Email,@Team,@Location,@Building,@CostCenter,@EnteredBy,@DateCreated)";
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

                        Subject = "Housekeeping Supplies Order from " + txtFirstName.Text + " " + txtLastName.Text + " | Order ID: " + OrderID;

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
                        if ((ddlLocation.SelectedItem.Text == "North Community Service Center") || (ddlLocation.SelectedItem.Text == "South Community Service Center"))
                        {
                            sbody.Append("<TR><TD width=350><b>Team: </b></TD> <TD>" + ddlBuilding.SelectedItem.Text + "</TD></TR>");
                        }
                        sbody.Append("<TR><TD width=350><b>Account #: </b></TD> <TD width=250> " + "1877993" + "</TD></TR>");
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

                        for (int i = 1; i < 6; i++)
                        {
                            SqlConnection con3 = new SqlConnection(cs);
                            TextBox tb1 = this.FindControl("txt" + i) as TextBox;
                            Label lb1 = this.FindControl("lbl" + i) as Label;
                            String ProdID = lb1.Text.Split('(', ')')[1];


                            if (tb1 != null)
                            {
                                if (tb1.Text != "0" & tb1.Text != "")
                                {
                                    //sbody.Append("<TR><TD width=325><b> " + lb1.Text + "</b></TD> <TD width=80 align=right><b> " + tb1.Text.TrimStart('0') + "</b></TD></TR>");
                                    sbody.Append("<TR><TD width=330><b> " + lb1.Text + "</b></TD> <TD width=60 align=right><b> " + tb1.Text.TrimStart('0') + "</b></TD></TR>");

                                    //Create order detail record in the OS_OrderDetail table
                                    try
                                    {
                                        using (SqlConnection connection = con3)
                                        {
                                            SqlCommand cmd3 = new SqlCommand("INSERT INTO HS_OrderDetail (OrderID,ProductID,Quantity) VALUES (@OrderID,@ProductID,@Quantity)", con3);
                                            cmd3.Parameters.AddWithValue("@OrderID", OrderID);
                                            cmd3.Parameters.AddWithValue("@ProductID", ProdID);
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
                            lblError.Text = "&nbsp;&nbsp;ERROR_08: Unable to send Office Supplies Order email.<br />ERROR: " + ex.Message + "&nbsp; &nbsp;";
                            //WriteLog("SendConfirm()", lblMessage.Text);
                        }

                        InitializeQuantities();
                        lblMessage.Text = "Order has been submitted.";
                    }

                }
                else
                {
                    InitializeQuantities();
                    lblMessage.Text = "";
                    lblError.Text = "&nbsp;&nbsp;ERROR_09: Please complete form before submitting.";
                }
            }
            else
            {
                InitializeQuantities();
                lblMessage.Text = "";
                lblError.Text = "&nbsp;&nbsp;ERROR_10: Session has timed out ... Please re-enter and submit.";
            }
        }


        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedItem.Text == "Mary J. Labyak Service Center" || ddlLocation.SelectedItem.Text == "South Community Service Center" || ddlLocation.SelectedItem.Text == "North Community Service Center")
            {
                BuildingDDL_Load();
            }
        }

    }

}
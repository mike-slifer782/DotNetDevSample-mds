using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
       // DataSet ds = new DataSet();
       // Connection String should be encrypted in web.config not plaintext here
        SqlConnection con = new SqlConnection("Server=DESKTOP-DUSQID8/SQLEXPRESS,Database=DotNetDevSample;");
        SqlCommand cmd = new SqlCommand();        
        cmd.Parameters.Add("@v_WidgetID", SqlDbType.Int).Value = 0;
        cmd.Parameters.Add("@v_InventoryCode", SqlDbType.VarChar).Value = "    ";
        cmd.Parameters.Add("@v_Description", SqlDbType.VarChar).Value = "    ";
        cmd.Parameters.Add("@v_QuantityOnHand", SqlDbType.Int).Value = 0;
        cmd.Parameters.Add("@v_ReorderQuantity", SqlDbType.Int).Value = 0;
        cmd.CommandText = "dbo.uscRetrieveListIfRowsInWidgetTable";
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable table = new DataTable();
        da.Fill(table);
        WidgetGridView.DataSource = table;
        WidgetGridView.DataBind();

    }
    protected void WidgetGridView_RowCommand(object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        string currentCommand = e.CommandName;
        int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
        int WidgetID = (int)WidgetGridView.DataKeys[currentRowIndex].Value;
        Label Label1 = new Label();
        Label Label2 = new Label();
        Label Label3 = new Label();
        Label1.Text = "Command: "   + currentCommand;
        Label2.Text = "Row Index: " + currentRowIndex;
        Label3.Text = "Widget ID: " + WidgetID;

    }

    protected void WidgetGridView_RowEditing(object sender,
        System.Web.UI.WebControls.GridViewCommandEventArgs   e)
    {
        WidgetGridView.EditIndex = 2;
        BindData();
    }

    protected void WidgetGridView_RowCancelEditing(object sender,
        System.Web.UI.WebControls.GridViewCancelEditEventArgs e)   
    {
        e.Cancel = true;
        WidgetGridView.EditIndex = -1;
        BindData();
    }

    protected void WidgetGridView_RowUpdating(object sender,
        System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        GridViewRow row = WidgetGridView.Rows[e.RowIndex];
        
        TextBox txtInventoryCode = (TextBox)row.FindControl("txtInventoryCode");
        TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
        TextBox txtQuantityOnHand = (TextBox)row.FindControl("txtQuantityOnHand");
        TextBox txtReorderQuantity = (TextBox)row.FindControl("txtReorderQuantity");

        int WidgetID = Int32.Parse(WidgetGridView.DataKeys[e.RowIndex].Value.ToString());

        string InventoryCode = txtInventoryCode.Text;
        string Description = txtDescription.Text;
        int QuantityOnHand = Int32.Parse(txtQuantityOnHand.Text);
        int ReorderQuantity  = Int32.Parse(txtReorderQuantity.Text);
        UpdateWidget(WidgetID, InventoryCode, Description, QuantityOnHand, ReorderQuantity);
    }


    private void InsertWidget(int WidgetID, string InventoryCode, string Description, int QuantityOnHand, int ReorderQuantity)
    {
        try
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-DUSQID8/SQLEXPRESS,Database=DotNetDevSample;");
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@v_WidgetID", SqlDbType.Int).Value = WidgetID;
            cmd.Parameters.Add("@v_InventoryCode", SqlDbType.VarChar).Value = InventoryCode;
            cmd.Parameters.Add("@v_Description", SqlDbType.VarChar).Value = Description;
            cmd.Parameters.Add("@v_QuantityOnHand", SqlDbType.Int).Value = QuantityOnHand;
            cmd.Parameters.Add("@v_ReorderQuantity", SqlDbType.Int).Value = ReorderQuantity;
            cmd.CommandText = "dbo.uscSaveANewOrModifiedWidget";
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable table = new DataTable();
            //da.Fill(table);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateWidget(int WidgetID, string InventoryCode, string Description, int QuantityOnHand, int ReorderQuantity)
    {
        try
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-DUSQID8/SQLEXPRESS,Database=DotNetDevSample;");
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@v_WidgetID", SqlDbType.Int).Value = WidgetID;
            cmd.Parameters.Add("@v_InventoryCode", SqlDbType.VarChar).Value = InventoryCode;
            cmd.Parameters.Add("@v_Description", SqlDbType.VarChar).Value = Description;
            cmd.Parameters.Add("@v_QuantityOnHand", SqlDbType.Int).Value = QuantityOnHand;
            cmd.Parameters.Add("@v_ReorderQuantity", SqlDbType.Int).Value = ReorderQuantity;
            cmd.CommandText = "dbo.uscSaveANewOrModifiedWidget";
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void WidgetGridView_RowDeleting(object sender,
     System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        GridViewRow row = WidgetGridView.Rows[e.RowIndex];

        int WidgetID = Int32.Parse(WidgetGridView.DataKeys[e.RowIndex].Value.ToString());

        DeleteWidget(WidgetID);
    }

    private void DeleteWidget(int WidgetID)
    {
        try
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-DUSQID8/SQLEXPRESS,Database=DotNetDevSample;");
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@v_WidgetID", SqlDbType.Int).Value = WidgetID;
            cmd.CommandText = "dbo.uscDeleteAWidget";
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
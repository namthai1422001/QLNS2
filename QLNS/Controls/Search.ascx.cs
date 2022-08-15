using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLNS.Controls
{
    public partial class Search : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string _query = "Search@";

            _query += new DeEncodeBase64().EncodeTo64(txtSearch.Text);
            Response.Redirect(_query);
        }
    }
}
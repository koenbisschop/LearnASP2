﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LedenbeheerDomain.Business;

namespace Ledenbeheer
{
    public partial class Dropdown : System.Web.UI.Page
    {
        Controller c;
        private List<Lid> LedenLijst { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            c = (Controller)Session["controller"];
            LedenLijst = c.GetLeden();
            if (!IsPostBack)
            {
                ddlLeden.DataSource = LedenLijst;
                ddlLeden.DataTextField = "Naam";
                ddlLeden.DataValueField = "Id";
                ddlLeden.DataBind();
            }
            ToonInfo();
        }

        protected void ToonInfo(Int32 id=0)
        {
            Lid lid = c.GetLid(id); //of beter: id!=0 ? c.GetLid(id) : null;
            if (lid != null)
            {
                grvBijdragenLid.DataSource = lid.Bijdragen;
                grvBijdragenLid.DataBind();
                lblInfo.Text = lid.ToString();
            }
            else
            {
                grvBijdragenLid.DataSource = null;
                lblInfo.Text = "";
            }
        }
        protected void btnTerug_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void ddlLeden_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToonInfo(Convert.ToInt32(ddlLeden.SelectedValue));
        }
    }
}
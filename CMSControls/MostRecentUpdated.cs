﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Carrotware.CMS.Core;
using Carrotware.CMS.Interface;
using Carrotware.Web.UI.Controls;
/*
* CarrotCake CMS
* http://www.carrotware.com/
*
* Copyright 2011, Samantha Copeland
* Dual licensed under the MIT or GPL Version 2 licenses.
*
* Date: October 2011
*/

namespace Carrotware.CMS.UI.Controls {

	[ToolboxData("<{0}:MostRecentUpdated runat=server></{0}:MostRecentUpdated>")]
	public class MostRecentUpdated : BaseNavHeaded, IWidgetLimitedProperties {

		public enum ListContentType {
			Unknown,
			Blog,
			ContentPage,
			SpecifiedCategories
		}

		[Widget(WidgetAttribute.FieldMode.DictionaryList)]
		public Dictionary<string, string> lstContentType {
			get {
				Dictionary<string, string> _dict = new Dictionary<string, string>();
				_dict.Add("Blog", "Blog");
				_dict.Add("ContentPage", "Content Page");
				_dict.Add("SpecifiedCategories", "Specified Categories");
				return _dict;
			}
		}

		[Category("Appearance")]
		[DefaultValue("ContentPage")]
		[Widget(WidgetAttribute.FieldMode.DropDownList, "lstContentType")]
		public ListContentType ContentType {
			get {
				String s = (String)ViewState["ContentType"];
				ListContentType c = ListContentType.ContentPage;
				if (!string.IsNullOrEmpty(s)) {
					c = (ListContentType)Enum.Parse(typeof(ListContentType), s, true);
				}
				return c;
			}

			set {
				ViewState["ContentType"] = value.ToString();
			}
		}

		[Category("Appearance")]
		[DefaultValue(5)]
		public int TakeTop {
			get {
				String s = (String)ViewState["TakeTop"];
				return ((s == null) ? 5 : int.Parse(s));
			}
			set {
				ViewState["TakeTop"] = value.ToString();
			}
		}

		private List<GuidItem> guidList = null;
		[
		Category("Behavior"),
		Description("The GuidItem collection"),
		Browsable(false),
		DefaultValue(null),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Editor(typeof(GuidItemCollectionEditor), typeof(UITypeEditor)),
		NotifyParentProperty(true),
		TemplateContainer(typeof(GuidItem)),
		PersistenceMode(PersistenceMode.InnerProperty)
		]
		public List<GuidItem> CategoryGuidList {
			get {
				if (guidList == null) {
					guidList = new List<GuidItem>();
				}
				return guidList;
			}
		}

		private List<Guid> _guids = null;

		[Widget(WidgetAttribute.FieldMode.CheckBoxList, "lstCategories")]
		public List<Guid> SelectedCategories {
			get {
				if (_guids == null) {
					if (CategoryGuidList.Count > 0) {
						_guids = (from n in CategoryGuidList select n.GuidValue).ToList();
					} else {
						_guids = new List<Guid>();
					}
				}
				return _guids;
			}
			set {
				_guids = value;
			}
		}

		[Widget(WidgetAttribute.FieldMode.DictionaryList)]
		public Dictionary<string, string> lstCategories {
			get {

				Dictionary<string, string> _dict = (from c in SiteData.CurrentSite.GetCategoryList()
													orderby c.CategoryText
													where c.SiteID == SiteData.CurrentSiteID
													select c).ToList().ToDictionary(k => k.ContentCategoryID.ToString(), v => v.CategoryText + " (" + v.CategorySlug + ")");

				return _dict;
			}
		}

		protected override void LoadData() {
			base.LoadData();

			this.NavigationData = GetUpdates();
		}

		protected List<SiteNav> GetUpdates() {

			switch (ContentType) {
				case ListContentType.Blog:
					return navHelper.GetLatestPosts(SiteData.CurrentSiteID, TakeTop, !SecurityData.IsAuthEditor);
				case ListContentType.ContentPage:
					return navHelper.GetLatest(SiteData.CurrentSiteID, TakeTop, !SecurityData.IsAuthEditor);
				case ListContentType.SpecifiedCategories:
					if (TakeTop > 0) {
						return navHelper.GetFilteredContentByIDPagedList(SiteData.CurrentSite, SelectedCategories, !SecurityData.IsAuthEditor, TakeTop, 0, "GoLiveDate", "DESC");
					} else {
						return navHelper.GetFilteredContentByIDPagedList(SiteData.CurrentSite, SelectedCategories, !SecurityData.IsAuthEditor, 100000, 0, "NavMenuText", "ASC");
					}
			}

			return new List<SiteNav>();
		}

		public List<string> LimitedPropertyList {
			get {
				List<string> lst = new List<string>();
				lst.Add("TakeTop");
				lst.Add("MetaDataTitle");
				lst.Add("CssClass");
				lst.Add("EnableViewState");
				lst.Add("ContentType");
				lst.Add("SelectedCategories");
				return lst;
			}
		}

		protected override void OnPreRender(EventArgs e) {

			base.OnPreRender(e);

			try {

				if (PublicParmValues.Count > 0) {

					TakeTop = int.Parse(GetParmValue("TakeTop", "5"));

					ContentType = (ListContentType)Enum.Parse(typeof(ListContentType), GetParmValue("ContentType", "Blog"), true);

					SelectedCategories = new List<Guid>();

					List<string> lstCategories = GetParmValueList("SelectedCategories");
					foreach (string sCat in lstCategories) {
						if (!string.IsNullOrEmpty(sCat)) {
							SelectedCategories.Add(new Guid(sCat));
						}
					}
				}
				if (SelectedCategories.Count > 0) {
					ContentType = ListContentType.SpecifiedCategories;
				}
			} catch (Exception ex) {
			}
		}
	}
}

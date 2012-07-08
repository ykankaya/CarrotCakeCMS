﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.Caching;

/*
* CarrotCake CMS
* http://carrotware.com/
*
* Copyright 2011, Samantha Copeland
* Dual licensed under the MIT or GPL Version 2 licenses.
*
* Date: October 2011
*/

namespace Carrotware.CMS.Core {

	public class UserEditState {

		public UserEditState() { }

		public string EditorMargin { get; set; }

		public string EditorOpen { get; set; }

		public string EditorScrollPosition { get; set; }


		public static string ContentKey {
			get {
				return "cms_UserEditState_" + SiteData.CurrentUser.UserName.ToLower();
			}
		}

		//cache the settings but only long enough for the page to save & refresh
		public static UserEditState cmsUserEditState {
			get {
				UserEditState c = null;
				try { c = (UserEditState)HttpContext.Current.Cache[ContentKey]; } catch { }
				return c;
			}
			set {
				if (value == null) {
					HttpContext.Current.Cache.Remove(ContentKey);
				} else {
					HttpContext.Current.Cache.Insert(ContentKey, value, null, DateTime.Now.AddSeconds(15), Cache.NoSlidingExpiration);
				}
			}
		}


	}
}

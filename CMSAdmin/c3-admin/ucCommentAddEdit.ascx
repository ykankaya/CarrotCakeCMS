﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCommentAddEdit.ascx.cs" Inherits="Carrotware.CMS.UI.Admin.c3_admin.ucCommentAddEdit" %>
<fieldset style="width: 360px;">
	<legend>
		<label>
			Page Info
		</label>
	</legend>
	<table width="700">
		<tr>
			<td width="125" valign="top" class="tablecaption">
				title:
			</td>
			<td valign="top">
				<asp:Label ID="lblTitle" runat="server" />
			</td>
		</tr>
		<tr>
			<td valign="top" class="tablecaption">
				filename:
			</td>
			<td valign="top">
				<asp:Label ID="lblFile" runat="server" />
			</td>
		</tr>
	</table>
</fieldset>
<fieldset style="width: 360px;">
	<legend>
		<label>
			Comment
		</label>
	</legend>
	<table width="700">
		<tr>
			<tr>
				<td width="125" valign="top" class="tablecaption">
					IP address:
				</td>
				<td valign="top">
					<asp:Label ID="lblIP" runat="server" />
				</td>
			</tr>
			<td valign="top" class="tablecaption">
				email:
			</td>
			<td valign="top">
				<asp:TextBox ValidationGroup="inputForm" onkeypress="return ProcessKeyPress(event)" onblur="CheckFileName()" ID="txtEmail" runat="server" Columns="60"
					MaxLength="200" />
				<asp:RequiredFieldValidator ValidationGroup="inputForm" ControlToValidate="txtEmail" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
					Display="Dynamic" />
				<asp:TextBox runat="server" ValidationGroup="inputForm" ID="txtFileValid" MaxLength="25" Columns="25" Style="display: none;" />
			</td>
		</tr>
		<tr>
			<td valign="top" class="tablecaption">
				name:
			</td>
			<td valign="top">
				<asp:TextBox ValidationGroup="inputForm" onkeypress="return ProcessKeyPress(event)" ID="txtName" runat="server" Columns="60" MaxLength="200" />
				<asp:RequiredFieldValidator ValidationGroup="inputForm" ControlToValidate="txtName" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required"
					Display="Dynamic" />
			</td>
		</tr>
		<tr>
			<td valign="top" class="tablecaption">
				url:
			</td>
			<td valign="top">
				<asp:TextBox ValidationGroup="inputForm" onkeypress="return ProcessKeyPress(event)" ID="txtURL" runat="server" Columns="60" MaxLength="200" />
			</td>
		</tr>
		<tr>
			<td valign="top" class="tablecaption">
				&nbsp;
			</td>
			<td valign="top">
				<div class="table-subblock">
					<b class="caption">approved:</b>&nbsp;&nbsp;
					<asp:CheckBox ID="chkApproved" runat="server" />
				</div>
				<div class="table-subblock">
					<b class="caption">spam:</b>&nbsp;&nbsp;
					<asp:CheckBox ID="chkSpam" runat="server" />
				</div>
			</td>
		</tr>
		<tr>
			<td valign="top" class="tablecaption">
				comment:
			</td>
			<td valign="top">
				<asp:TextBox Style="height: 250px; width: 600px;" ID="txtComment" runat="server" TextMode="MultiLine" Rows="15" Columns="80" />
			</td>
		</tr>
	</table>
</fieldset>
<asp:Button ValidationGroup="inputForm" ID="btnSaveButton" runat="server" OnClientClick="SubmitPage()" Text="Save" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input type="button" id="btnCancel" value="Cancel" onclick="cancelEditing()" />
<br />
<div style="display: none;">
	<asp:Button ValidationGroup="inputForm" ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
</div>
<script type="text/javascript">

	function SubmitPage() {
		setTimeout("ClickBtn();", 250);
	}
	function ClickBtn() {
		$('#<%=btnSave.ClientID %>').click();
	}

	function cancelEditing() {
		window.setTimeout("location.href = './<%=ReturnPage %>?<%=ReturnPageQueryString %>';", 250);
	}

</script>

<%@ Import Namespace="Sitecore.Analytics"%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Analysis.aspx.cs" Inherits="Andover.UI.Analysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
	<style type="text/css">
		.block 
		{
		    max-width: 960px;
		    margin: 0 auto;
		    display: block;
		}

        .category {
            background-color: #778899;
		    color: #ffffff;
            padding: 10px;
            margin: 0 0 5px 0;
        }

		.component {
		    background-color: #a9a9a9;
		    color: #ffffff;
            padding: 10px;
		    margin: 0 0 5px 0;
		}

		.result {
		    background-color: #C0C0C0;
		    color: #ffffff;
		}

        table {
            margin: 0 0 5px 0;
        }
	</style>
</head>
<body>
    <form id="form1" runat="server">
    
        <div class="block">
            
    <h2>
    <asp:Label runat="server" ID="lblSummary"></asp:Label>
    </h2>
    
    <asp:GridView runat="server" ID="gvSummary" AutoGenerateColumns="false" Width="100%">
	    <HeaderStyle BackColor="#E6DDFF"></HeaderStyle>
        <Columns>
            <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="true"/>
            <asp:BoundField DataField="Component" HeaderText="Component" ReadOnly="true"/>
            <asp:BoundField DataField="Compliant" HeaderText="Compliant" ReadOnly="true"/> 
        </Columns>
    </asp:GridView>
            
    <h2>Report Details</h2>
    <asp:Repeater runat="server" ID="rCategories" OnItemDataBound="rCategories_OnItemDataBound">
		<ItemTemplate>
		    <div class="category">
		        <h1><asp:Literal runat="server" ID="litCategory"></asp:Literal></h1>
                <asp:Literal runat="server" ID="litDescription"></asp:Literal>
            </div>

            <asp:Repeater runat="server" ID="rComponents" OnItemDataBound="rComponents_OnItemDataBound">
		        <ItemTemplate>
		            <div class="component">
		                <h2><asp:Literal runat="server" ID="litComponent"></asp:Literal></h2>
                        <asp:Literal runat="server" ID="litDescription"></asp:Literal>
		            </div>
            
                    <asp:Repeater runat="server" ID="rComponentResults" OnItemDataBound="rComponentsResults_OnItemDataBound">
		                <ItemTemplate>
		                    <asp:GridView runat="server" ID="gvResults" AutoGenerateColumns="false" Width="100%">
		                        <HeaderStyle BackColor="#E6DDFF"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="true" ItemStyle-Width="150px"/>
                                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="true" ItemStyle-Width="440"/> 
                                    <asp:BoundField DataField="Value" HeaderText="Value" ReadOnly="true" ItemStyle-Width="350"/> 
                                </Columns>
		                    </asp:GridView>
		                </ItemTemplate>
		                <FooterTemplate>
		                </FooterTemplate>
                    </asp:Repeater>             <!-- rComponentsResults -->

		        </ItemTemplate>
		        <FooterTemplate>
		        </FooterTemplate>
            </asp:Repeater>             <!--- rComponents -->

		</ItemTemplate>
		<FooterTemplate>
		</FooterTemplate>
    </asp:Repeater>             <!-- rCategories -->
        </div>
    </form>
</body>
</html>

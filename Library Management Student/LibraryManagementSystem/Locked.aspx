<%@ Page Title="" Language="C#" MasterPageFile="~/Library.Master" AutoEventWireup="true" CodeBehind="Locked.aspx.cs" Inherits="LibraryManagementSystem.Locked" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="col s12 m9 l9">

        <div class="card">
             <div class="card-content">
                <span class="card-title">Locked Accounts</span>

                  <asp:GridView runat="server" ID="gvLocked" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Department" HeaderText="Department" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="IsActive" HeaderText="IsActive" />
                       
                        </Columns>
                    </asp:GridView>
                </div>
        </div>
          
            
         </div>
    
</asp:Content>

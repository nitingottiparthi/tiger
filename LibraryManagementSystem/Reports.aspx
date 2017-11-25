<%@ Page Title="" Language="C#" MasterPageFile="~/Library.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="LibraryManagementSystem.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="col s12 m9 l9">

        <div class="card">
             <div class="card-content">
                 <div style="float:right">
                     <asp:ImageButton runat="server" ID="btnExcel" ImageUrl="~/Img/excel.png" Width="32px" Height="32px" OnClick="btnExcel_Click" />
                     <asp:ImageButton runat="server" ID="btnWord" ImageUrl="~/Img/word.png" Width="32px" Height="32px" OnClick="btnWord_Click" />
                 </div>
                <span class="card-title">Reports</span>

                       <asp:GridView runat="server" ID="gvBooks" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="BookId" HeaderText="Id" />
                        <asp:BoundField DataField="BookName" HeaderText="Name" />
                        <asp:BoundField DataField="BookCategory" HeaderText="Category" />
                        <asp:BoundField DataField="BookPrice" HeaderText="Price" />
                        <asp:BoundField DataField="BookQty" HeaderText="Qty" />
                        <asp:BoundField DataField="BookRack" HeaderText="Rack" />
                       
                    </Columns>
                </asp:GridView>

                 </div>
            </div>
          </div>
    
</asp:Content>

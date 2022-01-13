<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="carga.aspx.cs" Inherits="Datos.carga" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script language=Javascript>
          <!--
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57 || charCode == 47 ))
                return false;

            return true;
        }
          //-->
    </script>
    
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    
    <title>Carga Manual</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">

</head>
<body>
    <div id="wrapper">
    <nav class="navbar-default navbar-static-side" role="navigation">
        <div class="sidebar-collapse">
            <ul class="nav metismenu" id="side-menu">
                <li class="nav-header">
                    <div class="dropdown profile-element">
                         <img alt="image" src="img/bat.png"/>
                    </div>
                    <div class="logo-element">
                        FTEM
                    </div>
                </li>
                 <li><a href="index.aspx"><i class="fa fa-table"></i> <span class="nav-label">Datos</span></a></li>
                 <li><a href="bd.aspx?Id=1"><i class="fa fa-area-chart"></i> <span class="nav-label">Indicadores</span></a></li>
                 <li><a href="dds.aspx?Id=1"><i class="fa fa-tachometer"></i> <span class="nav-label">DDS</span></a></li>
                <li class="active">
                    <a href="carga.aspx?Id=1"><i class="fa fa-upload"></i> <span class="nav-label">Carga Manual</span></a>
                </li>
                <li><a href="targets.aspx"><i class="fa fa-bullseye"></i> <span class="nav-label">Targets</span></a></li>
            </ul>

        </div>
    </nav>

    <div id="page-wrapper" class="gray-bg dashbard-1">
        <div class="wrapper wrapper-content ">
            <div class="row">
                <div class="col-md-12">
                    <div class="ibox ">
                        <div class="ibox-title">
                            <h5>Enercon Dashboard 3.0 - Carga Manual</h5>
                            <div class="float-right">
                                <div class="btn-group">
                                    <asp:Panel ID="PanelBtn" runat="server"></asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">


    <form id="form1" runat="server">
        <div>

                <asp:GridView ID="gvDatos" onrowdatabound="gvDatos_RowDataBound" runat="server" AutoGenerateColumns="False" DataKeyNames="IndicadorId"
                ShowHeaderWhenEmpty="True"

                OnRowCommand="gvDatos_RowCommand" OnRowEditing="gvDatos_RowEditing" OnRowCancelingEdit="gvDatos_RowCancelingEdit"
                OnRowUpdating="gvDatos_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None">
                <%-- Theme Properties --%>
                
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                <Columns>
                    <asp:TemplateField HeaderText="OrdenArea">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("OrdenArea") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Area">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Area") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OrdenInd">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("OrdenInd") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IndicadorId">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("IndicadorId") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Indicador">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Indicador") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unidad">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Unidad") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monday">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Monday") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width="100" ID="txtMonday" Text='<%# Eval("Monday") %>' runat="server"  onkeypress="return isNumberKey(event)" type="text"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tuesday">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Tuesday") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width="100" ID="txtTuesday" Text='<%# Eval("Tuesday") %>' runat="server"  onkeypress="return isNumberKey(event)" type="text"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wednesday">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Wednesday") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width="100" ID="txtWednesday" Text='<%# Eval("Wednesday") %>' runat="server"  onkeypress="return isNumberKey(event)" type="text"/>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Thursday">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Thursday") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width="100" ID="txtThursday" Text='<%# Eval("Thursday") %>' runat="server"  onkeypress="return isNumberKey(event)" type="text"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Friday">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Friday") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width="100" ID="txtFriday" Text='<%# Eval("Friday") %>' runat="server"  onkeypress="return isNumberKey(event)" type="text"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Saturday">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Saturday") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width="100" ID="txtSaturday" Text='<%# Eval("Saturday") %>' runat="server"  onkeypress="return isNumberKey(event)" type="text"/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sunday">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Sunday") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width="100" ID="txtSunday" Text='<%# Eval("Sunday") %>' runat="server" onkeypress="return isNumberKey(event)" type="text" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="24px" Height="24px"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="24px" Height="24px"/>
                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="24px" Height="24px"/>
                        </EditItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblSuccessMessage" Text="" runat="server" ForeColor="Green" />
            <br />
            <asp:Label ID="lblErrorMessage" Text="" runat="server" ForeColor="Red" />

        </div>
    </form>
    
                                                                    </div>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    --<script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>

    <script src="js/plugins/dataTables/datatables.min.js"></script>
    <script src="js/plugins/dataTables/dataTables.bootstrap4.min.js"></script>
        <script>
            function clickAnt() {
                location.href = 'carga.aspx?Id=0';
            }
            function clickAct() {
                location.href = 'carga.aspx?Id=1';
            }

        </script>

</body>
</html>

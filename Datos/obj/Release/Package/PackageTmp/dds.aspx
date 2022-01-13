<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dds.aspx.cs" Inherits="Datos.dds" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>


<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="dds.aspx.cs" Inherits="Datos.dds" %>

<!DOCTYPE html>
<html>
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    
    <title>DDS</title>

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
                 <li class="active"><a href="dds.aspx?Id=1"><i class="fa fa-tachometer"></i> <span class="nav-label">DDS</span></a></li>
                <li>
                    <a href="carga.aspx?Id=1"><i class="fa fa-upload"></i> <span class="nav-label">Carga Manual</span></a>
                </li>
                <li><a href="targets.aspx"><i class="fa fa-bullseye"></i> <span class="nav-label">Targets</span></a></li>
            </ul>

        </div>
    </nav>

    <div id="page-wrapper" class="gray-bg dashbard-1">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-md-12">
                    <div class="ibox ">
                        <div class="ibox-title">
                            <h5>Enercon Dashboard 3.0 - DDS</h5>
                            <div class="float-right">
                                <div class="btn-group">
                                    <asp:Panel ID="PanelBtn" runat="server"></asp:Panel>
                                </div>
                            </div>

                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-md-12">
                                    <table id="t01"  >
                                        <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                                    </table>
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
                location.href = 'dds.aspx?Id=0';
        }
        function clickAct() {
            location.href = 'dds.aspx?Id=1';
        }

        </script>

</body>
</html>

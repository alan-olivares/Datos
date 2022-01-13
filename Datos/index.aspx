<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Datos.index" %>
<!DOCTYPE html>
<html>

<head><meta charset="utf-8" /><meta name="viewport" content="width=device-width, initial-scale=1.0" /><title>
	Datos
</title><link href="css/bootstrap.min.css" rel="stylesheet" /><link href="font-awesome/css/font-awesome.css" rel="stylesheet" /><link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet" /><link href="css/animate.css" rel="stylesheet" /><link href="css/style.css" rel="stylesheet" />
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
                <li><a href="index.html"><i class="fa fa-table"></i> <span class="nav-label">Graficas</span></a></li>
                 <li class="active"><a href="index.aspx"><i class="fa fa-table"></i> <span class="nav-label">Datos</span></a></li>
                 <li><a href="bd.aspx"><i class="fa fa-area-chart"></i> <span class="nav-label">Indicadores</span></a></li>
                 <li><a href="dds.aspx"><i class="fa fa-tachometer"></i> <span class="nav-label">DDS</span></a></li>
                <li><a href="carga.aspx"><i class="fa fa-upload"></i> <span class="nav-label">Carga Manual</span></a></li>
                <li><a href="targets.html"><i class="fa fa-bullseye"></i> <span class="nav-label">Targets</span></a></li>


            </ul>

        </div>
    </nav>
    

        <div id="page-wrapper" class="gray-bg">

            <div class="page-content">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                <div class="ibox ">
                    <div class="ibox-title" style="height:100px;">
                        <h5>Enercon Dashboard 3.0 - Datos por Área</h5>
                        <div class="float-right">
                                <div class="form-inline text-center d-flex justify-content-center">
                                    <h5 style="margin-right:20px;"><label class="">Fecha:</label>
                                        <input type="week" class="form-control" runat="server" id="date1" onchange="obtenerDatos()" onkeydown="return false">
                                    </h5>
                                </div>
                            </div>
                    </div>
                    <div class="ibox-content">
                        
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover dataTables-example2" >
                            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                        </table>
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
    

    <script src="js/plugins/dataTables/datatables.min.js"></script>
    <script src="js/plugins/dataTables/dataTables.bootstrap4.min.js"></script>

        <!-- Page-Level Scripts -->
    <script>
        $(document).ready(function () {
            $('.dataTables-example2').DataTable({
                pageLength: 25,
                responsive: true,
                dom: '<"html5buttons"B>lTfgitp',
                buttons: [
                    {
                        extend: 'copy',
                        text: 'Copiar'
                    },
                    { extend: 'csv' },
                    { extend: 'excel', title: 'Datos' }
                ],
                "language": {
                    "decimal": "",
                    "emptyTable": "No hay datos",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                    "infoEmpty": "Mostrando 0 a 0 de 0 registros",
                    "infoFiltered": "(filtrando de _MAX_ registros totales)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "lengthMenu": "Mostrar _MENU_ registros",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "No se encontraron registros",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                    "aria": {
                        "sortAscending": ": activar para ordenar columna ascendente",
                        "sortDescending": ": activar para ordenar columna descendiente"
                    },
                    buttons: {
                        copySuccess: {
                            copyTitle: "Copiar al portapapeles",
                            _: '%d registros copiados',
                            1: '1 registro copiado'
                        }
                    }
                }
            });

        });

    </script>
        <div class="footer">
        <div class="float-right">
                 <strong>British American Tobacco México</strong>
            </div>
        </div>
    </div>
    </div>
    
        

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <script src="js/plugins/dataTables/datatables.min.js"></script>
    <script src="js/plugins/dataTables/dataTables.bootstrap4.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script>
        function obtenerDatos() {
            location.href = 'index.aspx?fecha=' + $('#date1').val();
        }
    </script>



</body>

</html>
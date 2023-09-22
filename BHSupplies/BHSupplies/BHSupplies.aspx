<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/BHSupplies.aspx.cs" Inherits="BHSupplies.BHSupplies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Building Housekeeping Supplies</title>
  <meta charset="utf-8" />
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.3/jquery-ui.js"></script>
  <script src='//code.jquery.com/jquery-latest.min.js' type='text/javascript'></script>
  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.3/themes/smoothness/jquery-ui.css" />
  <link rel="stylesheet" type="text/css" href="Style.css"/>
  <link href="Images/favicon.ico" type="image/x-icon" rel="icon"/>

<script>
    $(document).ready(function () {

        //Load page with appropriate fields displayed
        if ($("#ddlLocation option:selected").text() == "Mary J. Labyak Service Center") {
            $('#ddlBuilding').prop('disabled', false);
        }
        else {
            $('#ddlBuilding').val('0');
            $('#ddlBuilding').prop('disabled', true);
        }

        //Select proper fields based on location
        $('#ddlLocation').change(function () {

            if ($("#ddlLocation option:selected").text() == "Mary J. Labyak Service Center") {
                $('#ddlBuilding').prop('disabled', false);
                $("#ddlBuilding").val('0');
            }
            else {
                $('#ddlBuilding').val('0');
                $('#ddlBuilding').prop('disabled', true);
            }
        });

        $('#cbBags').change(function () {
            if ($('#cbBags').prop("checked")) {
                ClearCheckBoxes();
                $('#cbBags').prop("checked", true);
                $('#divBags').css("display", "block");
            }
            else {
                $('#divBags').css("display", "none");
            }
        });

        if ($('#cbBags').prop("checked")) {
            $('#divBags').css("display", "block");
        }
        else {
            $('#divBags').css("display", "none");
        }

        $('#cbCleaners').change(function () {
            if ($('#cbCleaners').prop("checked")) {
                ClearCheckBoxes();
                $('#cbCleaners').prop("checked", true);
                $('#divCleaners').css("display", "block");
            }
            else {
                $('#divCleaners').css("display", "none");
            }
        });

        if ($('#cbCleaners').prop("checked")) {
            $('#divCleaners').css("display", "block");
        }
        else {
            $('#divCleaners').css("display", "none");
        }

        $('#cbKitchen').change(function () {
            if ($('#cbKitchen').prop("checked")) {
                ClearCheckBoxes();
                $('#cbKitchen').prop("checked", true);
                $('#divKitchen').css("display", "block");
            }
            else {
                $('#divKitchen').css("display", "none");
            }
        });

        if ($('#cbKitchen').prop("checked")) {
            $('#divKitchen').css("display", "block");
        }
        else {
            $('#divKitchen').css("display", "none");
        }

        $('#cbLaundry').change(function () {
            if ($('#cbLaundry').prop("checked")) {
                ClearCheckBoxes();
                $('#cbLaundry').prop("checked", true);
                $('#divLaundry').css("display", "block");
            }
            else {
                $('#divLaundry').css("display", "none");
            }
        });

        if ($('#cbLaundry').prop("checked")) {
            $('#divLaundry').css("display", "block");
        }
        else {
            $('#divLaundry').css("display", "none");
        }

        $('#cbLiners').change(function () {
            if ($('#cbLiners').prop("checked")) {
                ClearCheckBoxes();
                $('#cbLiners').prop("checked", true);
                $('#divLiners').css("display", "block");
            }
            else {
                $('#divLiners').css("display", "none");
            }
        });

        if ($('#cbLiners').prop("checked")) {
            $('#divLiners').css("display", "block");
        }
        else {
            $('#divLiners').css("display", "none");
        }

        $('#cbPaperGoods').change(function () {
            if ($('#cbPaperGoods').prop("checked")) {
                ClearCheckBoxes();
                $('#cbPaperGoods').prop("checked", true);
                $('#divPaperGoods').css("display", "block");
            }
            else {
                $('#divPaperGoods').css("display", "none");
            }
        });

        if ($('#cbPaperGoods').prop("checked")) {
            $('#divPaperGoods').css("display", "block");
        }
        else {
            $('#divPaperGoods').css("display", "none");
        }

        $('#cbSoap').change(function () {
            if ($('#cbSoap').prop("checked")) {
                ClearCheckBoxes();
                $('#cbSoap').prop("checked", true);
                $('#divSoap').css("display", "block");
            }
            else {
                $('#divSoap').css("display", "none");
            }
        });

        if ($('#cbSoap').prop("checked")) {
            $('#divSoap').css("display", "block");
        }
        else {
            $('#divSoap').css("display", "none");
        }

        $('#cbWater').change(function () {
            if ($('#cbWater').prop("checked")) {
                ClearCheckBoxes();
                $('#cbWater').prop("checked", true);
                $('#divWater').css("display", "block");
            }
            else {
                $('#divWater').css("display", "none");
            }
        });

        if ($('#cbWater').prop("checked")) {
            $('#divWater').css("display", "block");
        }
        else {
            $('#divWater').css("display", "none");
        }

        function ClearCheckBoxes() {
            $('#cbBags').removeAttr('checked');
            $('#divBags').css("display", "none");
            $('#cbCleaners').removeAttr('checked');
            $('#divCleaners').css("display", "none");
            $('#cbKitchen').removeAttr('checked');
            $('#divKitchen').css("display", "none");
            $('#cbLaundry').removeAttr('checked');
            $('#divLaundry').css("display", "none");
            $('#cbLiners').removeAttr('checked');
            $('#divLiners').css("display", "none");
            $('#cbPaperGoods').removeAttr('checked');
            $('#divPaperGoods').css("display", "none");
            $('#cbSoap').removeAttr('checked');
            $('#divSoap').css("display", "none");
            $('#cbWater').removeAttr('checked');
            $('#divWater').css("display", "none");
        }

    });

    //-- Perform validation on required fields
    $(function () {
        $("#btnSubmit").click(function () {
            document.getElementById('lblError').textContent = '';
            if ($("#txtFirstName").val().length == 0) {
                alert('WARNING:\nFirst Name is required!');
                return false;
            }
            else {
                if ($("#txtLastName").val().length == 0) {
                    alert('WARNING:\nLast Name is required!');
                    return false;
                }
                else {
                    if ($("#txtEmail").val().length == 0) {
                        alert('WARNING:\nEmail is required!');
                        return false;
                    }
                    else {
                        if ($("#txtTeam").val().length == 0) {
                            alert('WARNING:\nTeam is required!');
                            return false;
                        }
                        else {
                            if ($("#ddlLocation").val() == '0') {
                                alert('WARNING:\nLocation is required!');
                                return false;
                            }
                            else {
                                if (($("#ddlLocation option:selected").text() == "Mary J. Labyak Service Center") && ($("#ddlBuilding").val() == "0")) {
                                    alert('WARNING:\nBuilding is required!');
                                    return false;
                                }
                                else {
                                    $("#btnSubmit").css("visibility", "hidden");
                                }
                            }
                        }
                    }
                }
            }
        });
    });

</script>

</head>
<body style="background-color:gray;">
    <header class="banner">
        <table>
            <tr>
                <td><img src="Images/EmpathHealth.PNG" id="imgLogo" onclick="window.location = 'https://suncoasthospiceportal.org/'" style="height: 93px; width: 271px" /></td>
                <td style="width:53%; text-align:center;">
                    <h1>Building Housekeeping Supplies</h1>
                    <p>User: <asp:Label ID="lblUserName" runat="server" ForeColor="Pink" Text=""></asp:Label></p>
                </td>
                <td style="width:20%; text-align:center;">
                    <h1><asp:Label ID="lblEnvironment" runat="server" Text="" ForeColor="Pink"></asp:Label> </h1>
                </td>
            </tr>
        </table>
    </header>

    <form id="form1" runat="server">
       <article style="padding-left:40px;">

            <div runat="server" id="divMain"  style="display: inline-block;  vertical-align:top; width: 800px; height:595px; border: hidden; padding:5px; padding-left:40px;">
                <p style="height:5px;">&nbsp;</p>
                <div runat="server" id="divEmployee"  style="display: inline-block;  vertical-align:top; width: 800px; height:150px; border: groove; padding:5px; ">
                    <p style="height:5px;">&nbsp;</p>
                    <div style="display: inline-block; vertical-align:top; width: 160px; height:17px; padding-left:12px; padding-top:5px;"><b>First Name:</b>
                       <asp:TextBox ID="txtFirstName" runat="server" Width="140" ></asp:TextBox>
                    </div>
                    <div style="display: inline-block; vertical-align:top; width: 233px; height:17px; padding-left:2px; padding-top:5px;"><b>Last Name:</b>
                       <asp:TextBox ID="txtLastName" runat="server" Width="194" ></asp:TextBox>
                    </div>
                    <div style="display: inline-block; vertical-align:top; width: 300px; height:17px; padding-left:2px; padding-top:5px;"><b>Department:</b>
                       <asp:TextBox ID="txtTeam" runat="server" Width="360" ></asp:TextBox>
                    </div>
                    <p style="height:5px;">&nbsp;</p><br />
                    <div style="display: inline-block; vertical-align:top; width: 400px; height:17px; padding-left:12px; padding-top:5px;"><b>Email:</b>
                       <asp:TextBox ID="txtEmail" runat="server" Width="360" ></asp:TextBox>
                    </div>
                    <div style="display: inline-block; vertical-align:top; width: 300px; height:17px; padding-left:2px; padding-top:5px;"><b>Location:</b>
                       <asp:DropDownList ID="ddlLocation" runat="server" Width="360" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <p style="height:5px;">&nbsp;</p><br />
                    <div style="display: inline-block; vertical-align:top; width: 400px; height:17px; padding-left:12px; padding-top:5px;">
                    </div>
                    <div style="display: inline-block; vertical-align:top; width: 300px; height:17px; padding-left:2px; padding-top:5px;"><b>Building:</b>
                       <asp:DropDownList ID="ddlBuilding" runat="server" Width="360"  >
                            <asp:ListItem ></asp:ListItem>
                            <asp:ListItem Value="1">-- Please Select --</asp:ListItem> 
                            <asp:ListItem Value="2">410</asp:ListItem> 
                            <asp:ListItem Value="3">500</asp:ListItem>
                            <asp:ListItem Value="4">500HH</asp:ListItem>
                            <asp:ListItem Value="5">510</asp:ListItem> 
                            <asp:ListItem Value="6">720 Welcome Center EPIC</asp:ListItem> 
                       </asp:DropDownList>
                    </div>
               </div>
               <p style="height:5px;">&nbsp;</p>
               <div runat="server" id="divSupplies"  style="display: inline-block;  vertical-align:top; width: 800px; height:395px; border: groove; padding:5px; ">

                   <br /><asp:CheckBox  ID="cbBags" Font-Bold="true" Text="Bags" runat="server" AutoPostBack="true" OnCheckedChanged="cbBags_CheckedChanged"  />
                   <div runat="server" id="divBags"  style="display: none;  vertical-align:top; width: 785px; height:135px; border: hidden; padding:5px; ">
                        <div style="display: inline-block; width: 400px; border: hidden; ">                       
                           <asp:Label ID="lbl1" runat="server" Width="325" Text="#4 Brown Bags:"></asp:Label>
                           <asp:TextBox ID="txt1" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl2" runat="server" Width="321" Text="#6 Brown Bags:"></asp:Label>
                           <asp:TextBox ID="txt2" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">                       
                           <asp:Label ID="lbl3" runat="server" Width="325" Text="#8 Brown Bags:"></asp:Label>
                           <asp:TextBox ID="txt3" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl4" runat="server" Width="321" Text="#20 Brown Bags:"></asp:Label>
                           <asp:TextBox ID="txt4" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">                       
                           <asp:Label ID="lbl5" runat="server" Width="325" Text="#57 Brown Bags:"></asp:Label>
                           <asp:TextBox ID="txt5" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl6" runat="server" Width="321" Text="Large Handled Bag:"></asp:Label>
                           <asp:TextBox ID="txt6" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">                       
                           <asp:Label ID="lbl7" runat="server" Width="325" Text="Ziplock Sandwich Bag:"></asp:Label>
                           <asp:TextBox ID="txt7" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl8" runat="server" Width="321" Text="Ziplock 1 gallon Bag:"></asp:Label>
                           <asp:TextBox ID="txt8" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">                       
                           <asp:Label ID="lbl9" runat="server" Width="325" Text="Ziplock 2 gallon Bag:"></asp:Label>
                           <asp:TextBox ID="txt9" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl10" runat="server" Width="321" Text="Standard Thank You T-Shirt Bag:"></asp:Label>
                           <asp:TextBox ID="txt10" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">                       
                           <asp:Label ID="lbl11" runat="server" Width="325" Text="Large Thank You T-Shirt Bag:"></asp:Label>
                           <asp:TextBox ID="txt11" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                   </div>

                   <br /><asp:CheckBox ID="cbCleaners" runat="server" Font-Bold="True" Text="Cleaners" AutoPostBack="true" OnCheckedChanged="cbCleaners_CheckedChanged" />
                      <div runat="server" id="divCleaners"  style="display: none;  vertical-align:top; width: 785px; height:185px; border: hidden; padding:5px; ">
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl12" runat="server" Width="325" Text="Bolt Floor Cleaner:"></asp:Label>
                           <asp:TextBox ID="txt12" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl13" runat="server" Width="321" Text="Clorox Clean Up w/Bleach:"></asp:Label>
                           <asp:TextBox ID="txt13" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl14" runat="server" Width="325" Text="Clorox Dispatch Spray (CLO68970):"></asp:Label>
                           <asp:TextBox ID="txt14" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl15" runat="server" Width="321" Text="Clorox Germicidal Wipes - Bucket:"></asp:Label>
                           <asp:TextBox ID="txt15" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl16" runat="server" Width="325" Text="Glass Cleaner:"></asp:Label>
                           <asp:TextBox ID="txt16" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl17" runat="server" Width="321" Text="Lemon Furniture Polish:"></asp:Label>
                           <asp:TextBox ID="txt17" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl18" runat="server" Width="325" Text="Lysol Disinfectant Spray:"></asp:Label>
                           <asp:TextBox ID="txt18" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl19" runat="server" Width="321" Text="Lysol Foaming Spray:"></asp:Label>
                           <asp:TextBox ID="txt19" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl20" runat="server" Width="325" Text="AirWick Air Freshener (RAC77002):"></asp:Label>
                           <asp:TextBox ID="txt20" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl21" runat="server" Width="321" Text="Lysol Toilet Bowl Cleaner:"></asp:Label>
                           <asp:TextBox ID="txt21" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                       <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl22" runat="server" Width="325" Text="Plastic Spray Bottle 32oz:"></asp:Label>
                           <asp:TextBox ID="txt22" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl23" runat="server" Width="321" Text="Trigger Sprayer for 32oz Bottle:"></asp:Label>
                           <asp:TextBox ID="txt23" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl24" runat="server" Width="325" Text="Windex - Sprayer (SJN322338):"></asp:Label>
                           <asp:TextBox ID="txt24" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl25" runat="server" Width="321" Text="Johnny Mops (case/25):"></asp:Label>
                           <asp:DropDownList ID="ddlJMops" runat="server" Width="45"  >
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem> 
                            <asp:ListItem Value="2">2</asp:ListItem> 
                            <asp:ListItem Value="3">3</asp:ListItem> 
                           </asp:DropDownList>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl26" runat="server" Width="325" Text="Johnny Mop Holder:"></asp:Label>
                           <asp:TextBox ID="txt26" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl27" runat="server" Width="321" Text="Urinal Deodorizer Screen (3WDS60CME):"></asp:Label>
                           <asp:TextBox ID="txt27" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                   </div>

                   <br /><asp:CheckBox ID="cbKitchen" runat="server" Font-Bold="True" Text="Kitchen" AutoPostBack="true" OnCheckedChanged="cbKitchen_CheckedChanged"/>
                      <div runat="server" id="divKitchen"  style="display: inline-block;  vertical-align:top; width: 785px; height:160px; border: hidden; padding:5px; ">
                       <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl28" runat="server" Width="325" Text="16oz Translucent Cup:"></asp:Label>
                           <asp:TextBox ID="txt28" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl29" runat="server" Width="321" Text="16oz Translucent Cup Lids:"></asp:Label>
                           <asp:TextBox ID="txt29" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl30" runat="server" Width="325" Text="6in. Plates ECO:"></asp:Label>
                           <asp:TextBox ID="txt30" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl31" runat="server" Width="321" Text="9in. Plates ECO:"></asp:Label>
                           <asp:TextBox ID="txt31" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl32" runat="server" Width="325" Text="Bowls ECO:"></asp:Label>
                           <asp:TextBox ID="txt32" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl33" runat="server" Width="321" Text="Coffee Cups 10oz (GJO11256CT):"></asp:Label>
                           <asp:TextBox ID="txt33" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl34" runat="server" Width="325" Text="Coffee Cup lids (GJO11259CT):"></asp:Label>
                           <asp:TextBox ID="txt34" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl35" runat="server" Width="321" Text="Forks - Heavy Weight:"></asp:Label>
                           <asp:TextBox ID="txt35" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl36" runat="server" Width="325" Text="Green Scrub Pads:"></asp:Label>
                           <asp:TextBox ID="txt36" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl37" runat="server" Width="321" Text="Knives - Heavy Weight:"></asp:Label>
                           <asp:TextBox ID="txt37" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl38" runat="server" Width="325" Text="Lunch Napkins:"></asp:Label>
                           <asp:TextBox ID="txt38" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl39" runat="server" Width="321" Text="Spoons - Heavy Weight:"></asp:Label>
                           <asp:TextBox ID="txt39" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl40" runat="server" Width="325" Text="Straws - Flex:"></asp:Label>
                           <asp:TextBox ID="txt40" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                      </div>

                   <br /><asp:CheckBox ID="cbLaundry" runat="server" Font-Bold="True" Text="Laundry" AutoPostBack="true" OnCheckedChanged="cbLaundry_CheckedChanged" />
                     <div runat="server" id="divLaundry"  style="display: inline-block;  vertical-align:top; width: 785px; height:30px; border: hidden; padding:5px; ">
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl41" runat="server" Width="325" Text="Bleach Case/4:"></asp:Label>
                           <asp:TextBox ID="txt41" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl42" runat="server" Width="321" Text="Dryer Sheets:"></asp:Label>
                           <asp:TextBox ID="txt42" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl43" runat="server" Width="325" Text="Tide Laundry Soap:"></asp:Label>
                           <asp:TextBox ID="txt43" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                   </div>

                   <br /><asp:CheckBox ID="cbLiners" runat="server" Font-Bold="True" Text="Liners" AutoPostBack="true" OnCheckedChanged="cbLiners_CheckedChanged" />
                        <div runat="server" id="divLiners"  style="display: none;  vertical-align:top; width: 785px; height:85px; border: hidden; padding:5px; ">
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl44" runat="server" Width="325" Text="17 x 18 Liner:"></asp:Label>
                           <asp:TextBox ID="txt44" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl45" runat="server" Width="321" Text="24 x 23 Liner (S242306N):"></asp:Label>
                           <asp:TextBox ID="txt45" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl46" runat="server" Width="325" Text="24 x 33 Liner (VH243308N):"></asp:Label>
                           <asp:TextBox ID="txt46" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px;">
                           <asp:Label ID="lbl47" runat="server" Width="321" Text="30 x 37 Liner (VH303710N):"></asp:Label>
                           <asp:TextBox ID="txt47" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl48" runat="server" Width="325" Text="38 x 60 Liner (VH386017N):"></asp:Label>
                           <asp:TextBox ID="txt48" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px;">
                           <asp:Label ID="lbl49" runat="server" Width="321" Text="43 x 48 Liner (VH434816N):"></asp:Label>
                           <asp:TextBox ID="txt49" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl50" runat="server" Width="325" Text="55 Gallon Black Bags (BLK385815):"></asp:Label>
                           <asp:TextBox ID="txt50" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                   </div>

                   <br /><asp:CheckBox ID="cbPaperGoods" runat="server" Font-Bold="True" Text="Paper Goods" AutoPostBack="true" OnCheckedChanged="cbPaperGoods_CheckedChanged" />
                     <div runat="server" id="divPaperGoods"  style="display: none;  vertical-align:top; width: 785px; height:110px; border: hidden; padding:5px; ">
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl51" runat="server" Width="325" Text="Facial Tissue/Angel Soft:"></asp:Label>
                           <asp:TextBox ID="txt51" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl52" runat="server" Width="321" Text="Facial Tissue/low end:"></asp:Label>
                           <asp:TextBox ID="txt52" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl53" runat="server" Width="325" Text="Jumbo TP Rolls:"></asp:Label>
                           <asp:TextBox ID="txt53" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl54" runat="server" Width="321" Text="Multifold Towels:"></asp:Label>
                           <asp:TextBox ID="txt54" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl55" runat="server" Width="325" Text="Roll Kitchen Towels:"></asp:Label>
                           <asp:TextBox ID="txt55" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl56" runat="server" Width="321" Text="Toilet Paper:"></asp:Label>
                           <asp:TextBox ID="txt56" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl57" runat="server" Width="325" Text="Wax Liners:"></asp:Label>
                           <asp:TextBox ID="txt57" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl58" runat="server" Width="321" Text="Wax Paper (5016MAR):"></asp:Label>
                           <asp:TextBox ID="txt58" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl59" runat="server" Width="325" Text="PeakServe Continuous Hand Towel, 12 Packs:"></asp:Label>
                           <asp:TextBox ID="txt59" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                     </div>

                   <br /><asp:CheckBox ID="cbSoap" runat="server" Font-Bold="True" Text="Soap" AutoPostBack="true" OnCheckedChanged="cbSoap_CheckedChanged" />
                     <div runat="server" id="divSoap"  style="display: none;  vertical-align:top; width: 785px; height:90px; border: hidden; padding:5px; ">
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl60" runat="server" Width="325" Text="Antibacterial Hand Soap (DER8200):"></asp:Label>
                           <asp:TextBox ID="txt60" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl61" runat="server" Width="321" Text="Hand Soap Dispenser Refill (DER8100):"></asp:Label>
                           <asp:TextBox ID="txt61" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl62" runat="server" Width="325" Text="Purell Gel Refill (GOJ5456-4):"></asp:Label>
                           <asp:TextBox ID="txt62" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl63" runat="server" Width="321" Text="Hand Soap Dispenser Refill (GP42818 - EH South only 1200ml):"></asp:Label>
                           <asp:TextBox ID="txt63" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                        <p style="height:5px;">&nbsp;</p>
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl64" runat="server" Width="325" Text="Dawn Dish Soap (1 Gallon)"></asp:Label>
                           <asp:TextBox ID="txt64" TextMode="Number" runat="server" width="37" min="0" max="5" step="1" />  
                        </div> 
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl65" runat="server" Width="321" Text="Tork Mild Foam Soap Refill (TRK401211) 6 cartons"></asp:Label>
                           <asp:TextBox ID="txt65" TextMode="Number" runat="server" width="37" min="0" max="10" step="1"/>
                        </div>
                   </div> 

                   <br /><asp:CheckBox ID="cbWater" runat="server" Font-Bold="True" Text="Water" AutoPostBack="true" OnCheckedChanged="cbWater_CheckedChanged"/>
                     <div runat="server" id="divWater"  style="display: none;  vertical-align:top; width: 785px; height:30px; border: hidden; padding:5px; ">
                        <div style="display: inline-block; width: 400px; border: hidden; ">
                           <asp:Label ID="lbl66" runat="server" Width="325" Text="Distilled Water:"></asp:Label>
                           <asp:TextBox ID="txt66" TextMode="Number" runat="server" width="37" min="0" max="10" step="1" />
                        </div>
                        <div style="display: inline-block; width: 375px; border: hidden; padding-left: 5px; ">
                           <asp:Label ID="lbl67" runat="server" Width="321" Text="Drinking Water - 40/16.9oz:"></asp:Label>
                           <asp:DropDownList ID="ddlDrWater" runat="server" Width="45"  >
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="1">5</asp:ListItem> 
                            <asp:ListItem Value="2">10</asp:ListItem> 
                            <asp:ListItem Value="3">15</asp:ListItem> 
                            <asp:ListItem Value="4">20</asp:ListItem> 
                            <asp:ListItem Value="5">25</asp:ListItem> 
                           </asp:DropDownList>
                        </div>
                   </div>

               </div>
                <p><asp:Label ID="lblNote" runat="server" Text="" Font-Bold="true" ForeColor="Red" BackColor="WhiteSmoke"></asp:Label></p>
            </div>
       </article>

       <article><div id="divSubmit" style="text-align:center" runat="server"><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Width="150px" CssClass="button1"></asp:Button></div></article>

    <p><asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="Green" BackColor="WhiteSmoke"></asp:Label></p>
    <p><asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red" BackColor="WhiteSmoke"></asp:Label></p>

     </form>

    <footer>
	  <p style="font-size:medium; text-align:center;" >&copy; <%= DateTime.Now.Year %> &bull; All Rights Reserved &bull; Empath Health<br />5771 Roosevelt Blvd. &bull; Clearwater, FL 33760 &bull; 727.523.4003</p>
    </footer>

</body>
</html>

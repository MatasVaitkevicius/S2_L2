<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="L2.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .button {
            font-size: Large;
            height: 36px;
            width: 350px;
            background-color: #ffd800;
            font-family: 'Times New Roman';
        }
        .text-box {
            width: 1400px;
            height: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="LD_16.Mokesčiai"></asp:Label>
            <p>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Skaityti pradinius duomenis ir spausdinti" CssClass="button" />
            </p>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" OnTextChanged="TextBox1_TextChanged" Width="1400px" Height="300px"></asp:TextBox>
            <p>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Spausdinti rezultatus" CssClass="button" />
            </p>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" OnTextChanged="TextBox2_TextChanged" Width="1400px" Height="300px"></asp:TextBox>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Įveskite norimą Mėnesį, pagal kurį bus išfiltruotas sąrašas"></asp:Label>
                &nbsp;Galimi mėnesiai: Sausis, Vasaris, Kovas, Balandis, Geguze, Birzelis, Liepa, Rugjputis, Rugsejis, Spalis, Lapkritis, Gruodis
            </p>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <p>
                <asp:Label ID="Label3" runat="server" Text="Įveskite norimą komunalinę paslauga, pagal kurią bus išfiltruotas sąrašas"></asp:Label>
                &nbsp;Galimos komunalines paslaugos: Elektra, Vanduo, Siuksles
            </p>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <p>
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Filtruoti sąrašą" CssClass="button" />
            </p>
            <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" OnTextChanged="TextBox3_TextChanged" Width="1400px" Height="300px"></asp:TextBox>
        </div>
    </form>
</body>
</html>

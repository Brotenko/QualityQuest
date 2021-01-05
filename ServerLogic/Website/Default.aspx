<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Website._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div class="text-center">
    <h1 class="display-4">Welcome to Quality Quest</h1>
    <p>Please enter a Room Code in the field below to start playing!</p>
</div>

<div class="text-center-input">
    <h5 style="text-align: left; margin-left: 5%">Room Code:</h5>
    <form>
        <input type="text" class="input-group-text newtec-input-box" id="SessionKey" maxlength="6" minlength="6" placeholder="Please enter the 6-figure Room Code" required />
        <button class="input-button">Play game</button>
    </form>
    <p style="font-weight: bold; font-size: 12px">By playing the game, you agree to the <a target="_blank" rel="noopener noreferrer" href="~/TermsOfService">Terms of Service.</a></p>
    <a target="_blank" rel="noopener noreferrer" href="https://www.newtec.de/"><img src="newtec_logo.png" alt="NewTec logo" class="logo" /></a>
</div>

</asp:Content>

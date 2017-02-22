<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CryptoSampleCode.aspx.cs" Inherits="SampleCode.C3.Samples.Goc.Components.Crypto.C3.Sample.CryptoSampleCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <style>
        pre.code code {
            color: #222;
        }

        .code {
            background: #efefef no-repeat scroll 0 0;
            padding: 15px 10px 10px 10px;
            width: 800px;
            overflow: auto;
            border-bottom: 1px solid #ccc;
        }
        .alignText {
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
<!--

        $(document).ready(function() {
            $('#lnkShowCode').click(function() {
                toggle_visibility($('#ddlAlgorithmTypes').val(), $('#ddlDataTypes').val());
            });
        });

        function toggle_visibility(algoType, dataType) {

            var requestType = algoType + dataType;
            var e = document.getElementById(requestType);
            e.style.display = ((e.style.display !== 'none') ? 'none' : 'block');
            
            switch (requestType) {
            case "AESText":
                document.getElementById("AESBytes").style.display = 'none';
                document.getElementById("AESMemoryStream").style.display = 'none';
                document.getElementById("TripleDESText").style.display = 'none';
                document.getElementById("TripleDESBytes").style.display = 'none';
                document.getElementById("TripleDESMemoryStream").style.display = 'none';
                break;

            case "AESBytes":
                document.getElementById("AESText").style.display = 'none';
                document.getElementById("AESMemoryStream").style.display = 'none';
                document.getElementById("TripleDESText").style.display = 'none';
                document.getElementById("TripleDESBytes").style.display = 'none';
                document.getElementById("TripleDESMemoryStream").style.display = 'none';
                break;

            case "AESMemoryStream":
                document.getElementById("AESText").style.display = 'none';
                document.getElementById("AESBytes").style.display = 'none';
                document.getElementById("TripleDESText").style.display = 'none';
                document.getElementById("TripleDESBytes").style.display = 'none';
                document.getElementById("TripleDESMemoryStream").style.display = 'none';
                break;

            case "TripleDESText":
                document.getElementById("AESText").style.display = 'none';
                document.getElementById("AESBytes").style.display = 'none';
                document.getElementById("AESMemoryStream").style.display = 'none';
                document.getElementById("TripleDESBytes").style.display = 'none';
                document.getElementById("TripleDESMemoryStream").style.display = 'none';
                break;

            case "TripleDESBytes":
                document.getElementById("AESText").style.display = 'none';
                document.getElementById("AESBytes").style.display = 'none';
                document.getElementById("AESMemoryStream").style.display = 'none';
                document.getElementById("TripleDESText").style.display = 'none';
                document.getElementById("TripleDESMemoryStream").style.display = 'none';
                break;

            case "TripleDESMemoryStream":
                document.getElementById("AESText").style.display = 'none';
                document.getElementById("AESBytes").style.display = 'none';
                document.getElementById("AESMemoryStream").style.display = 'none';
                document.getElementById("TripleDESText").style.display = 'none';
                document.getElementById("TripleDESBytes").style.display = 'none';
                break;
            }
       
        }
        //-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblDataTypes" runat="server" AssociatedControlID="txtValue" CssClass="alignText" Text="Enter your text which will be of type"></asp:Label>
            <asp:DropDownList ID="ddlDataTypes" runat="server" Width="150px">
                <asp:ListItem Value="Text" Text="Text" />
                <asp:ListItem Value="Bytes" Text="Bytes" />
                <asp:ListItem Value="MemoryStream" Text="MemoryStream" />
            </asp:DropDownList>
            <br /><br />
            <asp:TextBox ID="txtValue" runat="server" TextMode="MultiLine" Rows="10" Columns="98"></asp:TextBox>
        </div>
        <br />
        <div>
            <div>
                <asp:Label ID="lblAlgorithmType" runat="server" AssociatedControlID="ddlAlgorithmTypes" Text="Algorithm Types:"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlAlgorithmTypes" runat="server" Width="150px">
                    <asp:ListItem Value="TripleDES" Text="TripleDES" />
                    <asp:ListItem Value="AES" Text="AES" />                    
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div>
            <%-- ReSharper disable once Html.IdNotResolved --%>
            <strong> Optional parameter</strong>
            <br/>
            <asp:Label ID="lblKeyGenerator" runat="server" Text="The Salt key is an optional parameter if you are to use the SALT key then you can enter your own SALT Key or click the button to generate one." AssociatedControlID="txtSaltKey"></asp:Label>            
            <br/>
            <asp:Button ID="btnGenerateSaltKey" runat="server" Text="Generate Salt Key" OnClick="BtnGenerateSaltKeyClick" />
            <div> &nbsp; </div>
            <asp:TextBox ID="txtSaltKey" runat="server" Width="800px"></asp:TextBox>
        </div>
        <div>
            <br />
            
        </div>
        <br />
        <div>
            <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" OnClick="BtnEncryptClick" />
            <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" OnClick="BtnDecryptClick" />
        </div>
        <a id="lnkShowCode" href="#">Click here to code use for this example</a>
        <div id="AESText" style="display: none;">
            <pre class="code">
                <code id="codeAES" runat="server">
// AES - CODE EXAMPLE FOR TEXT ENCRYPTION/DECRYPTION

// References
using GoC.Components.Cryptography.SaltGenerator;
using System.Security.Cryptography;
using GoC.Components.Cryptography;

// ***************************************************************** 
// Encrypt Text
// ***************************************************************** 

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Encrypt(SecretKey, plainText);
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Encrypt(SecretKey, plainText, SaltKey);
}

// *****************************************************************                     
// Decrypt Text
// ***************************************************************** 

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Decrypt(SecretKey, encryptedText);
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Decrypt(SecretKey, encryptedText, SaltKey);
}
                </code>
            </pre>
        </div>
        <div id="AESBytes" style="display: none;">
            <pre class="code">
                <code id="codeAESBytes" runat="server">
// AES - CODE EXAMPLE FOR BYTES ENCRYPTION/DECRYPTION

// References
using GoC.Components.Cryptography.SaltGenerator;
using System.Security.Cryptography;
using GoC.Components.Cryptography;

// ***************************************************************** 
// Encrypt Text
// ***************************************************************** 

var bytes = Encoding.UTF8.GetBytes(this.plainText.Text);

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Encrypt(SecretKey, bytes);
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Encrypt(SecretKey, bytes, SaltKey);
}

// *****************************************************************                     
// Decrypt Text
// ***************************************************************** 

//Decrypt the encrypted data
this.encryptedData = Convert.FromBase64String(this.encryptedValue.Text);

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Decrypt(SecretKey, this.encryptedData)
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Decrypt(SecretKey, this.encryptedData, this.txtSaltKey.Text)
}
                </code>
            </pre>
        </div>
        <div id="AESMemoryStream" style="display: none;">
            <pre class="code">
                <code id="code2" runat="server">
// AES - CODE EXAMPLE FOR MEMORY STREAM ENCRYPTION/DECRYPTION

// References
using GoC.Components.Cryptography.SaltGenerator;
using System.Security.Cryptography;
using GoC.Components.Cryptography;

// ***************************************************************** 
// Encrypt Text
// ***************************************************************** 

var bytes = Encoding.UTF8.GetBytes(this.plainText.Text);

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Encrypt(SecretKey, bytes);
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Encrypt(SecretKey, bytes, SaltKey);
}

// *****************************************************************                     
// Decrypt Text
// ***************************************************************** 

//Decrypt the encrypted data
this.encryptedData = Convert.FromBase64String(this.encryptedValue.Text);

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Decrypt(SecretKey, this.encryptedData)
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
{
    crypto.Decrypt(SecretKey, this.encryptedData, this.txtSaltKey.Text)
}
                </code>
            </pre>
        </div>
        <div id="TripleDESText" style="display: none;">
            <pre class="code">
                <code id="codeTripleDES" runat="server">
// TRIPLEDES - CODE EXAMPLE FOR TEXT ENCRYPTION/DECRYPTION

// References
using GoC.Components.Cryptography.SaltGenerator;
using System.Security.Cryptography;
using GoC.Components.Cryptography;

// ***************************************************************** 
// Encrypt Text
// ***************************************************************** 

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Encrypt(SecretKey, plainText);
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Encrypt(SecretKey, plainText, SaltKey);
}

// ***************************************************************** 
// Decrypt Text
// ***************************************************************** 

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Decrypt(SecretKey, encryptedText)
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Decrypt(SecretKey, encryptedText, SaltKey);
}                                       
    
                </code>
            </pre>
        </div>
        <div id="TripleDESBytes" style="display: none;">
            <pre class="code">
                <code id="codeTripleDESBytes" runat="server">
// TRIPLEDES - CODE EXAMPLE FOR BYTES ENCRYPTION/DECRYPTION

// References
using GoC.Components.Cryptography.SaltGenerator;
using System.Security.Cryptography;
using GoC.Components.Cryptography;

// ***************************************************************** 
// Encrypt Text
// ***************************************************************** 

var bytes = Encoding.UTF8.GetBytes(this.plainText.Text);

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Encrypt(SecretKey, bytes);
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Encrypt(SecretKey, bytes, SaltKey);
}

// ***************************************************************** 
// Decrypt Text
// ***************************************************************** 
                                        
//Decrypt the encrypted data
this.encryptedData = Convert.FromBase64String(this.encryptedValue.Text);

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Decrypt(SecretKey, this.encryptedData)
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Decrypt(SecretKey, this.encryptedData, this.txtSaltKey.Text)
}
                </code>
            </pre>
        </div> 
        <div id="TripleDESMemoryStream" style="display: none;">
            <pre class="code">
                <code id="code1" runat="server">
// TRIPLEDES - CODE EXAMPLE FOR MEMORY STREAM ENCRYPTION/DECRYPTION

// References
using GoC.Components.Cryptography.SaltGenerator;
using System.Security.Cryptography;
using GoC.Components.Cryptography;

// ***************************************************************** 
// Encrypt Text
// ***************************************************************** 

byte[] encryptBytes = Encoding.UTF8.GetBytes(this.plainText.Text);

// This is without a SALT Key
using (var memory = new MemoryStream(encryptBytes))
{
    using (var crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
    {
        this.encryptedMemoryStream = crypto.Encrypt(SecretKey, memory);
    }
}
                                            
// This is with a SALT Key
using (var memory = new MemoryStream(encryptBytes))
{
    using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
    {
        this.encryptedMemoryStream = crypto.Encrypt(SecretKey, memory, SaltKey);
    }
}

// ***************************************************************** 
// Decrypt Text
// ***************************************************************** 

this.encryptedMemoryStream = new MemoryStream(Convert.FromBase64String(this.encryptedValue.Text));

// This is without a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Decrypt(SecretKey, encryptedMemoryStream)
}
                            
// This is with a SALT Key
using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
{
    crypto.Decrypt(SecretKey, encryptedMemoryStream, SaltKey);
}    
                </code>
            </pre>
        </div>                   
    </form>
</body>
</html>

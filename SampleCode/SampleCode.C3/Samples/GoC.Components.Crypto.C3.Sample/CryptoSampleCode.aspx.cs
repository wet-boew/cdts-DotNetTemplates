
namespace SampleCode.C3.Samples.Goc.Components.Crypto.C3.Sample
{
    using System;
    using System.IO;

    using System.Security.Cryptography;
    using System.Text;

    using global::GoC.Components.Cryptography;
    using global::GoC.Components.Cryptography.SaltGenerator;

    public partial class CryptoSampleCode : System.Web.UI.Page
    {
        private const string SecretKey = "8fd4702b-9054-4a72-8a81-492b27266f58";
        private MemoryStream memoryStream;
        private byte[] encryptedData;

#region Events
        /// <summary>
        /// Generate a salt key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGenerateSaltKeyClick(object sender, EventArgs e)
        {
            this.txtSaltKey.Text = SaltGenerator.GenerateSaltKey();
        }

        /// <summary>
        /// Encrypt value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnEncryptClick(object sender, EventArgs e)
        {
            switch (this.ddlDataTypes.SelectedValue)
            {
                case "Text":
                    this.EncryptText(this.ddlAlgorithmTypes.SelectedValue);
                    break;

                case "Bytes":
                    this.EncryptBytes(this.ddlAlgorithmTypes.SelectedValue);
                    break;

                case "MemoryStream":
                    this.EncryptMemoryStream(this.ddlAlgorithmTypes.SelectedValue);
                    break;
            }
        }

        /// <summary>
        /// Decrypt value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnDecryptClick(object sender, EventArgs e)
        {
            switch (this.ddlDataTypes.SelectedValue)
            {
                case "Text":
                    this.DecryptText(this.ddlAlgorithmTypes.SelectedValue);
                    break;

                case "Bytes":
                    this.DecryptBytes(this.ddlAlgorithmTypes.SelectedValue);
                    break;

                case "MemoryStream":
                    this.DecryptMemoryStream(this.ddlAlgorithmTypes.SelectedValue);
                    break;
            }
        } 
#endregion

#region Encrypt Methods 

        /// <summary>
        /// Encrypt a text
        /// </summary>
        /// <param name="algorigthmName"></param>
        private void EncryptText(string algorigthmName)
        {
            switch (algorigthmName)
            {
                case "AES":
                    using (var crypto = new CryptoServices(typeof(AesManaged)))
                    {
                        this.txtValue.Text = this.txtSaltKey.Text == string.Empty ? crypto.Encrypt(SecretKey, this.txtValue.Text) : crypto.Encrypt(SecretKey, this.txtValue.Text, this.txtSaltKey.Text);
                    }
                    break;

                case "TripleDES":
                    using (var crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
                    {
                        this.txtValue.Text = crypto.Encrypt(SecretKey, this.txtValue.Text);
                    }
                    break;
            }
        }

        /// <summary>
        /// Encrypts the bytes.
        /// </summary>
        /// <param name="algorigthmName"></param>
        private void EncryptBytes(string algorigthmName)
        {
            var bytes = Encoding.UTF8.GetBytes(this.txtValue.Text);

            switch (algorigthmName)
            {
                case "AES":
                    using (var crypto = new CryptoServices(typeof(AesManaged)))
                    {
                        this.encryptedData = this.txtSaltKey.Text == string.Empty ? crypto.Encrypt(SecretKey, bytes) : crypto.Encrypt(SecretKey, bytes, this.txtSaltKey.Text);
                    }
                    break;

                case "TripleDES":
                    using (var crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
                    {
                        this.encryptedData = this.txtSaltKey.Text == string.Empty ? crypto.Encrypt(SecretKey, bytes) : crypto.Encrypt(SecretKey, bytes, this.txtSaltKey.Text);
                    }
                    break;
            }

            this.txtValue.Text = Convert.ToBase64String(this.encryptedData);
        }

        /// <summary>
        /// Encrypts the memory stream.
        /// </summary>
        private void EncryptMemoryStream(string algorigthmName)
        {
            byte[] encryptBytes = Encoding.UTF8.GetBytes(this.txtValue.Text);
            using (var memory = new MemoryStream(encryptBytes))
            {
                switch (algorigthmName)
                {
                    case "AES":
                        using (var crypto = new CryptoServices(typeof(AesManaged)))
                        {
                            this.memoryStream = this.txtSaltKey.Text == string.Empty ? crypto.Encrypt(SecretKey, memory) : crypto.Encrypt(SecretKey, memory, this.txtSaltKey.Text);
                        }
                        break;

                    case "TripleDES":
                        using (var crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
                        {
                            this.memoryStream = this.txtSaltKey.Text == string.Empty ? crypto.Encrypt(SecretKey, memory) : crypto.Encrypt(SecretKey, memory, this.txtSaltKey.Text);
                        }
                        break;
                }

                this.txtValue.Text = Convert.ToBase64String(this.memoryStream.ToArray());
            }
        } 

#endregion

#region Decrypt Methods

        /// <summary>
        /// Decrypts the bytes.
        /// </summary>
        /// <param name="algorigthmName">The algorigthm Name.</param>
        private void DecryptBytes(string algorigthmName)
        {
            this.encryptedData = Convert.FromBase64String(this.txtValue.Text.Trim());

            switch (algorigthmName)
            {

                case "AES":
                    using (var crypto = new CryptoServices(typeof(AesManaged)))
                    {
                        this.txtValue.Text = Encoding.Default.GetString(this.txtSaltKey.Text == string.Empty ? crypto.Decrypt(SecretKey, this.encryptedData) : crypto.Decrypt(SecretKey, this.encryptedData, this.txtSaltKey.Text));
                    }
                    break;

                case "TripleDES":
                    using (var crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
                    {
                        this.txtValue.Text = Encoding.Default.GetString(this.txtSaltKey.Text == string.Empty ? crypto.Decrypt(SecretKey, this.encryptedData) : crypto.Decrypt(SecretKey, this.encryptedData, this.txtSaltKey.Text));
                    }
                    break;
            }
        }

        /// <summary>
        ///     Decrypts the memory stream.
        /// </summary>
        /// <param name="algorigthmName"></param>

        private void DecryptMemoryStream(string algorigthmName)
        {
            this.memoryStream = new MemoryStream(Convert.FromBase64String(this.txtValue.Text.Trim()));

            switch (algorigthmName)
            {

                case "AES":
                    using (CryptoServices crypto = new CryptoServices(typeof(AesManaged)))
                    {
                        this.txtValue.Text = ReadAll(this.txtSaltKey.Text == string.Empty ? crypto.Decrypt(SecretKey, this.memoryStream) : crypto.Decrypt(SecretKey, this.memoryStream, this.txtSaltKey.Text));
                    }
                    break;

                case "TripleDES":
                    using (CryptoServices crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
                    {
                        this.txtValue.Text = ReadAll(this.txtSaltKey.Text == string.Empty ? crypto.Decrypt(SecretKey, this.memoryStream) : crypto.Decrypt(SecretKey, this.memoryStream, this.txtSaltKey.Text));
                    }
                    break;
            }
        }

        /// <summary>
        ///     Decrypts the text.
        /// </summary>
        /// <param name="algorigthmName"></param>
        private void DecryptText(string algorigthmName)
        {
            switch (algorigthmName)
            {

                case "AES":
                    using (var crypto = new CryptoServices(typeof(AesManaged)))
                    {
                        this.txtValue.Text = this.txtSaltKey.Text == string.Empty ? crypto.Decrypt(SecretKey, this.txtValue.Text) : crypto.Decrypt(SecretKey, this.txtValue.Text, this.txtSaltKey.Text);
                    }
                    break;

                case "TripleDES":
                    using (var crypto = new CryptoServices(typeof(TripleDESCryptoServiceProvider)))
                    {
                        this.txtValue.Text = this.txtSaltKey.Text == string.Empty ? crypto.Decrypt(SecretKey, this.txtValue.Text) : crypto.Decrypt(SecretKey, this.txtValue.Text, this.txtSaltKey.Text);
                    }
                    break;
            }
        }

        #endregion

#region Private Functions
        /// <summary>
        /// Reads all.
        /// </summary>
        /// <param name="memStream">The memory stream.</param>
        /// <returns></returns>
        private static string ReadAll(MemoryStream memStream)
        {
            // Reset the stream otherwise you will just get an empty string.
            // Remember the position so we can restore it later.
            var pos = memStream.Position;
            memStream.Position = 0;

            var reader = new StreamReader(memStream);
            var str = reader.ReadToEnd();

            // Reset the position so that subsequent writes are correct.
            memStream.Position = pos;

            return str;
        } 

#endregion

    }
}
using System;
using System.Text;
using System.Windows;
using System.Windows.Input;

using WpfApp.Models;
namespace WpfApp
{
    using System.Security.Cryptography;

    public partial class MainWindow : Window
    {
        #region 0. Golbal Values
        const String AES_Key = null; /* ■ Kirigakuretora: 執行前需替換此參數 */
        const String AES_IV = null; /* ■ Kirigakuretora: 執行前需替換此參數 */

        OriginalText AES_Text = new OriginalText();
        #endregion

        #region 1.Load
        public MainWindow()
        {
            InitializeComponent();
        }

        internal enum ErrorMsgEnum
        {
            未回傳訊息=-3,
            重覆執行=-2,
            連線錯誤=-1,
            不得為空值=0
        }
        #endregion

        #region 2.Bind
        internal String Encryption(String Text)
        {
            AesManaged Aes = new AesManaged();
            Aes.BlockSize = 128;
            Aes.KeySize = 256;
            Aes.Mode = CipherMode.CBC;
            Aes.IV = Encoding.UTF8.GetBytes(AES_IV);
            Aes.Key = Encoding.UTF8.GetBytes(AES_Key);
            Aes.Padding = PaddingMode.PKCS7;

            Byte[] ByteText = Encoding.UTF8.GetBytes(Text);
            Byte[] EncryptText = Aes.CreateEncryptor().TransformFinalBlock(ByteText, 0, ByteText.Length);

            return Convert.ToBase64String(EncryptText);
        }

        private void BtnRun_CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void BtnClear_CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void BtnRun_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            AES_Text.Text = TB_Input.Text;
            if (!String.IsNullOrEmpty(AES_Text.Text))
            {
                TB_Result.Text = Encryption(AES_Text.Text);
            }
            else
            {
                MessageBox.Show(nameof(ErrorMsgEnum.不得為空值));
            }
        }

        private void BtnClear_CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TB_Input.Text = String.Empty;
            TB_Result.Text = String.Empty;
        }
        #endregion
    }
}

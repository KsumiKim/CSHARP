using System.Windows.Forms;

namespace MyLibrary.Helpers
{
    public class WinformHelper
    {
        /// <summary>
        /// 삭제확인 대화상자를 띄운다.
        /// </summary>
        /// <param name="text">MessageBox.Show의 text 매개변수</param>
        /// <param name="caption">MessageBox.Show의 caption 매개변수</param>
        /// <returns>Yes를 클릭했으면 true, 그 외는 false</returns>
        public static bool AskToDelete(string text = "삭제하시겠습니까?", string caption = "확인")
        {
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }
        
    }
}

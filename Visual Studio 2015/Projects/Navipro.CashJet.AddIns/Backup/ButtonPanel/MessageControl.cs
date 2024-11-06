using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.CashJet.AddIns
{
    public class MessageControl
    {
        public MessageControl()
        {

        }

        public void showMessage(string title, string message)
        {
            MessageBox messageBox = new MessageBox();
            messageBox.init(title, message);
            messageBox.ShowDialog();
            messageBox.Dispose();
        }

        public int showConfirm(string title, string message, string button1, string button2)
        {
            ConfirmBox confirmBox = new ConfirmBox();
            confirmBox.init(title, message, button1, button2, "");
            confirmBox.ShowDialog();
            int result = confirmBox.getResult();
            confirmBox.Dispose();
            return result;
        }

        public int showConfirm(string title, string message, string button1, string button2, string button3)
        {
            ConfirmBox confirmBox = new ConfirmBox();
            confirmBox.init(title, message, button1, button2, button3);
            confirmBox.ShowDialog();
            int result = confirmBox.getResult();
            confirmBox.Dispose();
            return result;
        }

        public int showInput(string title, string message, string button1, string button2, ref string inputText)
        {
            InputBox inputBox = new InputBox();
            inputBox.init(title, message, button1, button2);
            inputBox.ShowDialog();
            int result = inputBox.getResult();
            inputText = inputBox.getInputText();
            inputBox.Dispose();
            return result;
        }

        public int showInput2(string title, string message, string button1, string button2, string button3, ref string inputText)
        {
            InputBox2 inputBox = new InputBox2();
            inputBox.init(title, message, button1, button2, button3);
            inputBox.ShowDialog();
            int result = inputBox.getResult();
            inputText = inputBox.getInputText();
            inputBox.Dispose();
            return result;
        }

        public string showKeyboard(string input)
        {
            Keyboard keyBoard = new Keyboard();
            keyBoard.init(input);
            keyBoard.ShowDialog();
            input = keyBoard.getInput();
            keyBoard.Dispose();
            return input;
        }
    }
}

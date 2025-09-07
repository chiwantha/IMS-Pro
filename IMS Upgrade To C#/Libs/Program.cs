using System;
using System.Linq;
using System.Windows.Forms;

namespace IMS_Upgrade_To_C_
{
    internal static class Program
    {
        private static frmE_Attendance eAttendanceForm;
        private static frmManualAttendance mAttendanceForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Register the key event handler at the application level
            Application.AddMessageFilter(new GlobalKeyHandler());

            // Show the Splash form
            var splashForm = new Splash();
            splashForm.Show();

            Application.Run();
        }

        private class GlobalKeyHandler : IMessageFilter
        {
            private const int WM_KEYDOWN = 0x0100;
            private const Keys F4TriggerKey = Keys.F4;
            private const Keys F3TriggerKey = Keys.F3;
            private const Keys F2TriggerKey = Keys.F2;
            private const Keys F1TriggerKey = Keys.F1;
            private bool formIsOpen = false;
            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_KEYDOWN)
                {
                    Keys pressedKey = (Keys)m.WParam;

                    if (pressedKey == F4TriggerKey)
                    {
                        ToggleEAttendanceForm();
                        return true;
                    }
                    else if (pressedKey == F2TriggerKey)
                    {
                        SwapFormOnF2();
                        return true;
                    }
                    else if (pressedKey == F1TriggerKey)
                    {
                        //SwapFormOnF1();
                        return true;
                    }
                    else if (pressedKey == F3TriggerKey)
                    {
                        ToggleMAttendanceForm();
                        return true;
                    }
                }

                return false;
            }
            private void ToggleEAttendanceForm()
            {

                if (eAttendanceForm == null || eAttendanceForm.IsDisposed)
                {

                    eAttendanceForm = new frmE_Attendance();
                    eAttendanceForm.Show();
                    formIsOpen = true;
                }
                else
                {
                    if (formIsOpen)
                    {

                        eAttendanceForm.BringToFront();
                        formIsOpen = false;
                    }
                    else
                    {

                        eAttendanceForm.Close();
                    }
                }
            }
            private void ToggleMAttendanceForm()
            {

                if (mAttendanceForm == null || mAttendanceForm.IsDisposed)
                {

                    mAttendanceForm = new frmManualAttendance();
                    mAttendanceForm.Show();
                    formIsOpen = true;
                }
                else
                {
                    if (formIsOpen)
                    {

                        mAttendanceForm.BringToFront();
                        formIsOpen = false;
                    }
                    else
                    {

                        mAttendanceForm.Close();
                    }
                }
            }
            private void SwapFormOnF2()
            {
                MDI mdiParentForm = Application.OpenForms.OfType<MDI>().FirstOrDefault();

                if (mdiParentForm != null)
                {
                    pgInvoiceCart pgInvoicecart = new pgInvoiceCart(mdiParentForm.get_username_strip());
                    mdiParentForm.SwapForms(pgInvoicecart);
                }
            }
            
        }
    }
}

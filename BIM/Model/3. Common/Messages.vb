Imports DevExpress.XtraEditors

Public Module Messages
    Public Function message_text(description_text As String, button As MessageBoxButtons) As DialogResult
        WindowsFormsSettings.DefaultFont = New Font("Arial Narrow", 10)
        WindowsFormsSettings.DefaultLookAndFeel.SkinName = "Office 2019 White"
        WindowsFormsSettings.DefaultLookAndFeel.UseDefaultLookAndFeel = True

        Return XtraMessageBox.Show(description_text, "Business Information Management", button, MessageBoxIcon.Information)
    End Function
End Module

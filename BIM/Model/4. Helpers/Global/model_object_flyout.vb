Imports DevExpress.XtraBars.Docking2010.Customization

Public Class model_object_flyout
    Inherits FlyoutDialog

    Public Sub New(owner As Form, control As Control)
        MyBase.New(owner, sessions.flyout_action, control, True)
        InitStyle()
    End Sub

    Protected Overrides Sub InitStyle()
        MyBase.Appearance.BackColor = Color.White
        MyBase.Appearance.BackColor2 = Color.White
        MyBase.FormBorderStyle = FormBorderStyle.None
        MyBase.ShowIcon = False
        MyBase.ShowInTaskbar = False
        MyBase.KeyPreview = True
        MyBase.FormBorderStyle = FormBorderStyle.None
        MyBase.StartPosition = FormStartPosition.Manual
        MyBase.SetStyle(ControlStyles.Selectable, False)
        MyBase.SetStyle(ControlStyles.UserPaint, True)
        MyBase.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        MyBase.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.SetStyle(ControlStyles.ResizeRedraw, True)
    End Sub
End Class

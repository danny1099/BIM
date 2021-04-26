Imports System.ComponentModel
Imports System.Reflection

Public Module LookFeel
    Public Function skin_manager() As String
        Dim assembly As Assembly = GetType(DevExpress.UserSkins.SkinBIM2021.SkinBIM2021).Assembly
        DevExpress.Skins.SkinManager.Default.RegisterAssembly(assembly)

        Return "My Office 2019 White"
    End Function

    Public Sub frame_form(sender As Object, e As PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, New Rectangle(0, 0, sender.Width, sender.Height), Color.FromName("Silver"),
       CDec(1.0), ButtonBorderStyle.Solid, Color.FromName("Silver"), CDec(1.0),
       ButtonBorderStyle.Solid, Color.FromName("Silver"), CDec(1.0), ButtonBorderStyle.Solid,
     Color.FromName("Silver"), CDec(1.0), ButtonBorderStyle.Solid)
    End Sub
End Module
Public Class SkinRegistration
    Inherits Component

    Private Shared current_instance As SkinRegistration = Nothing
    Private Shared ReadOnly sync As New Object

    Public Shared ReadOnly Property Instance() As SkinRegistration
        Get
            If current_instance Is Nothing Then
                SyncLock sync
                    If current_instance Is Nothing Then
                        current_instance = New SkinRegistration()
                    End If
                End SyncLock
            End If

            Return current_instance
        End Get
    End Property

    Public Sub New()
        DevExpress.Skins.SkinManager.Default.RegisterAssembly(GetType(DevExpress.UserSkins.SkinBIM2021.SkinBIM2021).Assembly)
    End Sub
End Class

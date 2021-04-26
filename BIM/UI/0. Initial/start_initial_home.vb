Public Class start_initial_home
    Private object_look As SkinRegistration = SkinRegistration.Instance

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As System.Windows.Forms.CreateParams = MyBase.CreateParams
            cp.Style = cp.Style Or &H20000
            cp.Style = cp.Style Or &H40
            Return cp
        End Get
    End Property


#Region "contructors"
    Public Sub New()
        InitializeComponent()
        PerformAutoScale()

        'Cambia el tamaño del formulario que abarque la pantalla del usuario
        Me.Size = My.Computer.Screen.WorkingArea.Size
    End Sub

    Private Sub module_load(sender As Object, e As EventArgs) Handles MyBase.Load
        sessions.person_code = 1
    End Sub

    Private Sub module_frame(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        frame_form(sender, e)
    End Sub
#End Region

#Region "options"
    Private Sub minimize_option(sender As Object, e As EventArgs) Handles object_button_minimize.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub closed_session(sender As Object, e As EventArgs) Handles object_button_closed.Click
        Close()
    End Sub

    Private Sub update_option(sender As Object, e As EventArgs) Handles object_button_update.Click
    End Sub
#End Region
End Class
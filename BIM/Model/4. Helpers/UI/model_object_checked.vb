Imports System.ComponentModel

<ToolboxItem(True)>
Public Class model_object_checked
    Inherits DevExpress.XtraEditors.CheckedComboBoxEdit


#Region "objectos"
    Private color_left As Color
    Private color_top As Color
    Private color_right As Color
    Private color_bottom As Color
#End Region

#Region "propiedades"
    Property ColorLeft As Color
        Get
            Return color_left
        End Get
        Set(value As Color)
            color_left = value
        End Set
    End Property

    Property ColorTop As Color
        Get
            Return color_top
        End Get
        Set(value As Color)
            color_top = value
        End Set
    End Property

    Property ColorRight As Color
        Get
            Return color_right
        End Get
        Set(value As Color)
            color_right = value
        End Set
    End Property

    Property ColorBottom As Color
        Get
            Return color_bottom
        End Get
        Set(value As Color)
            color_bottom = value
        End Set
    End Property
#End Region


#Region "metodos"
    Public Sub New()
    End Sub

    Public Sub New(source As DataTable, field As String, display As String)
        Datasources(source, field, display)
    End Sub

    Public Sub Datasources(source As DataTable, field As String, display As String)
        Try
            MyBase.Properties.Items.Clear()
            MyBase.Properties.DataSource = Nothing
            MyBase.Properties.DataSource = source
            MyBase.Properties.ValueMember = field
            MyBase.Properties.DisplayMember = display
        Catch ex As Exception
        End Try
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        ControlPaint.DrawBorder(e.Graphics, Me.ClientRectangle, color_left, 1, ButtonBorderStyle.Solid, color_top, 1, ButtonBorderStyle.Solid, color_right, 1, ButtonBorderStyle.Solid, color_bottom, 1, ButtonBorderStyle.Solid)
    End Sub
#End Region

End Class

Namespace sessions
    Public Module Session

#Region "properties"
        Public Property person_name As String
        Public Property person_code As Integer
        Public Property person_photo As Image
        Public Property position_name As String
        Public Property position_rol As Integer
        Public Property flyout_action As New DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction
#End Region

#Region "methods"
        Public Function search_permission() As DataTable
            Return json_deserialize("")
        End Function
#End Region
    End Module
End Namespace

